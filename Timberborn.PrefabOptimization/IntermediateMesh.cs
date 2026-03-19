using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000014 RID: 20
	public class IntermediateMesh
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003FF3 File Offset: 0x000021F3
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003FFB File Offset: 0x000021FB
		public int VertexCount { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004004 File Offset: 0x00002204
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000400C File Offset: 0x0000220C
		public Vector3[] Vertices { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004015 File Offset: 0x00002215
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000401D File Offset: 0x0000221D
		public Vector3[] Normals { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004026 File Offset: 0x00002226
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000402E File Offset: 0x0000222E
		public Vector4[] Tangents { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004037 File Offset: 0x00002237
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000403F File Offset: 0x0000223F
		public Color32[] Colors { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004048 File Offset: 0x00002248
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00004050 File Offset: 0x00002250
		public Vector4[] UV0 { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004059 File Offset: 0x00002259
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004061 File Offset: 0x00002261
		public Vector4[] UV1 { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000406A File Offset: 0x0000226A
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00004072 File Offset: 0x00002272
		public Vector4[] UV2 { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000407B File Offset: 0x0000227B
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00004083 File Offset: 0x00002283
		public ValueTuple<NullableKey<Material>, int[]>[] Submeshes { get; set; }
	}
}
