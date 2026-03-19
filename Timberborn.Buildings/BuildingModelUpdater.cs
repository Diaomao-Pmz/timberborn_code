using System;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x02000012 RID: 18
	public class BuildingModelUpdater : ILoadableSingleton, ISingletonPreviewNavMeshListener, ISingletonInstantNavMeshListener
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003088 File Offset: 0x00001288
		public BuildingModelUpdater(IBlockService blockService, EventBus eventBus)
		{
			this._blockService = blockService;
			this._eventBus = eventBus;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000309E File Offset: 0x0000129E
		public void Load()
		{
			this._eventBus.Register(this);
			this._loaded = true;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000030B3 File Offset: 0x000012B3
		public void OnPreviewNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.OnNavMeshUpdated(navMeshUpdate);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000030B3 File Offset: 0x000012B3
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.OnNavMeshUpdated(navMeshUpdate);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000030BC File Offset: 0x000012BC
		[OnEvent]
		public void OnBlockObjectSetEvent(BlockObjectSetEvent blockObjectSetEvent)
		{
			this.UpdateBuildingsModelsAround(blockObjectSetEvent.BlockObject);
			this.UpdateBuildingModelsBelow(blockObjectSetEvent.BlockObject);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000030D6 File Offset: 0x000012D6
		[OnEvent]
		public void OnBlockObjectUnsetEvent(BlockObjectUnsetEvent blockObjectUnsetEvent)
		{
			this.UpdateBuildingsModelsAround(blockObjectUnsetEvent.BlockObject);
			this.UpdateBuildingModelsBelow(blockObjectUnsetEvent.BlockObject);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000030F0 File Offset: 0x000012F0
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.UpdateBuildingsModelsAt(navMeshUpdate.TerrainCoordinates);
			this.UpdateBuildingModelsBelow(navMeshUpdate.TerrainCoordinates);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000310C File Offset: 0x0000130C
		public void UpdateBuildingsModelsAt(ReadOnlyList<Vector3Int> coordinates)
		{
			if (this._loaded)
			{
				for (int i = 0; i < coordinates.Count; i++)
				{
					this.UpdateBuildingModelsAt(coordinates[i]);
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003144 File Offset: 0x00001344
		public void UpdateBuildingModelsAt(Vector3Int coordinates)
		{
			ReadOnlyList<BlockObject> objectsAt = this._blockService.GetObjectsAt(coordinates);
			for (int i = 0; i < objectsAt.Count; i++)
			{
				BlockObjectModelController component = objectsAt[i].GetComponent<BlockObjectModelController>();
				if (component != null)
				{
					component.UpdateModel();
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003188 File Offset: 0x00001388
		public void UpdateBuildingsModelsAround(BlockObject blockObject)
		{
			Vector3Int coordinates = blockObject.Coordinates;
			Vector3Int vector = blockObject.Blocks.Size - new Vector3Int(1, 1, 1);
			Vector3Int b = coordinates + blockObject.Orientation.Transform(vector);
			ValueTuple<Vector3Int, Vector3Int> valueTuple = Vectors.MinMax(coordinates, b);
			Vector3Int item = valueTuple.Item1;
			Vector3Int item2 = valueTuple.Item2;
			for (int i = item.z; i <= item2.z; i++)
			{
				for (int j = item.x - 1; j <= item2.x + 1; j++)
				{
					this.UpdateBuildingModelsAt(new Vector3Int(j, item.y - 1, i));
					this.UpdateBuildingModelsAt(new Vector3Int(j, item2.y + 1, i));
				}
				for (int k = item.y; k <= item2.y; k++)
				{
					this.UpdateBuildingModelsAt(new Vector3Int(item.x - 1, k, i));
					this.UpdateBuildingModelsAt(new Vector3Int(item2.x + 1, k, i));
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003298 File Offset: 0x00001498
		public void UpdateBuildingModelsBelow(ReadOnlyList<Vector3Int> coordinates)
		{
			if (this._loaded)
			{
				for (int i = 0; i < coordinates.Count; i++)
				{
					this.UpdateBuildingModelsAt(coordinates[i].Below());
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000032D4 File Offset: 0x000014D4
		public void UpdateBuildingModelsBelow(BlockObject blockObject)
		{
			Vector3Int coordinates = blockObject.Coordinates;
			this.UpdateBuildingModelsAt(new Vector3Int(coordinates.x, coordinates.y, coordinates.z - 1));
		}

		// Token: 0x0400002C RID: 44
		public readonly IBlockService _blockService;

		// Token: 0x0400002D RID: 45
		public readonly EventBus _eventBus;

		// Token: 0x0400002E RID: 46
		public bool _loaded;
	}
}
