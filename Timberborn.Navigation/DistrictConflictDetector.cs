using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x0200000F RID: 15
	public class DistrictConflictDetector
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002EDC File Offset: 0x000010DC
		public bool AreDistrictsInConflict(RoadNavMeshGraph roadNavMeshGraph, DistrictObstacleService districtObstacleService, IEnumerable<int> districtCenters)
		{
			this._assignedNodes.Clear();
			this._enqueuedNodes.Clear();
			int num = 0;
			using (IEnumerator<int> enumerator = districtCenters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int nodeId = enumerator.Current;
					this.EnqueueNode(districtObstacleService, nodeId, num++);
				}
				goto IL_C2;
			}
			IL_4C:
			DistrictConflictDetector.AssignedNode assignedNode = this._enqueuedNodes.Dequeue();
			int id = assignedNode.Id;
			int districtId = assignedNode.DistrictId;
			ReadOnlyList<NavMeshNode> neighbors = roadNavMeshGraph.GetNeighbors(id);
			for (int i = 0; i < neighbors.Count; i++)
			{
				int id2 = neighbors[i].Id;
				int num2;
				if (this._assignedNodes.TryGetValue(id2, out num2))
				{
					if (num2 != districtId)
					{
						return true;
					}
				}
				else
				{
					this.EnqueueNode(districtObstacleService, id2, districtId);
				}
			}
			IL_C2:
			if (this._enqueuedNodes.IsEmpty<DistrictConflictDetector.AssignedNode>())
			{
				return false;
			}
			goto IL_4C;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FCC File Offset: 0x000011CC
		public void EnqueueNode(DistrictObstacleService districtObstacleService, int nodeId, int nodeDistrictId)
		{
			if (!districtObstacleService.IsSetObstacle(nodeId))
			{
				this._assignedNodes[nodeId] = nodeDistrictId;
				this._enqueuedNodes.Enqueue(new DistrictConflictDetector.AssignedNode(nodeId, nodeDistrictId));
			}
		}

		// Token: 0x04000029 RID: 41
		public readonly Dictionary<int, int> _assignedNodes = new Dictionary<int, int>();

		// Token: 0x0400002A RID: 42
		public readonly Queue<DistrictConflictDetector.AssignedNode> _enqueuedNodes = new Queue<DistrictConflictDetector.AssignedNode>();

		// Token: 0x02000010 RID: 16
		public readonly struct AssignedNode
		{
			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00003014 File Offset: 0x00001214
			public int Id { get; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000060 RID: 96 RVA: 0x0000301C File Offset: 0x0000121C
			public int DistrictId { get; }

			// Token: 0x06000061 RID: 97 RVA: 0x00003024 File Offset: 0x00001224
			public AssignedNode(int id, int districtId)
			{
				this.Id = id;
				this.DistrictId = districtId;
			}
		}
	}
}
