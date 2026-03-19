using System;
using Timberborn.Analytics;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000005 RID: 5
	public class AnalyticsSettingsController
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002126 File Offset: 0x00000326
		public AnalyticsSettingsController(AnalyticsConsent analyticsConsent)
		{
			this._analyticsConsent = analyticsConsent;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002135 File Offset: 0x00000335
		public void Initialize(VisualElement root)
		{
			this._analyticsEnabled = UQueryExtensions.Q<Toggle>(root, "AnalyticsEnabled", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._analyticsEnabled, delegate(ChangeEvent<bool> v)
			{
				this.ToggleConsent(v.newValue);
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002161 File Offset: 0x00000361
		public void Update()
		{
			this._analyticsEnabled.SetValueWithoutNotify(this._analyticsConsent.IsConsentGiven);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002179 File Offset: 0x00000379
		public void ToggleConsent(bool newValue)
		{
			if (newValue)
			{
				this._analyticsConsent.GiveConsent();
				return;
			}
			this._analyticsConsent.RemoveConsent();
		}

		// Token: 0x04000008 RID: 8
		public readonly AnalyticsConsent _analyticsConsent;

		// Token: 0x04000009 RID: 9
		public Toggle _analyticsEnabled;
	}
}
