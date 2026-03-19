using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.BlockObstacles
{
	// Token: 0x0200000C RID: 12
	public class LayeredBlockObstacle : BaseComponent, IAwakableComponent, IFinishedStateListener, IPersistentEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600002E RID: 46 RVA: 0x000026C8 File Offset: 0x000008C8
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x00002700 File Offset: 0x00000900
		public event EventHandler MaxOccupancyRangeChanged;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002735 File Offset: 0x00000935
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000273D File Offset: 0x0000093D
		public float OccupancyRange { get; private set; }

		// Token: 0x06000032 RID: 50 RVA: 0x00002746 File Offset: 0x00000946
		public LayeredBlockObstacle(EventBus eventBus, BlockOccupationLayerFactory blockOccupationLayerFactory, ITerrainService terrainService)
		{
			this._eventBus = eventBus;
			this._blockOccupationLayerFactory = blockOccupationLayerFactory;
			this._terrainService = terrainService;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000276E File Offset: 0x0000096E
		public float MaxOccupancyRange
		{
			get
			{
				return this._maxOccupancyRange;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002776 File Offset: 0x00000976
		public void Awake()
		{
			this._layeredBlockObstacleSpec = base.GetComponent<LayeredBlockObstacleSpec>();
			base.DisableComponent();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000278A File Offset: 0x0000098A
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(LayeredBlockObstacle.LayeredVerticalBlockObstacleKey).Set(LayeredBlockObstacle.OccupancyRangeKey, this.OccupancyRange);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027A8 File Offset: 0x000009A8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(LayeredBlockObstacle.LayeredVerticalBlockObstacleKey);
			this.OccupancyRange = component.Get(LayeredBlockObstacle.OccupancyRangeKey);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027D4 File Offset: 0x000009D4
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
			this._anchorWorldHeight = base.Transform.TransformPoint(this._layeredBlockObstacleSpec.AnchorPosition).y;
			this.CreateBlockOccupationLayers();
			base.EnableComponent();
			if (this.TryUpdateBlockOccupationLayers(-3.4028235E+38f, this.OccupancyRange))
			{
				this.UpdateMaxOccupancyRange();
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000284A File Offset: 0x00000A4A
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
			this._terrainService.TerrainHeightChanged -= this.OnTerrainHeightChanged;
			this.RemoveBlockOccupationLayers();
			base.DisableComponent();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000287C File Offset: 0x00000A7C
		[OnEvent]
		public void OnBlockObjectSet(BlockObjectSetEvent blockObjectSetEvent)
		{
			if (base.Enabled)
			{
				IEnumerable<Vector3Int> allCoordinates = blockObjectSetEvent.BlockObject.PositionedBlocks.GetAllCoordinates();
				this.UpdateMaxOccupancyRangeIfCoordinatesMatch(allCoordinates);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028AC File Offset: 0x00000AAC
		[OnEvent]
		public void OnBlockObjectUnset(BlockObjectUnsetEvent blockObjectUnsetEvent)
		{
			if (base.Enabled)
			{
				IEnumerable<Vector3Int> allCoordinates = blockObjectUnsetEvent.BlockObject.PositionedBlocks.GetAllCoordinates();
				this.UpdateMaxOccupancyRangeIfCoordinatesMatch(allCoordinates);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028D9 File Offset: 0x00000AD9
		public void ModifyOccupancyRange(float occupancyRangeDelta)
		{
			this.SetOccupancyRange(this.OccupancyRange + occupancyRangeDelta);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028EC File Offset: 0x00000AEC
		public void SetOccupancyRange(float occupancyRange)
		{
			float occupancyRange2 = this.OccupancyRange;
			this.OccupancyRange = Mathf.Clamp(occupancyRange, 0f, this.MaxOccupancyRange);
			if (this.TryUpdateBlockOccupationLayers(occupancyRange2, this.OccupancyRange))
			{
				this.UpdateMaxOccupancyRange();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000292C File Offset: 0x00000B2C
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			if (base.Enabled)
			{
				this.UpdateMaxOccupancyRangeIfCoordinatesMatch(terrainHeightChangeEventArgs.Change.Coordinates);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002958 File Offset: 0x00000B58
		public void CreateBlockOccupationLayers()
		{
			for (int i = this.GetStartingGridHeight(); i >= 0; i--)
			{
				BlockOccupationLayer item = this._blockOccupationLayerFactory.Create(base.Transform, this._layeredBlockObstacleSpec.AnchorPosition, i, this._layeredBlockObstacleSpec.LayerSize);
				this._blockOccupationLayers.Add(item);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029B0 File Offset: 0x00000BB0
		public int GetStartingGridHeight()
		{
			return Mathf.FloorToInt(this._anchorWorldHeight) - this._layeredBlockObstacleSpec.BlockCreationOffset;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029CC File Offset: 0x00000BCC
		public void RemoveBlockOccupationLayers()
		{
			foreach (BlockOccupationLayer blockOccupationLayer in this._blockOccupationLayers)
			{
				blockOccupationLayer.Remove();
			}
			this._blockOccupationLayers.Clear();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A28 File Offset: 0x00000C28
		public bool TryUpdateBlockOccupationLayers(float oldOccupancyRange, float newOccupancyRange)
		{
			int num = Mathf.FloorToInt(this._anchorWorldHeight - oldOccupancyRange);
			int num2 = Mathf.FloorToInt(this._anchorWorldHeight - newOccupancyRange);
			if (num != num2)
			{
				this.UpdateBlockOccupationLayers(num2);
				return true;
			}
			return false;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A60 File Offset: 0x00000C60
		public void UpdateBlockOccupationLayers(int minimumGridHeight)
		{
			bool flag = false;
			foreach (BlockOccupationLayer blockOccupationLayer in this._blockOccupationLayers)
			{
				if (LayeredBlockObstacle.LayerIsValidToOccupy(blockOccupationLayer, minimumGridHeight) && !flag)
				{
					blockOccupationLayer.AddToServices();
				}
				else
				{
					blockOccupationLayer.RemoveFromServices();
					flag = true;
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002ACC File Offset: 0x00000CCC
		public static bool LayerIsValidToOccupy(BlockOccupationLayer blockOccupationLayer, int minimumGridHeight)
		{
			bool flag = blockOccupationLayer.CanBeAddedToServices();
			bool flag2 = blockOccupationLayer.GridHeight >= minimumGridHeight;
			return flag && flag2;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public void UpdateMaxOccupancyRangeIfCoordinatesMatch(IEnumerable<Vector3Int> coordinates)
		{
			foreach (Vector3Int value in coordinates)
			{
				if (this.UpdateMaxOccupancyRangeIfCoordinatesMatch(value.XY()))
				{
					break;
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B44 File Offset: 0x00000D44
		public bool UpdateMaxOccupancyRangeIfCoordinatesMatch(Vector2Int coordinates)
		{
			if (this._blockOccupationLayers.First<BlockOccupationLayer>().Contains(coordinates))
			{
				this.UpdateMaxOccupancyRange();
				return true;
			}
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B64 File Offset: 0x00000D64
		public void UpdateMaxOccupancyRange()
		{
			float maxOccupancyRange = this._maxOccupancyRange;
			this._maxOccupancyRange = this.GetMaxPotentialOccupancyRange();
			this.DecreaseOccupancyRangeForEachInvalidLayer(ref this._maxOccupancyRange);
			this.OccupancyRange = Mathf.Clamp(this.OccupancyRange, 0f, this.MaxOccupancyRange);
			if (!maxOccupancyRange.Equals(this._maxOccupancyRange))
			{
				EventHandler maxOccupancyRangeChanged = this.MaxOccupancyRangeChanged;
				if (maxOccupancyRangeChanged == null)
				{
					return;
				}
				maxOccupancyRangeChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public float GetMaxPotentialOccupancyRange()
		{
			int gridHeight = this._blockOccupationLayers.Last<BlockOccupationLayer>().GridHeight;
			return this._anchorWorldHeight - (float)gridHeight;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BFC File Offset: 0x00000DFC
		public void DecreaseOccupancyRangeForEachInvalidLayer(ref float occupancyRange)
		{
			for (int i = 0; i < this._blockOccupationLayers.Count; i++)
			{
				if (!this._blockOccupationLayers[i].CanBeAddedToServices())
				{
					int num = this._blockOccupationLayers.Count - i;
					occupancyRange -= (float)num;
					return;
				}
			}
		}

		// Token: 0x04000013 RID: 19
		public static readonly ComponentKey LayeredVerticalBlockObstacleKey = new ComponentKey("LayeredVerticalBlockObstacle");

		// Token: 0x04000014 RID: 20
		public static readonly PropertyKey<float> OccupancyRangeKey = new PropertyKey<float>("OccupancyRange");

		// Token: 0x04000017 RID: 23
		public readonly EventBus _eventBus;

		// Token: 0x04000018 RID: 24
		public readonly BlockOccupationLayerFactory _blockOccupationLayerFactory;

		// Token: 0x04000019 RID: 25
		public readonly ITerrainService _terrainService;

		// Token: 0x0400001A RID: 26
		public LayeredBlockObstacleSpec _layeredBlockObstacleSpec;

		// Token: 0x0400001B RID: 27
		public float _anchorWorldHeight;

		// Token: 0x0400001C RID: 28
		public float _maxOccupancyRange;

		// Token: 0x0400001D RID: 29
		public readonly List<BlockOccupationLayer> _blockOccupationLayers = new List<BlockOccupationLayer>();
	}
}
