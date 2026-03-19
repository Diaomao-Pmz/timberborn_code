using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000064 RID: 100
	public class NavMeshUpdateBuilderFactory
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00006E8B File Offset: 0x0000508B
		public NavMeshUpdateBuilderFactory(NodeIdService nodeIdService)
		{
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006E9A File Offset: 0x0000509A
		public NavMeshUpdate.Builder Create()
		{
			return new NavMeshUpdate.Builder(this._nodeIdService);
		}

		// Token: 0x040000EF RID: 239
		public readonly NodeIdService _nodeIdService;
	}
}
