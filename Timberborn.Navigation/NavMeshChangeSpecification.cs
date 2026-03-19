using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000054 RID: 84
	public readonly struct NavMeshChangeSpecification
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005C8B File Offset: 0x00003E8B
		public NavMeshEdge NavMeshEdge { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005C93 File Offset: 0x00003E93
		public NavMeshChangeType NavMeshChangeType { get; }

		// Token: 0x060001A1 RID: 417 RVA: 0x00005C9B File Offset: 0x00003E9B
		public NavMeshChangeSpecification(NavMeshEdge navMeshEdge, NavMeshChangeType navMeshChangeType)
		{
			this.NavMeshEdge = navMeshEdge;
			this.NavMeshChangeType = navMeshChangeType;
		}
	}
}
