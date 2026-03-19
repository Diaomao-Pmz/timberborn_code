using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000052 RID: 82
	public readonly struct NavMeshChange
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00005A05 File Offset: 0x00003C05
		public NavMeshChange(NavMeshChangeType changeType, int startNodeId, int endNodeId, int groupId, float cost)
		{
			this._changeType = changeType;
			this._startNodeId = startNodeId;
			this._endNodeId = endNodeId;
			this._groupId = groupId;
			this._cost = cost;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005A2C File Offset: 0x00003C2C
		public void Apply(TerrainNavMeshSource terrainNavMeshSource, NavMeshUpdate.Builder navMeshUpdateBuilder)
		{
			switch (this._changeType)
			{
			case NavMeshChangeType.None:
				break;
			case NavMeshChangeType.AddEdge:
				terrainNavMeshSource.AddEdge(this._startNodeId, this._endNodeId, this._groupId, this._cost);
				break;
			case NavMeshChangeType.BlockEdge:
				terrainNavMeshSource.BlockEdge(this._startNodeId, this._endNodeId, this._groupId);
				break;
			case NavMeshChangeType.RemoveEdge:
				terrainNavMeshSource.RemoveEdge(this._startNodeId, this._endNodeId, this._groupId, this._cost);
				break;
			case NavMeshChangeType.UnblockEdge:
				terrainNavMeshSource.UnblockEdge(this._startNodeId, this._endNodeId, this._groupId);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (this._changeType != NavMeshChangeType.None)
			{
				navMeshUpdateBuilder.AddTerrainNode(this._startNodeId);
				navMeshUpdateBuilder.AddTerrainNode(this._endNodeId);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public void Apply(RoadNavMeshSource roadNavMeshSource, NavMeshUpdate.Builder navMeshUpdateBuilder)
		{
			switch (this._changeType)
			{
			case NavMeshChangeType.None:
				break;
			case NavMeshChangeType.AddEdge:
				roadNavMeshSource.AddEdge(this._startNodeId, this._endNodeId, this._groupId, this._cost);
				break;
			case NavMeshChangeType.BlockEdge:
				roadNavMeshSource.BlockEdge(this._startNodeId, this._endNodeId, this._groupId);
				break;
			case NavMeshChangeType.RemoveEdge:
				roadNavMeshSource.RemoveEdge(this._startNodeId, this._endNodeId, this._groupId, this._cost);
				break;
			case NavMeshChangeType.UnblockEdge:
				roadNavMeshSource.UnblockEdge(this._startNodeId, this._endNodeId, this._groupId);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (this._changeType != NavMeshChangeType.None)
			{
				navMeshUpdateBuilder.AddRoadNode(this._startNodeId);
				navMeshUpdateBuilder.AddRoadNode(this._endNodeId);
			}
		}

		// Token: 0x040000A1 RID: 161
		public readonly NavMeshChangeType _changeType;

		// Token: 0x040000A2 RID: 162
		public readonly int _startNodeId;

		// Token: 0x040000A3 RID: 163
		public readonly int _endNodeId;

		// Token: 0x040000A4 RID: 164
		public readonly int _groupId;

		// Token: 0x040000A5 RID: 165
		public readonly float _cost;
	}
}
