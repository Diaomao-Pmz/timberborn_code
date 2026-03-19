using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000034 RID: 52
	public interface INavMeshGraph
	{
		// Token: 0x0600013D RID: 317
		void ConnectNodes(int aNodeId, int bNodeId, int groupId, float cost);

		// Token: 0x0600013E RID: 318
		void DisconnectNodes(int aNodeId, int bNodeId);
	}
}
