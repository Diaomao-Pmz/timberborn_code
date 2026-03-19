using System;
using System.Collections.Generic;
using Timberborn.AlertPanelSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.StatusSystem;
using UnityEngine.UIElements;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x02000005 RID: 5
	public class StatusAlertFragment : IAlertFragment
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002374 File Offset: 0x00000574
		public StatusAlertFragment(StatusAggregator statusAggregator, EventBus eventBus, EntitySelectionService entitySelectionService, StatusAlertFragmentRowFactory statusAlertFragmentRowFactory)
		{
			this._statusAggregator = statusAggregator;
			this._eventBus = eventBus;
			this._entitySelectionService = entitySelectionService;
			this._statusAlertFragmentRowFactory = statusAlertFragmentRowFactory;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023A4 File Offset: 0x000005A4
		public void InitializeAlertFragment(VisualElement root)
		{
			this._root = new VisualElement
			{
				name = "StatusAlertFragment"
			};
			root.Add(this._root);
			this._alertStatusSubjectSelector = new AlertStatusSubjectSelector(this._statusAggregator, this._entitySelectionService);
			this._eventBus.Register(this);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023F8 File Offset: 0x000005F8
		public void UpdateAlertFragment()
		{
			foreach (StatusAlertFragmentRow statusAlertFragmentRow in this._rows)
			{
				int visibleStatusesCount = this._statusAggregator.GetVisibleStatusesCount(statusAlertFragmentRow.AlertDescription);
				statusAlertFragmentRow.UpdateRowState(visibleStatusesCount, null);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002460 File Offset: 0x00000660
		[OnEvent]
		public void OnStatusAlertAddedEvent(StatusAlertAddedEvent statusAlertAddedEvent)
		{
			StatusAlertFragmentRow statusAlertFragmentRow = this._statusAlertFragmentRowFactory.Create(statusAlertAddedEvent.StatusAlert, statusAlertAddedEvent.StatusSprite, this._alertStatusSubjectSelector, null);
			this._rows.Add(statusAlertFragmentRow);
			this._root.Add(statusAlertFragmentRow.Root);
		}

		// Token: 0x04000011 RID: 17
		public readonly StatusAggregator _statusAggregator;

		// Token: 0x04000012 RID: 18
		public readonly EventBus _eventBus;

		// Token: 0x04000013 RID: 19
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000014 RID: 20
		public readonly StatusAlertFragmentRowFactory _statusAlertFragmentRowFactory;

		// Token: 0x04000015 RID: 21
		public AlertStatusSubjectSelector _alertStatusSubjectSelector;

		// Token: 0x04000016 RID: 22
		public readonly List<StatusAlertFragmentRow> _rows = new List<StatusAlertFragmentRow>();

		// Token: 0x04000017 RID: 23
		public VisualElement _root;
	}
}
