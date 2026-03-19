using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000020 RID: 32
	public readonly struct FlowFieldNode
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004082 File Offset: 0x00002282
		public int Id { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000408A File Offset: 0x0000228A
		public float GScore { get; }

		// Token: 0x060000DA RID: 218 RVA: 0x00004092 File Offset: 0x00002292
		public FlowFieldNode(int id, float gScore)
		{
			this.Id = id;
			this.GScore = gScore;
		}
	}
}
