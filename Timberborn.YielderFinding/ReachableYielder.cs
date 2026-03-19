using System;
using Timberborn.Yielding;

namespace Timberborn.YielderFinding
{
	// Token: 0x02000005 RID: 5
	public readonly struct ReachableYielder : IComparable<ReachableYielder>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022A8 File Offset: 0x000004A8
		public Yielder Yielder { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022B0 File Offset: 0x000004B0
		public float Distance { get; }

		// Token: 0x0600000C RID: 12 RVA: 0x000022B8 File Offset: 0x000004B8
		public ReachableYielder(Yielder yielder, float distance)
		{
			this.Yielder = yielder;
			this.Distance = distance;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C8 File Offset: 0x000004C8
		public int CompareTo(ReachableYielder other)
		{
			int num = this.Distance.CompareTo(other.Distance);
			if (num != 0)
			{
				return num;
			}
			return this.Yielder.InstantiationOrder.CompareTo(other.Yielder.InstantiationOrder);
		}
	}
}
