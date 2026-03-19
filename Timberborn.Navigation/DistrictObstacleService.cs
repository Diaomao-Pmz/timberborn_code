using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000013 RID: 19
	public class DistrictObstacleService : ILoadableSingleton
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003623 File Offset: 0x00001823
		public DistrictObstacleService(NodeIdService nodeIdService)
		{
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003632 File Offset: 0x00001832
		public void Load()
		{
			this._obstacles = new bool[this._nodeIdService.NumberOfNodes];
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000364A File Offset: 0x0000184A
		public void SetObstacle(int nodeId)
		{
			if (this.IsSetObstacle(nodeId))
			{
				throw new InvalidOperationException(string.Format("Can't set obstacle at {0}", nodeId));
			}
			this._obstacles[nodeId] = true;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003674 File Offset: 0x00001874
		public void UnsetObstacle(int nodeId)
		{
			if (!this.IsSetObstacle(nodeId))
			{
				throw new InvalidOperationException(string.Format("Can't unset obstacle at {0}", nodeId));
			}
			this._obstacles[nodeId] = false;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000369E File Offset: 0x0000189E
		public bool IsSetObstacle(int nodeId)
		{
			return this._obstacles[nodeId];
		}

		// Token: 0x0400003D RID: 61
		public readonly NodeIdService _nodeIdService;

		// Token: 0x0400003E RID: 62
		public bool[] _obstacles;
	}
}
