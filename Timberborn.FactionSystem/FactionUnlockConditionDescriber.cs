using System;
using Timberborn.Localization;

namespace Timberborn.FactionSystem
{
	// Token: 0x0200000C RID: 12
	public class FactionUnlockConditionDescriber
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002EAA File Offset: 0x000010AA
		public FactionUnlockConditionDescriber(FactionSpecService factionSpecService, ILoc loc)
		{
			this._factionSpecService = factionSpecService;
			this._loc = loc;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002EC0 File Offset: 0x000010C0
		public string Describe(FactionSpec factionSpec)
		{
			UnlockableFactionSpec spec = factionSpec.GetSpec<UnlockableFactionSpec>();
			if (spec == null)
			{
				return "";
			}
			return this.DescribeUnlockCondition(spec);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002EE4 File Offset: 0x000010E4
		public string DescribeUnlockCondition(UnlockableFactionSpec unlockableFactionSpec)
		{
			return this._loc.T<int, string>(FactionUnlockConditionDescriber.WellbeingConditionLocKey, unlockableFactionSpec.AverageWellbeingToUnlock, this.GetPrerequisiteFactionDisplayName(unlockableFactionSpec));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F03 File Offset: 0x00001103
		public string GetPrerequisiteFactionDisplayName(UnlockableFactionSpec unlockableFactionSpec)
		{
			return this._factionSpecService.GetFaction(unlockableFactionSpec.PrerequisiteFaction).DisplayName.Value;
		}

		// Token: 0x0400002A RID: 42
		public static readonly string WellbeingConditionLocKey = "FactionSelection.WellbeingCondition";

		// Token: 0x0400002B RID: 43
		public readonly FactionSpecService _factionSpecService;

		// Token: 0x0400002C RID: 44
		public readonly ILoc _loc;
	}
}
