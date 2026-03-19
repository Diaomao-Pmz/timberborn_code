using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.MapStateSystem;
using Timberborn.TerrainPhysics;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x0200000D RID: 13
	public class ExplosionOutcomeGatherer
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002ABE File Offset: 0x00000CBE
		public ExplosionOutcomeGatherer(ITerrainPhysicsService terrainPhysicsService, ITerrainService terrainService, IBlockService blockService, MapSize mapSize)
		{
			this._terrainPhysicsService = terrainPhysicsService;
			this._terrainService = terrainService;
			this._blockService = blockService;
			this._mapSize = mapSize;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002AF9 File Offset: 0x00000CF9
		public void GetAllAffectedTerrainAndObjects(UnstableCore unstableCore, HashSet<Vector3Int> affectedTiles, HashSet<Vector3Int> affectedTerrain, HashSet<BlockObject> affectedObjects)
		{
			this.GetAffectedTiles(unstableCore, affectedTiles);
			this.GetAffectedTerrainAndObjects(affectedTiles.AsReadOnlyHashSet<Vector3Int>(), affectedTerrain, affectedObjects);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B14 File Offset: 0x00000D14
		public Dictionary<int, HashSet<Vector3Int>> GetAffectedTilesPerRadius(Vector3 center, float radius)
		{
			Dictionary<int, HashSet<Vector3Int>> dictionary = new Dictionary<int, HashSet<Vector3Int>>();
			foreach (ValueTuple<Vector3Int, float> valueTuple in this.GetCoordinatesInRadiusWithDistance(center, radius))
			{
				Vector3Int item = valueTuple.Item1;
				int group = Mathf.Max(Mathf.FloorToInt(valueTuple.Item2), 0);
				this.AddCoordinatesToRadiusGroup(dictionary, group, item);
			}
			return dictionary;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B84 File Offset: 0x00000D84
		public void GetAffectedTerrainAndObjects(ReadOnlyHashSet<Vector3Int> affectedTiles, HashSet<Vector3Int> affectedTerrain, HashSet<BlockObject> affectedObjects)
		{
			foreach (Vector3Int vector3Int in affectedTiles)
			{
				if (this._terrainService.Underground(vector3Int))
				{
					affectedTerrain.Add(vector3Int);
				}
				foreach (BlockObject blockObject in this._blockService.GetObjectsAt(vector3Int))
				{
					if (!blockObject.HasComponent<INonStackPickable>() && blockObject.HasComponent<EntityComponent>())
					{
						affectedObjects.Add(blockObject);
					}
				}
			}
			this.AddObjectsOnTopOfTerrain(affectedTiles, affectedTerrain, affectedObjects);
			this.ApplyTerrainPhysics(affectedTerrain, affectedObjects);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C54 File Offset: 0x00000E54
		public void GetAffectedTiles(UnstableCore unstableCore, HashSet<Vector3Int> affectedTiles)
		{
			float radius = (float)unstableCore.ExplosionRadius + unstableCore.InnerRadius;
			Vector3 explosionCenter = unstableCore.ExplosionCenter;
			foreach (ValueTuple<Vector3Int, float> valueTuple in this.GetCoordinatesInRadiusWithDistance(explosionCenter, radius))
			{
				Vector3Int item = valueTuple.Item1;
				if (this._mapSize.ContainsInTotal(item))
				{
					affectedTiles.Add(item);
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public void ApplyTerrainPhysics(HashSet<Vector3Int> affectedTerrain, HashSet<BlockObject> affectedObjects)
		{
			this._terrainPhysicsService.GetTerrainAndBlockObjectStack(affectedTerrain, affectedObjects, this._additionalTerrain, this._additionalBlockObjects);
			affectedTerrain.AddRange(this._additionalTerrain);
			affectedObjects.AddRange(this._additionalBlockObjects);
			this._additionalTerrain.Clear();
			this._additionalBlockObjects.Clear();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D24 File Offset: 0x00000F24
		public IEnumerable<ValueTuple<Vector3Int, float>> GetCoordinatesInRadiusWithDistance(Vector3 center, float radius)
		{
			int num = Mathf.FloorToInt(center.x - radius);
			int maxX = Mathf.CeilToInt(center.x + radius);
			int minY = Mathf.FloorToInt(center.y - radius);
			int maxY = Mathf.CeilToInt(center.y + radius);
			int minZ = Mathf.FloorToInt(center.z - radius);
			int maxZ = Mathf.CeilToInt(center.z + radius);
			int num2;
			for (int x = num; x <= maxX; x = num2 + 1)
			{
				for (int y = minY; y <= maxY; y = num2 + 1)
				{
					for (int z = minZ; z <= maxZ; z = num2 + 1)
					{
						float magnitude = (new Vector3((float)x + 0.5f, (float)y + 0.5f, (float)z + 0.5f) - center).magnitude;
						if (magnitude <= radius)
						{
							yield return new ValueTuple<Vector3Int, float>(new Vector3Int(x, y, z), magnitude);
						}
						num2 = z;
					}
					num2 = y;
				}
				num2 = x;
			}
			yield break;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D3C File Offset: 0x00000F3C
		public void AddCoordinatesToRadiusGroup(Dictionary<int, HashSet<Vector3Int>> affectedTilesPerRadius, int group, Vector3Int coordinates)
		{
			HashSet<Vector3Int> hashSet;
			if (!affectedTilesPerRadius.TryGetValue(group, out hashSet))
			{
				hashSet = new HashSet<Vector3Int>();
				affectedTilesPerRadius[group] = hashSet;
			}
			hashSet.Add(coordinates);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D6C File Offset: 0x00000F6C
		public void AddObjectsOnTopOfTerrain(ReadOnlyHashSet<Vector3Int> affectedTiles, HashSet<Vector3Int> affectedTerrain, HashSet<BlockObject> affectedObjects)
		{
			foreach (Vector3Int value in affectedTerrain)
			{
				Vector3Int vector3Int = value.Above();
				if (!affectedTiles.Contains(vector3Int) && !this._terrainService.Underground(vector3Int))
				{
					this.GetAllObjectsOnTerrain(affectedObjects, vector3Int);
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public void GetAllObjectsOnTerrain(HashSet<BlockObject> affectedObjects, Vector3Int tileAbove)
		{
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(tileAbove))
			{
				if (!blockObject.HasComponent<INonStackPickable>())
				{
					MatterBelow matterBelow = blockObject.PositionedBlocks.GetBlock(tileAbove).MatterBelow;
					if (matterBelow == MatterBelow.GroundOrStackable || matterBelow == MatterBelow.Ground)
					{
						affectedObjects.Add(blockObject);
					}
				}
			}
		}

		// Token: 0x04000026 RID: 38
		public readonly ITerrainPhysicsService _terrainPhysicsService;

		// Token: 0x04000027 RID: 39
		public readonly ITerrainService _terrainService;

		// Token: 0x04000028 RID: 40
		public readonly IBlockService _blockService;

		// Token: 0x04000029 RID: 41
		public readonly MapSize _mapSize;

		// Token: 0x0400002A RID: 42
		public readonly HashSet<Vector3Int> _additionalTerrain = new HashSet<Vector3Int>();

		// Token: 0x0400002B RID: 43
		public readonly HashSet<BlockObject> _additionalBlockObjects = new HashSet<BlockObject>();
	}
}
