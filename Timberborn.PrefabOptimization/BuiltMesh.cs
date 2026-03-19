using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000010 RID: 16
	public readonly struct BuiltMesh
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000338D File Offset: 0x0000158D
		public Mesh Mesh { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003395 File Offset: 0x00001595
		public Material[] Materials { get; }

		// Token: 0x0600005C RID: 92 RVA: 0x0000339D File Offset: 0x0000159D
		public BuiltMesh(Mesh mesh, Material[] materials)
		{
			this.Mesh = mesh;
			this.Materials = materials;
		}
	}
}
