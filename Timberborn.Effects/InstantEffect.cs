using System;
using Timberborn.NeedSpecs;

namespace Timberborn.Effects
{
	// Token: 0x0200000C RID: 12
	public readonly struct InstantEffect
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002515 File Offset: 0x00000715
		public string NeedId { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000251D File Offset: 0x0000071D
		public float Points { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002525 File Offset: 0x00000725
		public int Count { get; }

		// Token: 0x0600002D RID: 45 RVA: 0x0000252D File Offset: 0x0000072D
		public InstantEffect(string needId, float points, int count)
		{
			this.NeedId = needId;
			this.Points = points;
			this.Count = count;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002544 File Offset: 0x00000744
		public static InstantEffect FromSpec(InstantEffectSpec instantEffectSpec, int count)
		{
			return new InstantEffect(instantEffectSpec.NeedId, instantEffectSpec.Points, count);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002558 File Offset: 0x00000758
		public static InstantEffect DiscretizeContinuousEffect(string needId)
		{
			return new InstantEffect(needId, 0.05f, 20);
		}
	}
}
