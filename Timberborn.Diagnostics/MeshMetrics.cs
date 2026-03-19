using System;

namespace Timberborn.Diagnostics
{
	// Token: 0x02000007 RID: 7
	public class MeshMetrics
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002246 File Offset: 0x00000446
		public string Name { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000224E File Offset: 0x0000044E
		public int NumberOfVertices { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002256 File Offset: 0x00000456
		public int NumberOfTriangles { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000225E File Offset: 0x0000045E
		public int? NumberOfTrianglesPerTile { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002266 File Offset: 0x00000466
		public int NumberOfSubmeshes { get; }

		// Token: 0x06000014 RID: 20 RVA: 0x0000226E File Offset: 0x0000046E
		public MeshMetrics(string name, int numberOfVertices, int numberOfTriangles, int? numberOfTrianglesPerTile, int numberOfSubmeshes)
		{
			this.Name = name;
			this.NumberOfVertices = numberOfVertices;
			this.NumberOfTriangles = numberOfTriangles;
			this.NumberOfTrianglesPerTile = numberOfTrianglesPerTile;
			this.NumberOfSubmeshes = numberOfSubmeshes;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000229C File Offset: 0x0000049C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Name: ",
				this.Name,
				string.Format(", {0}: {1}", "NumberOfVertices", this.NumberOfVertices),
				string.Format(", {0}: {1}", "NumberOfTriangles", this.NumberOfTriangles),
				string.Format(", {0}: {1}", "NumberOfTrianglesPerTile", this.NumberOfTrianglesPerTile),
				string.Format(", {0}: {1}", "NumberOfSubmeshes", this.NumberOfSubmeshes)
			});
		}
	}
}
