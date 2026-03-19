using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000016 RID: 22
	public class NavMeshObjectUpdater
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003328 File Offset: 0x00001528
		public NavMeshObjectUpdater(NavMeshGroupService navMeshGroupService)
		{
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003384 File Offset: 0x00001584
		public void Update(BlockObject blockObject, NavMeshObject navMeshObject, BlockObjectNavMeshSettingsSpec blockObjectNavMeshSettingsSpec)
		{
			navMeshObject.Reset();
			if (blockObjectNavMeshSettingsSpec != null)
			{
				this.AddManuallySetEdges(blockObject, blockObjectNavMeshSettingsSpec);
				this.AddEdgesAddedByStackable(blockObject, blockObjectNavMeshSettingsSpec);
				foreach (NavMeshEdge navMeshEdge in this._addedEdges)
				{
					navMeshObject.AddEdge(navMeshEdge);
				}
			}
			foreach (Block block in blockObject.PositionedBlocks.GetOccupiedBlocks())
			{
				Vector3Int coordinates = block.Coordinates;
				if (block.Occupation.Intersects(BlockOccupations.Bottom))
				{
					navMeshObject.AddRestrictedCoordinates(coordinates);
					this._bottomOccupiedCoordinates.Add(coordinates);
				}
				if (block.Occupation.Intersects(BlockOccupations.Top))
				{
					this._topOccupiedCoordinates.Add(coordinates);
				}
			}
			this.AddUnblockedEdges(blockObject, blockObjectNavMeshSettingsSpec);
			this.AddBlockedEdges(blockObject, blockObjectNavMeshSettingsSpec);
			foreach (NavMeshEdge navMeshEdge2 in this._blockedEdges)
			{
				if (this.CanAddBlockingEdge(navMeshEdge2))
				{
					navMeshObject.BlockEdge(navMeshEdge2);
				}
			}
			this._addedEdges.Clear();
			this._bottomOccupiedCoordinates.Clear();
			this._topOccupiedCoordinates.Clear();
			this._coordinatesBelowEntranceLevel.Clear();
			this._blockedEdges.Clear();
			this._unblockedEdges.Clear();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000351C File Offset: 0x0000171C
		public void AddManuallySetEdges(BlockObject blockObject, BlockObjectNavMeshSettingsSpec blockObjectNavMeshSettingsSpec)
		{
			foreach (BlockObjectNavMeshEdgeGroupSpec blockObjectNavMeshEdgeGroupSpec in blockObjectNavMeshSettingsSpec.EdgeGroups)
			{
				foreach (BlockObjectNavMeshEdgeSpec blockObjectNavMeshEdgeSpec in blockObjectNavMeshEdgeGroupSpec.AddedEdges)
				{
					Vector3Int vector3Int = blockObject.TransformCoordinates(blockObjectNavMeshEdgeSpec.Start);
					Vector3Int vector3Int2 = blockObject.TransformCoordinates(blockObjectNavMeshEdgeSpec.End);
					this._addedEdges.Add(this.GetManuallySetEdge(vector3Int, vector3Int2, blockObjectNavMeshEdgeGroupSpec));
					if (blockObjectNavMeshEdgeSpec.IsTwoWay)
					{
						this._addedEdges.Add(this.GetManuallySetEdge(vector3Int2, vector3Int, blockObjectNavMeshEdgeGroupSpec));
					}
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000035CC File Offset: 0x000017CC
		public NavMeshEdge GetManuallySetEdge(Vector3Int start, Vector3Int end, BlockObjectNavMeshEdgeGroupSpec edgeGroup)
		{
			int groupId = edgeGroup.UseGroup ? this._navMeshGroupService.GetOrAddGroupId(edgeGroup.GroupName) : this._navMeshGroupService.GetDefaultGroupId();
			return NavMeshEdge.CreateGrouped(start, end, groupId, edgeGroup.IsPath, edgeGroup.Cost);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003614 File Offset: 0x00001814
		public void AddEdgesAddedByStackable(BlockObject blockObject, BlockObjectNavMeshSettingsSpec blockObjectNavMeshSettingsSpec)
		{
			if (blockObjectNavMeshSettingsSpec.GenerateFloorsOnStackable)
			{
				foreach (Block block in blockObject.PositionedBlocks.GetAllBlocks())
				{
					if (block.Stackable.IsStackable())
					{
						Vector3Int vector3Int = block.Coordinates.Above();
						foreach (Vector3Int vector3Int2 in Deltas.Neighbors4Vector3Int)
						{
							this._addedEdges.Add(NavMeshEdge.CreateDefault(vector3Int, vector3Int + vector3Int2));
						}
					}
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000036B0 File Offset: 0x000018B0
		public void AddUnblockedEdges(BlockObject blockObject, BlockObjectNavMeshSettingsSpec blockObjectNavMeshSettingsSpec)
		{
			if (blockObjectNavMeshSettingsSpec != null)
			{
				foreach (BlockObjectNavMeshUnblockedCoordinatesSpec blockObjectNavMeshUnblockedCoordinatesSpec in blockObjectNavMeshSettingsSpec.UnblockedCoordinates)
				{
					Vector3Int vector3Int = blockObject.TransformCoordinates(blockObjectNavMeshUnblockedCoordinatesSpec.Coordinates);
					int orAddGroupId = this._navMeshGroupService.GetOrAddGroupId(blockObjectNavMeshUnblockedCoordinatesSpec.Group);
					foreach (Vector3Int vector3Int2 in Deltas.Neighbors8Vector3IntOrdered)
					{
						Vector3Int end = vector3Int + vector3Int2;
						this._unblockedEdges.Add(NavMeshEdge.CreateBlocking(vector3Int, end, orAddGroupId));
					}
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003750 File Offset: 0x00001950
		public void AddBlockedEdges(BlockObject blockObject, BlockObjectNavMeshSettingsSpec blockObjectNavMeshSettingsSpec)
		{
			foreach (Vector3Int vector3Int in this._topOccupiedCoordinates)
			{
				this.AddBlockedEdges(vector3Int, vector3Int.Above());
			}
			if (blockObjectNavMeshSettingsSpec != null && blockObjectNavMeshSettingsSpec.BlockedEdges.Length > 0)
			{
				foreach (BlockObjectNavMeshBlockedEdgeSpec blockObjectNavMeshBlockedEdgeSpec in blockObjectNavMeshSettingsSpec.BlockedEdges)
				{
					Vector3Int start = blockObject.TransformCoordinates(blockObjectNavMeshBlockedEdgeSpec.Start);
					Vector3Int end = blockObject.TransformCoordinates(blockObjectNavMeshBlockedEdgeSpec.End);
					int orAddGroupId = this._navMeshGroupService.GetOrAddGroupId(blockObjectNavMeshBlockedEdgeSpec.Group);
					this._blockedEdges.Add(NavMeshEdge.CreateBlocking(start, end, orAddGroupId));
				}
			}
			if (blockObjectNavMeshSettingsSpec != null && blockObjectNavMeshSettingsSpec.NoAutoWalls)
			{
				return;
			}
			NavMeshEdge? entranceEdge = this.GetEntranceEdge(blockObject);
			if (entranceEdge != null)
			{
				NavMeshEdge valueOrDefault = entranceEdge.GetValueOrDefault();
				this.AddEdgesBlockedByElevatedEntrance(blockObject);
				this.AddEdgesBlockedByWalls(this._bottomOccupiedCoordinates);
				this._blockedEdges.Remove(valueOrDefault);
				return;
			}
			this.AddEdgesBlockedByCoordinates(this._bottomOccupiedCoordinates);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003880 File Offset: 0x00001A80
		public void AddEdgesBlockedByElevatedEntrance(BlockObject blockObject)
		{
			if (blockObject.Entrance.Coordinates.z > 0)
			{
				int z = blockObject.PositionedEntrance.Coordinates.z;
				foreach (Block block in blockObject.PositionedBlocks.GetAllBlocks())
				{
					if (block.Coordinates.z < z && block.Occupation.HasBottomOrFloorOrFull())
					{
						this._coordinatesBelowEntranceLevel.Add(block.Coordinates);
					}
				}
				this.AddEdgesBlockedByCoordinates(this._coordinatesBelowEntranceLevel);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003920 File Offset: 0x00001B20
		public void AddEdgesBlockedByCoordinates(ICollection<Vector3Int> coordinates)
		{
			foreach (Vector3Int coordinates2 in coordinates)
			{
				this.AddBlockedEdgesToNeighbors(coordinates2, coordinates);
			}
			this.AddEdgesBlockedByWalls(coordinates);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003970 File Offset: 0x00001B70
		public void AddBlockedEdgesToNeighbors(Vector3Int coordinates, ICollection<Vector3Int> neighbors)
		{
			foreach (Vector3Int vector3Int in Deltas.Neighbors8Vector3IntOrdered)
			{
				Vector3Int vector3Int2 = coordinates + vector3Int;
				if (neighbors.Contains(vector3Int2))
				{
					this.AddBlockedEdges(coordinates, vector3Int2);
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000039B4 File Offset: 0x00001BB4
		public void AddEdgesBlockedByWalls(ICollection<Vector3Int> coordinates)
		{
			foreach (Vector3Int coordinates2 in coordinates)
			{
				this.AddEdgesBlockedByWall(coordinates2, coordinates);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003A00 File Offset: 0x00001C00
		public NavMeshEdge? GetEntranceEdge(BlockObject blockObject)
		{
			if (blockObject.HasEntrance)
			{
				PositionedEntrance positionedEntrance = blockObject.PositionedEntrance;
				return new NavMeshEdge?(NavMeshEdge.CreateBlocking(positionedEntrance.DoorstepCoordinates, positionedEntrance.Coordinates, this._navMeshGroupService.GetDefaultGroupId()));
			}
			return null;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A48 File Offset: 0x00001C48
		public void AddEdgesBlockedByWall(Vector3Int coordinates, ICollection<Vector3Int> ignoredNeighbors)
		{
			foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
			{
				if (!ignoredNeighbors.Contains(coordinates + vector3Int))
				{
					this.AddEdgesBlockedByWall(coordinates, vector3Int);
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003A88 File Offset: 0x00001C88
		public void AddEdgesBlockedByWall(Vector3Int occupiedCoords, Vector3Int neighborDelta)
		{
			Vector3Int vector3Int = occupiedCoords + neighborDelta;
			this.AddBlockedEdges(occupiedCoords, vector3Int);
			Vector3Int vector3Int2;
			vector3Int2..ctor(neighborDelta.y, neighborDelta.x, 0);
			this.AddBlockedEdges(occupiedCoords, vector3Int + vector3Int2);
			this.AddBlockedEdges(occupiedCoords, vector3Int - vector3Int2);
			this.AddBlockedEdges(occupiedCoords + vector3Int2, vector3Int);
			this.AddBlockedEdges(occupiedCoords - vector3Int2, vector3Int);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public void AddBlockedEdges(Vector3Int start, Vector3Int end)
		{
			foreach (int groupId in this._navMeshGroupService.GetAllGroupIds())
			{
				NavMeshEdge item = NavMeshEdge.CreateBlocking(start, end, groupId);
				if (!this._unblockedEdges.Contains(item))
				{
					this._blockedEdges.Add(item);
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003B6C File Offset: 0x00001D6C
		public bool CanAddBlockingEdge(NavMeshEdge edge)
		{
			foreach (NavMeshEdge navMeshEdge in this._addedEdges)
			{
				if (navMeshEdge.Start == edge.Start && navMeshEdge.End == edge.End && navMeshEdge.GroupId == edge.GroupId)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000027 RID: 39
		public readonly NavMeshGroupService _navMeshGroupService;

		// Token: 0x04000028 RID: 40
		public readonly HashSet<NavMeshEdge> _addedEdges = new HashSet<NavMeshEdge>();

		// Token: 0x04000029 RID: 41
		public readonly HashSet<Vector3Int> _bottomOccupiedCoordinates = new HashSet<Vector3Int>();

		// Token: 0x0400002A RID: 42
		public readonly HashSet<Vector3Int> _topOccupiedCoordinates = new HashSet<Vector3Int>();

		// Token: 0x0400002B RID: 43
		public readonly HashSet<Vector3Int> _coordinatesBelowEntranceLevel = new HashSet<Vector3Int>();

		// Token: 0x0400002C RID: 44
		public readonly HashSet<NavMeshEdge> _blockedEdges = new HashSet<NavMeshEdge>();

		// Token: 0x0400002D RID: 45
		public readonly HashSet<NavMeshEdge> _unblockedEdges = new HashSet<NavMeshEdge>();
	}
}
