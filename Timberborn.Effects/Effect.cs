using System;

namespace Timberborn.Effects
{
	// Token: 0x02000007 RID: 7
	public readonly struct Effect
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002285 File Offset: 0x00000485
		public float Points { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000228D File Offset: 0x0000048D
		public int Count { get; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002295 File Offset: 0x00000495
		public Effect(float points, int count)
		{
			this.Points = points;
			this.Count = count;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022A5 File Offset: 0x000004A5
		public static Effect From(InstantEffect instantEffect)
		{
			return new Effect(instantEffect.Points, instantEffect.Count);
		}
	}
}
