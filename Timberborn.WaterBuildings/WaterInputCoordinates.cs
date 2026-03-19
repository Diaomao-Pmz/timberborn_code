using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000030 RID: 48
	public class WaterInputCoordinates : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity, IPostPlacementChangeListener, IPostInitializableEntity, IPersistentEntity, IDuplicable<WaterInputCoordinates>, IDuplicable
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600022C RID: 556 RVA: 0x00006D30 File Offset: 0x00004F30
		// (remove) Token: 0x0600022D RID: 557 RVA: 0x00006D68 File Offset: 0x00004F68
		public event EventHandler<Vector3Int> CoordinatesChanged;

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00006D9D File Offset: 0x00004F9D
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00006DA5 File Offset: 0x00004FA5
		public Vector3Int Coordinates { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00006DAE File Offset: 0x00004FAE
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00006DB6 File Offset: 0x00004FB6
		public int Depth { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00006DBF File Offset: 0x00004FBF
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00006DC7 File Offset: 0x00004FC7
		public int DepthLimit { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00006DD0 File Offset: 0x00004FD0
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public bool UseDepthLimit { get; private set; }

		// Token: 0x06000236 RID: 566 RVA: 0x00006DE1 File Offset: 0x00004FE1
		public WaterInputCoordinates(ITerrainService terrainService, IBlockService blockService, EventBus eventBus)
		{
			this._terrainService = terrainService;
			this._blockService = blockService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006E0C File Offset: 0x0000500C
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._waterInputSpec = base.GetComponent<WaterInputSpec>();
			this.DepthLimit = this._waterInputSpec.MaxDepth;
			Asserts.ValueIsInRange<int>(this.DepthLimit, 1, int.MaxValue, "DepthLimit");
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006E58 File Offset: 0x00005058
		public void InitializeEntity()
		{
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
			this._eventBus.Register(this);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006E7D File Offset: 0x0000507D
		public void PostInitializeEntity()
		{
			this.UpdateCoordinatesAndDepth();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006E85 File Offset: 0x00005085
		public void DeleteEntity()
		{
			this._terrainService.TerrainHeightChanged -= this.OnTerrainHeightChanged;
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006EAA File Offset: 0x000050AA
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(WaterInputCoordinates.WaterInputCoordinatesKey);
			component.Set(WaterInputCoordinates.DepthLimitKey, this.DepthLimit);
			component.Set(WaterInputCoordinates.UseDepthLimitKey, this.UseDepthLimit);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006ED8 File Offset: 0x000050D8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WaterInputCoordinates.WaterInputCoordinatesKey);
			this.DepthLimit = Math.Min(component.Get(WaterInputCoordinates.DepthLimitKey), this._waterInputSpec.MaxDepth);
			this.UseDepthLimit = component.Get(WaterInputCoordinates.UseDepthLimitKey);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006F23 File Offset: 0x00005123
		public void DuplicateFrom(WaterInputCoordinates source)
		{
			this.UseDepthLimit = source.UseDepthLimit;
			this.DepthLimit = Math.Min(source.DepthLimit, this._waterInputSpec.MaxDepth);
			this.UpdateCoordinatesAndDepth();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006E7D File Offset: 0x0000507D
		public void OnPostPlacementChanged()
		{
			this.UpdateCoordinatesAndDepth();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006F53 File Offset: 0x00005153
		public void SetDepthLimit(int depth)
		{
			this.UseDepthLimit = true;
			this.DepthLimit = depth;
			this.UpdateCoordinatesAndDepth();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006F69 File Offset: 0x00005169
		public void DisableDepthLimit()
		{
			this.UseDepthLimit = false;
			this.DepthLimit = this._waterInputSpec.MaxDepth;
			this.UpdateCoordinatesAndDepth();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006F89 File Offset: 0x00005189
		[OnEvent]
		public void OnBlockObjectSetEvent(BlockObjectSetEvent blockObjectSetEvent)
		{
			if (this.ShouldUpdate(blockObjectSetEvent.BlockObject.PositionedBlocks))
			{
				this.UpdateCoordinatesAndDepth();
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006FA4 File Offset: 0x000051A4
		[OnEvent]
		public void OnBlockObjectUnsetEvent(BlockObjectUnsetEvent blockObjectUnsetEvent)
		{
			if (this.ShouldUpdate(blockObjectUnsetEvent.BlockObject.PositionedBlocks))
			{
				this.UpdateCoordinatesAndDepth();
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006FC0 File Offset: 0x000051C0
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			if (terrainHeightChangeEventArgs.Change.Coordinates == this.Coordinates.XY())
			{
				this.UpdateCoordinatesAndDepth();
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006FF4 File Offset: 0x000051F4
		public bool ShouldUpdate(PositionedBlocks changedBlocks)
		{
			foreach (Vector3Int value in changedBlocks.GetAllCoordinates())
			{
				if (value.XY() == this.Coordinates.XY() && value.z <= this._blockObject.CoordinatesAtBaseZ.z)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007078 File Offset: 0x00005278
		public void UpdateCoordinatesAndDepth()
		{
			Vector3Int waterInputCoordinates = this._waterInputSpec.WaterInputCoordinates;
			Vector3Int startCoordinates = this._blockObject.TransformCoordinates(waterInputCoordinates) + new Vector3Int(0, 0, this._blockObject.BaseZ + 1);
			this.Coordinates = new Vector3Int(startCoordinates.x, startCoordinates.y, this.GetZCoordinateLimitedByDepth(startCoordinates));
			this.Depth = startCoordinates.z - this.Coordinates.z;
			EventHandler<Vector3Int> coordinatesChanged = this.CoordinatesChanged;
			if (coordinatesChanged == null)
			{
				return;
			}
			coordinatesChanged(this, this.Coordinates);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000710C File Offset: 0x0000530C
		public int GetZCoordinateLimitedByDepth(Vector3Int startCoordinates)
		{
			int z = startCoordinates.z;
			int num = this.UseDepthLimit ? this.DepthLimit : this._waterInputSpec.MaxDepth;
			for (int i = z - 1; i >= z - num; i--)
			{
				Vector3Int coordinates;
				coordinates..ctor(startCoordinates.x, startCoordinates.y, i);
				if (this.IsTileOccupied(coordinates))
				{
					return i + 1;
				}
			}
			return z - num;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007173 File Offset: 0x00005373
		public bool IsTileOccupied(Vector3Int coordinates)
		{
			if (this._terrainService.Underground(coordinates))
			{
				return true;
			}
			this._blockService.GetIntersectingObjectsAt(coordinates, WaterInputCoordinates.InvalidOccupations, this._blockObjectCache);
			bool result = this.HasOccupyingObject();
			this._blockObjectCache.Clear();
			return result;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000071B0 File Offset: 0x000053B0
		public bool HasOccupyingObject()
		{
			foreach (BlockObject blockObject in this._blockObjectCache)
			{
				if (!blockObject.Overridable && blockObject != this._blockObject && !blockObject.HasComponent<PipeIntersectionAllowerSpec>())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000D0 RID: 208
		public static readonly BlockOccupations InvalidOccupations = ~(BlockOccupations.Floor | BlockOccupations.Corners);

		// Token: 0x040000D1 RID: 209
		public static readonly ComponentKey WaterInputCoordinatesKey = new ComponentKey("WaterInputCoordinates");

		// Token: 0x040000D2 RID: 210
		public static readonly PropertyKey<int> DepthLimitKey = new PropertyKey<int>("DepthLimit");

		// Token: 0x040000D3 RID: 211
		public static readonly PropertyKey<bool> UseDepthLimitKey = new PropertyKey<bool>("UseDepthLimit");

		// Token: 0x040000D9 RID: 217
		public readonly ITerrainService _terrainService;

		// Token: 0x040000DA RID: 218
		public readonly IBlockService _blockService;

		// Token: 0x040000DB RID: 219
		public readonly EventBus _eventBus;

		// Token: 0x040000DC RID: 220
		public BlockObject _blockObject;

		// Token: 0x040000DD RID: 221
		public WaterInputSpec _waterInputSpec;

		// Token: 0x040000DE RID: 222
		public readonly List<BlockObject> _blockObjectCache = new List<BlockObject>();
	}
}
