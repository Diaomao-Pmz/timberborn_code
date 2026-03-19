using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Cutting;
using Timberborn.EntitySystem;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.WorldPersistence;
using Timberborn.YielderFinding;
using Timberborn.Yielding;
using UnityEngine;

namespace Timberborn.Forestry
{
	// Token: 0x02000015 RID: 21
	public class TreeCuttingArea : ISaveableSingleton, ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public TreeCuttingArea(ISingletonLoader singletonLoader, IBlockService blockService, EventBus eventBus, ITerrainService terrainService, MapEditorMode mapEditorMode)
		{
			this._singletonLoader = singletonLoader;
			this._blockService = blockService;
			this._eventBus = eventBus;
			this._terrainService = terrainService;
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002C02 File Offset: 0x00000E02
		public IEnumerable<Yielder> YieldersInArea
		{
			get
			{
				return this._yieldersInArea.Values;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002C0F File Offset: 0x00000E0F
		public IEnumerable<Vector3Int> CuttingArea
		{
			get
			{
				return this._cuttingArea.AsReadOnlyEnumerable<Vector3Int>();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002C1C File Offset: 0x00000E1C
		public bool AnyYielderSelected
		{
			get
			{
				return !this._yieldersInArea.IsEmpty<KeyValuePair<Vector3Int, Yielder>>();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002C2C File Offset: 0x00000E2C
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(TreeCuttingArea.TreeCuttingAreaKey, out objectLoader))
			{
				this._cuttingArea.AddRange(objectLoader.Get(TreeCuttingArea.CuttingAreaKey));
			}
			this._eventBus.Register(this);
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002C88 File Offset: 0x00000E88
		public void PostLoad()
		{
			foreach (Vector3Int coordinates in this._cuttingArea)
			{
				this.AddTree(coordinates);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002CDC File Offset: 0x00000EDC
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				singletonSaver.GetSingleton(TreeCuttingArea.TreeCuttingAreaKey).Set(TreeCuttingArea.CuttingAreaKey, this._cuttingArea);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D06 File Offset: 0x00000F06
		public bool IsInCuttingArea(Vector3Int coordinates)
		{
			return this._cuttingArea.Contains(coordinates);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D14 File Offset: 0x00000F14
		public bool HasYielder(Vector3Int coordinates)
		{
			Yielder yielder;
			return this.IsInCuttingArea(coordinates) && this._yieldersInArea.TryGetValue(coordinates, out yielder) && yielder.IsYieldingOrAlive();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D44 File Offset: 0x00000F44
		public void AddCoordinates(IEnumerable<Vector3Int> coordinates)
		{
			foreach (Vector3Int vector3Int in coordinates)
			{
				this._cuttingArea.Add(vector3Int);
				this.AddTree(vector3Int);
			}
			this._eventBus.Post(new TreeCuttingAreaChangedEvent(true));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002DAC File Offset: 0x00000FAC
		public void RemoveCoordinates(IEnumerable<Vector3Int> coordinates)
		{
			foreach (Vector3Int vector3Int in coordinates)
			{
				this._cuttingArea.Remove(vector3Int);
				TreeComponent bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<TreeComponent>(vector3Int);
				if (bottomObjectComponentAt != null)
				{
					this.RemoveYielder(bottomObjectComponentAt);
				}
			}
			this._eventBus.Post(new TreeCuttingAreaChangedEvent(false));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E24 File Offset: 0x00001024
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			TreeComponent component = entityInitializedEvent.Entity.GetComponent<TreeComponent>();
			if (component != null)
			{
				BlockObject component2 = component.GetComponent<BlockObject>();
				if (this._cuttingArea.Contains(component2.Coordinates))
				{
					this.AddYielder(component);
					this._eventBus.Post(new TreeAddedToCuttingAreaEvent(component));
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002E74 File Offset: 0x00001074
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			TreeComponent component = entityDeletedEvent.Entity.GetComponent<TreeComponent>();
			if (component != null)
			{
				this.RemoveYielder(component);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002E98 File Offset: 0x00001098
		[OnEvent]
		public void OnCuttableCut(CuttableCutEvent cuttableCutEvent)
		{
			TreeComponent component = cuttableCutEvent.Cuttable.GetComponent<TreeComponent>();
			if (component != null)
			{
				this.RemoveYielder(component);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002EBC File Offset: 0x000010BC
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			if (this._cuttingArea.RemoveWhere((Vector3Int coordinates) => this.CoordinatesInsideChanged(coordinates, terrainHeightChangeEventArgs)) > 0)
			{
				this._eventBus.Post(new TreeCuttingAreaChangedEvent(false));
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F08 File Offset: 0x00001108
		public void AddTree(Vector3Int coordinates)
		{
			TreeComponent bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<TreeComponent>(coordinates);
			if (bottomObjectComponentAt != null)
			{
				this.AddYielder(bottomObjectComponentAt);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002F2C File Offset: 0x0000112C
		public void AddYielder(TreeComponent treeComponent)
		{
			Cuttable component = treeComponent.GetComponent<Cuttable>();
			BlockObject component2 = treeComponent.GetComponent<BlockObject>();
			this._yieldersInArea[component2.Coordinates] = component.Yielder;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002F60 File Offset: 0x00001160
		public void RemoveYielder(TreeComponent treeComponent)
		{
			BlockObject component = treeComponent.GetComponent<BlockObject>();
			this._yieldersInArea.Remove(component.Coordinates);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002F88 File Offset: 0x00001188
		public bool CoordinatesInsideChanged(Vector3Int coordinates, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
			return coordinates.XY() == change.Coordinates && coordinates.z <= change.To + 1 && coordinates.z >= change.From + 1;
		}

		// Token: 0x04000025 RID: 37
		public static readonly SingletonKey TreeCuttingAreaKey = new SingletonKey("TreeCuttingArea");

		// Token: 0x04000026 RID: 38
		public static readonly ListKey<Vector3Int> CuttingAreaKey = new ListKey<Vector3Int>("CuttingArea");

		// Token: 0x04000027 RID: 39
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000028 RID: 40
		public readonly IBlockService _blockService;

		// Token: 0x04000029 RID: 41
		public readonly EventBus _eventBus;

		// Token: 0x0400002A RID: 42
		public readonly ITerrainService _terrainService;

		// Token: 0x0400002B RID: 43
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400002C RID: 44
		public readonly HashSet<Vector3Int> _cuttingArea = new HashSet<Vector3Int>();

		// Token: 0x0400002D RID: 45
		public readonly Dictionary<Vector3Int, Yielder> _yieldersInArea = new Dictionary<Vector3Int, Yielder>();
	}
}
