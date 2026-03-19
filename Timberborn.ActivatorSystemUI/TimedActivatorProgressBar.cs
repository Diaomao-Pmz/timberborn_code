using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.ActivatorSystemUI
{
	// Token: 0x02000006 RID: 6
	public class TimedActivatorProgressBar
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002154 File Offset: 0x00000354
		public TimedActivatorProgressBar(ILoc loc, Label label, ProgressBar progressBar, Func<float> progressGetter, Func<string> daysLeftGetter, Func<bool> countdownActiveGetter)
		{
			this._loc = loc;
			this._label = label;
			this._progressBar = progressBar;
			this._progressGetter = progressGetter;
			this._daysLeftGetter = daysLeftGetter;
			this._countdownActiveGetter = countdownActiveGetter;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002189 File Offset: 0x00000389
		public void Initialize(string progressActiveLabelLocKey, string progressNotActiveLabelLocKey, bool isHazardousActivator)
		{
			this._progressActiveLabelLocKey = progressActiveLabelLocKey;
			this._progressNotActiveLabelLocKey = progressNotActiveLabelLocKey;
			this._progressBar.EnableInClassList(TimedActivatorProgressBar.HazardousActivatorUssClass, isHazardousActivator);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021AC File Offset: 0x000003AC
		public void UpdateState()
		{
			if (this._countdownActiveGetter())
			{
				this._progressBar.SetProgress(this._progressGetter());
				this._label.text = this._loc.T<string>(this._progressActiveLabelLocKey, this._daysLeftGetter());
				return;
			}
			this._label.text = this._loc.T(this._progressNotActiveLabelLocKey);
			this._progressBar.SetProgress(0f);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string HazardousActivatorUssClass = "progress-bar--red";

		// Token: 0x04000009 RID: 9
		public readonly ILoc _loc;

		// Token: 0x0400000A RID: 10
		public readonly Label _label;

		// Token: 0x0400000B RID: 11
		public readonly ProgressBar _progressBar;

		// Token: 0x0400000C RID: 12
		public readonly Func<float> _progressGetter;

		// Token: 0x0400000D RID: 13
		public readonly Func<string> _daysLeftGetter;

		// Token: 0x0400000E RID: 14
		public readonly Func<bool> _countdownActiveGetter;

		// Token: 0x0400000F RID: 15
		public string _progressActiveLabelLocKey;

		// Token: 0x04000010 RID: 16
		public string _progressNotActiveLabelLocKey;
	}
}
