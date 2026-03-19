using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AutomationBuildings;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000031 RID: 49
	public class TimerModeDescriptions : ILoadableSingleton
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00006C0D File Offset: 0x00004E0D
		public TimerModeDescriptions(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006C28 File Offset: 0x00004E28
		public void Load()
		{
			string str = this._loc.T<string>(TimerModeDescriptions.ResetsWhenResetInputLocKey, this._loc.T(TimerModeDescriptions.ResetInputLocKey));
			string param = this._loc.T(TimerModeDescriptions.InputALocKey);
			string param2 = this._loc.T(TimerModeDescriptions.TimeALocKey);
			string param3 = this._loc.T(TimerModeDescriptions.TimeBLocKey);
			foreach (TimerMode timerMode in Enum.GetValues(typeof(TimerMode)).Cast<TimerMode>())
			{
				string key = string.Format("{0}{1}{2}", TimerModeDescriptions.DescriptionLocKeyPrefix, timerMode, TimerModeDescriptions.DescriptionLocKeyPostfix);
				this._dictionary.Add(timerMode, this._loc.T<string, string, string>(key, param, param2, param3) + "\n" + str);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006D1C File Offset: 0x00004F1C
		public string GetDescription(TimerMode timerMode)
		{
			return this._dictionary[timerMode];
		}

		// Token: 0x0400016E RID: 366
		public static readonly string DescriptionLocKeyPrefix = "Building.Timer.Mode.";

		// Token: 0x0400016F RID: 367
		public static readonly string DescriptionLocKeyPostfix = ".Description";

		// Token: 0x04000170 RID: 368
		public static readonly string InputALocKey = "Automation.Input.A";

		// Token: 0x04000171 RID: 369
		public static readonly string ResetInputLocKey = "Automation.Input.Reset";

		// Token: 0x04000172 RID: 370
		public static readonly string ResetsWhenResetInputLocKey = "Automation.ResetsWhenResetInput";

		// Token: 0x04000173 RID: 371
		public static readonly string TimeALocKey = "Building.Timer.TimeA";

		// Token: 0x04000174 RID: 372
		public static readonly string TimeBLocKey = "Building.Timer.TimeB";

		// Token: 0x04000175 RID: 373
		public readonly Dictionary<TimerMode, string> _dictionary = new Dictionary<TimerMode, string>();

		// Token: 0x04000176 RID: 374
		public readonly ILoc _loc;
	}
}
