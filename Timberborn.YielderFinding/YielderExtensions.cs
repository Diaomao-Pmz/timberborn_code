using System;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.Yielding;

namespace Timberborn.YielderFinding
{
	// Token: 0x02000006 RID: 6
	public static class YielderExtensions
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000230F File Offset: 0x0000050F
		public static bool IsYieldingOrAlive(this Yielder yielder)
		{
			return yielder.IsYielding || yielder.IsAlive();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002321 File Offset: 0x00000521
		public static bool IsAlive(this Yielder yielder)
		{
			return !yielder.GetComponent<LivingNaturalResource>().IsDead;
		}
	}
}
