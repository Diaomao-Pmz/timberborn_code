using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.NaturalResourcesContamination;
using Timberborn.NaturalResourcesMoisture;

namespace Timberborn.NaturalResourcesLifecycleUI
{
	// Token: 0x02000004 RID: 4
	public class DeadNaturalResourceDescriber
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public DeadNaturalResourceDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public string Describe(BaseComponent entity)
		{
			WateredNaturalResource component = entity.GetComponent<WateredNaturalResource>();
			LivingWaterNaturalResource component2 = entity.GetComponent<LivingWaterNaturalResource>();
			ContaminatedNaturalResource component3 = entity.GetComponent<ContaminatedNaturalResource>();
			if (component3 && component3.DyingProgress.Died)
			{
				return this._loc.T(DeadNaturalResourceDescriber.DiedFromContaminationLocKey);
			}
			if (component && component.DyingProgress.Died)
			{
				return this._loc.T(DeadNaturalResourceDescriber.DriedLocKey);
			}
			if (component2 && component2.DyingProgress.Died)
			{
				string key = component2.DeathByFlooding ? DeadNaturalResourceDescriber.DiedFromTooMuchWaterLocKey : DeadNaturalResourceDescriber.DiedFromNotEnoughWaterLocKey;
				return this._loc.T(key);
			}
			return this._loc.T(DeadNaturalResourceDescriber.GenericDiedLocKey);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string GenericDiedLocKey = "NaturalResources.GenericDied";

		// Token: 0x04000007 RID: 7
		public static readonly string DriedLocKey = "NaturalResources.Dried";

		// Token: 0x04000008 RID: 8
		public static readonly string DiedFromNotEnoughWaterLocKey = "NaturalResources.DiedFromNotEnoughWater";

		// Token: 0x04000009 RID: 9
		public static readonly string DiedFromTooMuchWaterLocKey = "NaturalResources.DiedFromTooMuchWater";

		// Token: 0x0400000A RID: 10
		public static readonly string DiedFromContaminationLocKey = "NaturalResources.DiedFromContamination";

		// Token: 0x0400000B RID: 11
		public readonly ILoc _loc;
	}
}
