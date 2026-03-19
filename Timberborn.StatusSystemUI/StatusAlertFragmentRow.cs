using System;
using Timberborn.CoreUI;
using Timberborn.StatusSystem;
using UnityEngine.UIElements;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x02000006 RID: 6
	public class StatusAlertFragmentRow
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000024A9 File Offset: 0x000006A9
		public string AlertDescription { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000024B1 File Offset: 0x000006B1
		public string WarningSound { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000024B9 File Offset: 0x000006B9
		public VisualElement Root { get; }

		// Token: 0x06000011 RID: 17 RVA: 0x000024C1 File Offset: 0x000006C1
		public StatusAlertFragmentRow(AlertStatusSubjectSelector alertStatusSubjectSelector, string alertDescription, string warningSound, VisualElement root, Button statusButton)
		{
			this._alertStatusSubjectSelector = alertStatusSubjectSelector;
			this.AlertDescription = alertDescription;
			this.WarningSound = warningSound;
			this.Root = root;
			this._statusButton = statusButton;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024EE File Offset: 0x000006EE
		public void Initialize()
		{
			this._statusButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SelectNext), 0);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002508 File Offset: 0x00000708
		public void UpdateRowState(int count, string value = null)
		{
			if (this._previousCount != count || this._previousValue != value)
			{
				this._statusButton.text = this.GetAlertText(count, value);
				this.Root.ToggleDisplayStyle(count > 0);
				this._previousCount = count;
				this._previousValue = value;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000255C File Offset: 0x0000075C
		public void ToggleHighlight()
		{
			this.ChangeHighlightState(!this._highlightActive);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000256D File Offset: 0x0000076D
		public void DisableHighlight()
		{
			this.ChangeHighlightState(false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002576 File Offset: 0x00000776
		public void SelectNext(ClickEvent evt)
		{
			this._alertStatusSubjectSelector.SelectNextSubject(this.AlertDescription);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000258C File Offset: 0x0000078C
		public string GetAlertText(int count, string value)
		{
			string text = (value != null) ? string.Format(this.AlertDescription, value) : this.AlertDescription;
			string result;
			if (count <= 1)
			{
				if ((result = text) == null)
				{
					return "";
				}
			}
			else
			{
				result = string.Format("{0} ({1})", text, count);
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025D1 File Offset: 0x000007D1
		public void ChangeHighlightState(bool active)
		{
			this._highlightActive = active;
			this.Root.EnableInClassList(StatusAlertFragmentRow.BlinkingClass, active);
		}

		// Token: 0x04000018 RID: 24
		public static readonly string BlinkingClass = "alert-panel-row--blink";

		// Token: 0x0400001C RID: 28
		public readonly AlertStatusSubjectSelector _alertStatusSubjectSelector;

		// Token: 0x0400001D RID: 29
		public readonly Button _statusButton;

		// Token: 0x0400001E RID: 30
		public int _previousCount;

		// Token: 0x0400001F RID: 31
		public string _previousValue;

		// Token: 0x04000020 RID: 32
		public bool _highlightActive;
	}
}
