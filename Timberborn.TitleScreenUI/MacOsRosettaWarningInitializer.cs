using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.TitleScreenUI
{
	// Token: 0x02000005 RID: 5
	public class MacOsRosettaWarningInitializer : ILoadableSingleton
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000211D File Offset: 0x0000031D
		public MacOsRosettaWarningInitializer(ILoc loc, TitleScreenFooter titleScreenFooter)
		{
			this._loc = loc;
			this._titleScreenFooter = titleScreenFooter;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002134 File Offset: 0x00000334
		public void Load()
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this._titleScreenFooter.Root, "MacOsRosettaWarningContainer", null);
			if (ProcessorInfo.IsAppleCpu() && ProcessorInfo.IsIntelProcess())
			{
				UQueryExtensions.Q<Label>(visualElement, "MacOsRosettaWarning", null).text = this.GetWarningText();
				return;
			}
			visualElement.ToggleDisplayStyle(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002185 File Offset: 0x00000385
		public string GetWarningText()
		{
			return this._loc.T(MacOsRosettaWarningInitializer.RosettaOnLocKey) + " " + this._loc.T(MacOsRosettaWarningInitializer.RosettaPerformanceLocKey);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string RosettaOnLocKey = "MainMenu.RosettaOn";

		// Token: 0x04000009 RID: 9
		public static readonly string RosettaPerformanceLocKey = "MainMenu.RosettaPerformanceWarning";

		// Token: 0x0400000A RID: 10
		public readonly ILoc _loc;

		// Token: 0x0400000B RID: 11
		public readonly TitleScreenFooter _titleScreenFooter;
	}
}
