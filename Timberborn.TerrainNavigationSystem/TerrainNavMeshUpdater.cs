using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainNavigationSystem
{
	// Token: 0x02000005 RID: 5
	public class TerrainNavMeshUpdater : ILoadableSingleton
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		public TerrainNavMeshUpdater(ITerrainService terrainService, INavMeshService navMeshService, NavMeshGroupService navMeshGroupService)
		{
			this._terrainService = terrainService;
			this._navMeshService = navMeshService;
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
		public void Load()
		{
			this.AddTerrainToNavMesh();
			this._terrainService.PreTerrainHeightChanged += this.OnPreTerrainHeightChanged;
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002134 File Offset: 0x00000334
		public void OnPreTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
			for (int i = change.From; i <= change.To + 1; i++)
			{
				Vector3Int vector3Int = change.Coordinates.ToVector3Int(i);
				foreach (Vector3Int vector3Int2 in Deltas.Neighbors8AndSelfVector3Int)
				{
					Vector3Int coordinates = vector3Int + vector3Int2;
					if (this.TileIsWalkable(coordinates))
					{
						this.RemoveEdgesToNeighbors(coordinates);
					}
				}
				this.UpdateBlockingEdges(vector3Int, false, i < change.To + 1);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021C4 File Offset: 0x000003C4
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
			for (int i = change.From; i <= change.To + 1; i++)
			{
				Vector3Int vector3Int = change.Coordinates.ToVector3Int(i);
				foreach (Vector3Int vector3Int2 in Deltas.Neighbors8AndSelfVector3Int)
				{
					Vector3Int coordinates = vector3Int + vector3Int2;
					if (this.TileIsWalkable(coordinates))
					{
						this.AddEdgesToNeighbors(coordinates);
					}
				}
				this.UpdateBlockingEdges(vector3Int, true, i < change.To + 1);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002254 File Offset: 0x00000454
		public void AddTerrainToNavMesh()
		{
			Vector3Int size = this._terrainService.Size;
			for (int i = 0; i < size.x; i++)
			{
				for (int j = 0; j < size.y; j++)
				{
					for (int k = 0; k < size.z; k++)
					{
						Vector3Int vector3Int;
						vector3Int..ctor(i, j, k);
						if (this._terrainService.OnGround(vector3Int))
						{
							this.AddEdgesToNeighbors(vector3Int);
						}
						else if (this._terrainService.Underground(vector3Int))
						{
							this.UpdateBlockingEdges(vector3Int, true);
						}
					}
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022E0 File Offset: 0x000004E0
		public void AddEdgesToNeighbors(Vector3Int coordinates)
		{
			this.GetValidEdgesToNeighbors(coordinates);
			foreach (NavMeshEdge navMeshEdge in this._validNeighbourEdgeCache)
			{
				this._navMeshService.AddEdge(navMeshEdge);
			}
			this._validNeighbourEdgeCache.Clear();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000234C File Offset: 0x0000054C
		public void RemoveEdgesToNeighbors(Vector3Int coordinates)
		{
			this.GetValidEdgesToNeighbors(coordinates);
			foreach (NavMeshEdge navMeshEdge in this._validNeighbourEdgeCache)
			{
				this._navMeshService.RemoveEdge(navMeshEdge);
			}
			this._validNeighbourEdgeCache.Clear();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023B8 File Offset: 0x000005B8
		public bool TileIsWalkable(Vector3Int coordinates)
		{
			return this._terrainService.OnGround(coordinates);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023C6 File Offset: 0x000005C6
		public void GetValidEdgesToNeighbors(Vector3Int coordinates)
		{
			this.GetEdgesToNeighbors(coordinates, Deltas.Neighbors4Vector3Int, new Func<Vector3Int, Vector3Int, bool>(this.TilesAreOrthogonallyConnected));
			this.GetEdgesToNeighbors(coordinates, Deltas.Corners4Vector3Int, new Func<Vector3Int, Vector3Int, bool>(this.TilesAreDiagonallyConnected));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023F8 File Offset: 0x000005F8
		public void GetEdgesToNeighbors(Vector3Int coordinates, Vector3Int[] neighborDeltas, Func<Vector3Int, Vector3Int, bool> tilesAreConnected)
		{
			foreach (Vector3Int vector3Int in neighborDeltas)
			{
				Vector3Int vector3Int2 = coordinates + vector3Int;
				if (tilesAreConnected(coordinates, vector3Int2))
				{
					NavMeshEdge item = NavMeshEdge.CreateDefault(coordinates, vector3Int2);
					this._validNeighbourEdgeCache.Add(item);
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002445 File Offset: 0x00000645
		public bool TilesAreOrthogonallyConnected(Vector3Int coordinates, Vector3Int neighborCoordinates)
		{
			return this.HeightAtCoordinatesIsLessThanOrEqualTo(neighborCoordinates, coordinates.z);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002458 File Offset: 0x00000658
		public bool HeightAtCoordinatesIsLessThanOrEqualTo(Vector3Int coordinates, int height)
		{
			int num;
			return this._terrainService.TryGetRelativeHeight(coordinates, out num) && coordinates.z + num <= height;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002488 File Offset: 0x00000688
		public bool TilesAreDiagonallyConnected(Vector3Int coordinates, Vector3Int neighborCoordinates)
		{
			Vector3Int vector3Int = neighborCoordinates - coordinates;
			Vector3Int vector3Int2;
			vector3Int2..ctor(vector3Int.x, 0, 0);
			Vector3Int vector3Int3;
			vector3Int3..ctor(0, vector3Int.y, 0);
			return this._terrainService.OnGround(neighborCoordinates) && this._terrainService.OnGround(coordinates + vector3Int2) && this._terrainService.OnGround(coordinates + vector3Int3);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024F4 File Offset: 0x000006F4
		public void UpdateBlockingEdges(Vector3Int coordinates, bool block, bool updateNeighbors)
		{
			this.UpdateBlockingEdges(coordinates, block);
			if (updateNeighbors)
			{
				foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
				{
					this.UpdateBlockingEdges(coordinates + vector3Int, block);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002538 File Offset: 0x00000738
		public void UpdateBlockingEdges(Vector3Int coordinates, bool block)
		{
			if (this._terrainService.Underground(coordinates))
			{
				bool flag = this._terrainService.Underground(coordinates.Below());
				foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
				{
					Vector3Int vector3Int2 = coordinates + vector3Int;
					if (!flag || !this._terrainService.Underground(vector3Int2))
					{
						this.UpdateBlockingEdge(coordinates, vector3Int2, block);
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025A8 File Offset: 0x000007A8
		public void UpdateBlockingEdge(Vector3Int start, Vector3Int end, bool block)
		{
			foreach (int groupId in this._navMeshGroupService.GetAllGroupIds())
			{
				NavMeshEdge navMeshEdge = NavMeshEdge.CreateBlocking(start, end, groupId);
				if (block)
				{
					this._navMeshService.BlockEdge(navMeshEdge);
				}
				else
				{
					this._navMeshService.UnblockEdge(navMeshEdge);
				}
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly ITerrainService _terrainService;

		// Token: 0x04000007 RID: 7
		public readonly INavMeshService _navMeshService;

		// Token: 0x04000008 RID: 8
		public readonly NavMeshGroupService _navMeshGroupService;

		// Token: 0x04000009 RID: 9
		public readonly List<NavMeshEdge> _validNeighbourEdgeCache = new List<NavMeshEdge>();
	}
}
