using System;
using Timberborn.Automation;
using Timberborn.Illumination;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.AutomationUI
{
	// Token: 0x0200000D RID: 13
	public class AutomationStateIcon
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000026EE File Offset: 0x000008EE
		public AutomationStateIcon(Func<Automator> automatorGetter, Image icon)
		{
			this._automatorGetter = automatorGetter;
			this._icon = icon;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002704 File Offset: 0x00000904
		public void Update()
		{
			Automator automator = this._automatorGetter();
			if (automator != null)
			{
				this._icon.visible = true;
				this._icon.EnableInClassList(AutomationStateIcon.StateOnClass, automator.UnfinishedState == AutomatorState.On);
				this._icon.EnableInClassList(AutomationStateIcon.StateUnfinishedClass, !automator.Enabled);
				this._icon.style.unityBackgroundImageTintColor = this.GetColor(automator);
				return;
			}
			this._icon.visible = false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002787 File Offset: 0x00000987
		public Color GetColor(Automator automator)
		{
			return automator.GetComponent<CustomizableIlluminator>().IconColor;
		}

		// Token: 0x0400001E RID: 30
		public static readonly string StateOnClass = "automation-state-icon--on";

		// Token: 0x0400001F RID: 31
		public static readonly string StateUnfinishedClass = "automation-state-icon--unfinished";

		// Token: 0x04000020 RID: 32
		public readonly Func<Automator> _automatorGetter;

		// Token: 0x04000021 RID: 33
		public readonly Image _icon;
	}
}
