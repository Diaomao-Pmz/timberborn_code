using System;
using Timberborn.Bots;
using Timberborn.Localization;
using Timberborn.Wellbeing;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000021 RID: 33
	public class WellbeingNameHelper
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00004832 File Offset: 0x00002A32
		public WellbeingNameHelper(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004841 File Offset: 0x00002A41
		public string GetWellbeingName(WellbeingTracker wellbeingTracker)
		{
			return this._loc.T(wellbeingTracker.HasComponent<BotSpec>() ? WellbeingNameHelper.ConditionLocKey : WellbeingNameHelper.WellbeingLocKey);
		}

		// Token: 0x040000AA RID: 170
		public static readonly string WellbeingLocKey = "Wellbeing.DisplayName";

		// Token: 0x040000AB RID: 171
		public static readonly string ConditionLocKey = "Condition.DisplayName";

		// Token: 0x040000AC RID: 172
		public readonly ILoc _loc;
	}
}
