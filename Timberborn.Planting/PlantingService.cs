using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.MapStateSystem;
using Timberborn.NaturalResources;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000024 RID: 36
	public class PlantingService : ISaveableSingleton, ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00003E3C File Offset: 0x0000203C
		public PlantingService(ISingletonLoader singletonLoader, ITerrainService terrainService, EventBus eventBus, SpawnValidationService spawnValidationService, MapEditorMode mapEditorMode, PlantingMapSerializer plantingMapSerializer, IBlockService blockService)
		{
			this._singletonLoader = singletonLoader;
			this._terrainService = terrainService;
			this._eventBus = eventBus;
			this._spawnValidationService = spawnValidationService;
			this._mapEditorMode = mapEditorMode;
			this._plantingMapSerializer = plantingMapSerializer;
			this._blockService = blockService;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003E9A File Offset: 0x0000209A
		public IEnumerable<Vector3Int> PlantingCoordinates
		{
			get
			{
				return this._plantingMap.GetCoordinatesWithSetResource();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003EA8 File Offset: 0x000020A8
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(PlantingService.PlantingServiceKey, out objectLoader))
			{
				this._plantingMap = objectLoader.Get<PlantingMap>(PlantingService.PlantingMapKey, this._plantingMapSerializer);
				using (IEnumerator<Vector3Int> enumerator = this._plantingMap.GetCoordinatesWithSetResource().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Vector3Int coordinates = enumerator.Current;
						this.UpdatePlantingSpot(coordinates);
					}
					goto IL_76;
				}
			}
			this._plantingMap = new PlantingMap(this._terrainService.Size);
			IL_76:
			this._eventBus.Register(this);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003F48 File Offset: 0x00002148
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				singletonSaver.GetSingleton(PlantingService.PlantingServiceKey).Set<PlantingMap>(PlantingService.PlantingMapKey, this._plantingMap, this._plantingMapSerializer);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003F78 File Offset: 0x00002178
		public void PostLoad()
		{
			foreach (Vector3Int field in this._plantingMap.GetCoordinatesWithSetResource())
			{
				this._terrainService.SetField(field);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003FD0 File Offset: 0x000021D0
		[OnEvent]
		public void OnBlockObjectSet(BlockObjectSetEvent blockObjectSetEvent)
		{
			this.UpdateOccupiedPlantingSpots(blockObjectSetEvent.BlockObject);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003FDE File Offset: 0x000021DE
		[OnEvent]
		public void OnBlockObjectUnset(BlockObjectUnsetEvent blockObjectUnsetEvent)
		{
			this.UpdateOccupiedPlantingSpots(blockObjectUnsetEvent.BlockObject);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003FEC File Offset: 0x000021EC
		public bool IsResourceAt(Vector3Int coordinates)
		{
			return this._plantingMap.GetResource(coordinates) != null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003FFD File Offset: 0x000021FD
		public string GetResourceAt(Vector3Int coordinates)
		{
			return this._plantingMap.GetResource(coordinates);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000400C File Offset: 0x0000220C
		public PlantingSpot? GetSpotAt(Vector3Int coordinates)
		{
			PlantingSpot value;
			if (!this._plantingSpots.TryGetValue(coordinates, out value))
			{
				return null;
			}
			return new PlantingSpot?(value);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004039 File Offset: 0x00002239
		public void SetPlantingCoordinates(Vector3Int coordinates, string resource)
		{
			this.UnsetPlantingCoordinates(coordinates);
			this._terrainService.SetField(coordinates);
			this._plantingMap.SetResource(coordinates, resource);
			this.UpdatePlantingSpot(coordinates);
			this._eventBus.Post(new PlantingCoordinatesSetEvent(coordinates, resource));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004074 File Offset: 0x00002274
		public void UnsetPlantingCoordinates(Vector3Int coordinates)
		{
			this._terrainService.UnsetField(coordinates);
			string resource = this._plantingMap.GetResource(coordinates);
			this._plantingMap.UnsetResource(coordinates);
			this._plantingSpots.Remove(coordinates);
			this._eventBus.Post(new PlantingCoordinatesUnsetEvent(coordinates, resource));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000040C5 File Offset: 0x000022C5
		public void ReservePlantingCoordinates(Vector3Int coordinates)
		{
			this._reservedCoordinates.Add(coordinates);
			this._plantingSpots.Remove(coordinates);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000040E1 File Offset: 0x000022E1
		public void UnreservePlantingCoordinates(Vector3Int coordinates)
		{
			this._reservedCoordinates.Remove(coordinates);
			this.UpdatePlantingSpot(coordinates);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000040F8 File Offset: 0x000022F8
		public bool TryGetPlantingBlocker(Vector3Int coordinates, out BlockObject plantingBlocker)
		{
			string resourceAt = this.GetResourceAt(coordinates);
			if (resourceAt != null)
			{
				plantingBlocker = this.CreatePlantingSpot(coordinates, resourceAt).PlantingBlocker;
				return plantingBlocker != null;
			}
			plantingBlocker = null;
			return false;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000412C File Offset: 0x0000232C
		public void UpdateOccupiedPlantingSpots(BlockObject blockObject)
		{
			foreach (Vector3Int coordinates in blockObject.PositionedBlocks.GetOccupiedCoordinates())
			{
				this.UpdatePlantingSpotAtTerrainHeight(coordinates);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004180 File Offset: 0x00002380
		public void UpdatePlantingSpotAtTerrainHeight(Vector3Int coordinates)
		{
			this.UpdatePlantingSpot(coordinates);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000418C File Offset: 0x0000238C
		public void UpdatePlantingSpot(Vector3Int coordinates)
		{
			string resourceAt = this.GetResourceAt(coordinates);
			if (resourceAt != null && !this._reservedCoordinates.Contains(coordinates))
			{
				this._plantingSpots[coordinates] = this.CreatePlantingSpot(coordinates, resourceAt);
				return;
			}
			this._plantingSpots.Remove(coordinates);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000041D4 File Offset: 0x000023D4
		public PlantingSpot CreatePlantingSpot(Vector3Int coordinates, string resourceToPlant)
		{
			if (!this._spawnValidationService.IsUnobstructed(coordinates, resourceToPlant))
			{
				BlockObject pathObjectAt = this._blockService.GetPathObjectAt(coordinates);
				if (pathObjectAt != null)
				{
					return new PlantingSpot(coordinates, resourceToPlant, pathObjectAt);
				}
			}
			return new PlantingSpot(coordinates, resourceToPlant, null);
		}

		// Token: 0x04000069 RID: 105
		public static readonly SingletonKey PlantingServiceKey = new SingletonKey("PlantingService");

		// Token: 0x0400006A RID: 106
		public static readonly PropertyKey<PlantingMap> PlantingMapKey = new PropertyKey<PlantingMap>("PlantingMap");

		// Token: 0x0400006B RID: 107
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400006C RID: 108
		public readonly ITerrainService _terrainService;

		// Token: 0x0400006D RID: 109
		public readonly EventBus _eventBus;

		// Token: 0x0400006E RID: 110
		public readonly SpawnValidationService _spawnValidationService;

		// Token: 0x0400006F RID: 111
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000070 RID: 112
		public readonly PlantingMapSerializer _plantingMapSerializer;

		// Token: 0x04000071 RID: 113
		public readonly IBlockService _blockService;

		// Token: 0x04000072 RID: 114
		public PlantingMap _plantingMap;

		// Token: 0x04000073 RID: 115
		public readonly HashSet<Vector3Int> _reservedCoordinates = new HashSet<Vector3Int>();

		// Token: 0x04000074 RID: 116
		public readonly Dictionary<Vector3Int, PlantingSpot> _plantingSpots = new Dictionary<Vector3Int, PlantingSpot>();
	}
}
