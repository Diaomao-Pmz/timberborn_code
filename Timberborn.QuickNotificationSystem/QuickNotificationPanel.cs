using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.QuickNotificationSystem
{
	// Token: 0x02000008 RID: 8
	public class QuickNotificationPanel : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002124 File Offset: 0x00000324
		public QuickNotificationPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, QuickNotificationService quickNotificationService, ISpecService specService)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._quickNotificationService = quickNotificationService;
			this._specService = specService;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000214C File Offset: 0x0000034C
		public void Load()
		{
			this._quickNotificationSpec = this._specService.GetSingleSpec<QuickNotificationSpec>();
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/QuickNotificationPanel");
			this._alert = UQueryExtensions.Q<Label>(visualElement, "Alert", null);
			this._alert.ToggleDisplayStyle(false);
			this._uiLayout.AddAbsoluteItem(visualElement);
			this._quickNotificationService.AlertSent += this.OnNotificationSent;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021BC File Offset: 0x000003BC
		public void UpdateSingleton()
		{
			float unscaledTime = Time.unscaledTime;
			float? sendTime = this._sendTime;
			if (unscaledTime > sendTime.GetValueOrDefault() & sendTime != null)
			{
				this._sendTime = null;
				this._hideTime = Time.unscaledTime + this._duration;
				this._alert.ToggleDisplayStyle(true);
			}
			if (Time.unscaledTime > this._hideTime)
			{
				this._alert.ToggleDisplayStyle(false);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222C File Offset: 0x0000042C
		public void OnNotificationSent(object sender, QuickNotificationEventArgs e)
		{
			this._alert.text = e.Text;
			if (e.IsWarning)
			{
				this._alert.AddToClassList(QuickNotificationPanel.WarningClass);
				this._alert.RemoveFromClassList(QuickNotificationPanel.NormalClass);
			}
			else
			{
				this._alert.AddToClassList(QuickNotificationPanel.NormalClass);
				this._alert.RemoveFromClassList(QuickNotificationPanel.WarningClass);
			}
			this._duration = (e.IsWarning ? this._quickNotificationSpec.ExtendedDuration : this._quickNotificationSpec.Duration);
			this._sendTime = new float?(Time.unscaledTime);
		}

		// Token: 0x0400000A RID: 10
		public static readonly string WarningClass = "square-large--red";

		// Token: 0x0400000B RID: 11
		public static readonly string NormalClass = "square-large--green";

		// Token: 0x0400000C RID: 12
		public readonly UILayout _uiLayout;

		// Token: 0x0400000D RID: 13
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000E RID: 14
		public readonly QuickNotificationService _quickNotificationService;

		// Token: 0x0400000F RID: 15
		public readonly ISpecService _specService;

		// Token: 0x04000010 RID: 16
		public QuickNotificationSpec _quickNotificationSpec;

		// Token: 0x04000011 RID: 17
		public float _hideTime;

		// Token: 0x04000012 RID: 18
		public Label _alert;

		// Token: 0x04000013 RID: 19
		public float _duration;

		// Token: 0x04000014 RID: 20
		public float? _sendTime;
	}
}
