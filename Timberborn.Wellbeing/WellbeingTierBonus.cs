using System;

namespace Timberborn.Wellbeing
{
	// Token: 0x02000014 RID: 20
	public readonly struct WellbeingTierBonus
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002817 File Offset: 0x00000A17
		public int Wellbeing { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000281F File Offset: 0x00000A1F
		public float Bonus { get; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002827 File Offset: 0x00000A27
		public WellbeingTierBonus(int wellbeing, float bonus)
		{
			this.Wellbeing = wellbeing;
			this.Bonus = bonus;
		}
	}
}
