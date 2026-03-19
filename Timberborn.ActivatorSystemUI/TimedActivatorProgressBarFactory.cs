using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.ActivatorSystemUI
{
	// Token: 0x02000007 RID: 7
	public class TimedActivatorProgressBarFactory
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000223C File Offset: 0x0000043C
		public TimedActivatorProgressBarFactory(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000224C File Offset: 0x0000044C
		public TimedActivatorProgressBar Create(VisualElement root, Func<float> progressGetter, Func<string> daysLeftGetter, Func<bool> countdownActiveGetter)
		{
			Label label = UQueryExtensions.Q<Label>(root, "Text", null);
			ProgressBar progressBar = UQueryExtensions.Q<ProgressBar>(root, "ProgressBar", null);
			return new TimedActivatorProgressBar(this._loc, label, progressBar, progressGetter, daysLeftGetter, countdownActiveGetter);
		}

		// Token: 0x04000011 RID: 17
		public readonly ILoc _loc;
	}
}
