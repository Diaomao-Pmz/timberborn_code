using System;
using Timberborn.AlertPanelSystem;
using Timberborn.StatusSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x02000007 RID: 7
	public class StatusAlertFragmentRowFactory
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000025F7 File Offset: 0x000007F7
		public StatusAlertFragmentRowFactory(AlertPanelRowFactory alertPanelRowFactory)
		{
			this._alertPanelRowFactory = alertPanelRowFactory;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002608 File Offset: 0x00000808
		public StatusAlertFragmentRow Create(string alert, Sprite sprite, AlertStatusSubjectSelector subjectSelector, string warningSound = null)
		{
			VisualElement visualElement = this._alertPanelRowFactory.Create(sprite);
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = sprite;
			Button statusButton = UQueryExtensions.Q<Button>(visualElement, "Button", null);
			StatusAlertFragmentRow statusAlertFragmentRow = new StatusAlertFragmentRow(subjectSelector, alert, warningSound, visualElement, statusButton);
			statusAlertFragmentRow.Initialize();
			return statusAlertFragmentRow;
		}

		// Token: 0x04000021 RID: 33
		public readonly AlertPanelRowFactory _alertPanelRowFactory;
	}
}
