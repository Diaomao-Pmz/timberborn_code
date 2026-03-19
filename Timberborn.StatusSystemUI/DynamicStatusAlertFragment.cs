using System;
using System.Collections.Generic;
using Timberborn.AlertPanelSystem;
using Timberborn.GameSound;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.StatusSystem;
using UnityEngine.UIElements;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x02000004 RID: 4
	public class DynamicStatusAlertFragment : IAlertFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public DynamicStatusAlertFragment(DynamicStatusAggregator dynamicStatusAggregator, GameUISoundController gameUISoundController, StatusAlertRowBlinker statusAlertRowBlinker, EventBus eventBus, EntitySelectionService entitySelectionService, StatusAlertFragmentRowFactory statusAlertFragmentRowFactory)
		{
			this._dynamicStatusAggregator = dynamicStatusAggregator;
			this._gameUISoundController = gameUISoundController;
			this._statusAlertRowBlinker = statusAlertRowBlinker;
			this._eventBus = eventBus;
			this._entitySelectionService = entitySelectionService;
			this._statusAlertFragmentRowFactory = statusAlertFragmentRowFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002124 File Offset: 0x00000324
		public void InitializeAlertFragment(VisualElement root)
		{
			this._root = new VisualElement
			{
				name = "DynamicStatusAlertFragment"
			};
			root.Add(this._root);
			this._alertStatusSubjectSelector = new AlertStatusSubjectSelector(this._dynamicStatusAggregator, this._entitySelectionService);
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002178 File Offset: 0x00000378
		public void UpdateAlertFragment()
		{
			foreach (StatusAlertFragmentRow statusAlertFragmentRow in this._rows)
			{
				string alertDescription = statusAlertFragmentRow.AlertDescription;
				StatusData statusData;
				if (this._dynamicStatusAggregator.TryGetStatusData(alertDescription, out statusData))
				{
					string format = (statusData.StatusWarningType != StatusWarningType.None) ? "F1" : "F0";
					statusAlertFragmentRow.UpdateRowState(statusData.Count, statusData.Value.ToString(format));
					this.UpdateBlinkingState(statusData, alertDescription, statusAlertFragmentRow);
				}
				else
				{
					statusAlertFragmentRow.UpdateRowState(0, null);
					this._statusAlertRowBlinker.StopBlinking(statusAlertFragmentRow);
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002230 File Offset: 0x00000430
		[OnEvent]
		public void OnStatusAlertAddedEvent(DynamicStatusAlertAddedEvent statusAlertAddedEvent)
		{
			StatusInstance statusInstance = statusAlertAddedEvent.StatusInstance;
			StatusAlertFragmentRow statusAlertFragmentRow = this._statusAlertFragmentRowFactory.Create(statusInstance.AlertDescription, statusInstance.IconSmall, this._alertStatusSubjectSelector, statusInstance.WarningSound);
			this._rows.Add(statusAlertFragmentRow);
			this._root.Add(statusAlertFragmentRow.Root);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002288 File Offset: 0x00000488
		public void UpdateBlinkingState(StatusData statusData, string alertDescription, StatusAlertFragmentRow row)
		{
			switch (statusData.StatusWarningType)
			{
			case StatusWarningType.None:
				this._statusAlertRowBlinker.StopBlinking(row);
				return;
			case StatusWarningType.Short:
				if (DynamicStatusAlertFragment.ShouldStartBlinking(alertDescription, statusData.Value, this._blinkingRow))
				{
					this.PlaySound(row.WarningSound);
					this._statusAlertRowBlinker.StartShortBlinking(row);
				}
				this._blinkingRow[alertDescription] = statusData.Value;
				return;
			case StatusWarningType.Infinite:
				if (DynamicStatusAlertFragment.ShouldStartBlinking(alertDescription, statusData.Value, this._infiniteBlinkingRow))
				{
					this.PlaySound(row.WarningSound);
					this._statusAlertRowBlinker.StartInfiniteBlinking(row);
				}
				this._infiniteBlinkingRow[alertDescription] = statusData.Value;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002340 File Offset: 0x00000540
		public static bool ShouldStartBlinking(string alertDescription, float value, Dictionary<string, float> blinkingRow)
		{
			float num;
			return !blinkingRow.TryGetValue(alertDescription, out num) || value > num;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000235E File Offset: 0x0000055E
		public void PlaySound(string warningSound)
		{
			if (!string.IsNullOrEmpty(warningSound))
			{
				this._gameUISoundController.PlaySound2D(warningSound);
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly DynamicStatusAggregator _dynamicStatusAggregator;

		// Token: 0x04000007 RID: 7
		public readonly GameUISoundController _gameUISoundController;

		// Token: 0x04000008 RID: 8
		public readonly StatusAlertRowBlinker _statusAlertRowBlinker;

		// Token: 0x04000009 RID: 9
		public readonly EventBus _eventBus;

		// Token: 0x0400000A RID: 10
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400000B RID: 11
		public readonly StatusAlertFragmentRowFactory _statusAlertFragmentRowFactory;

		// Token: 0x0400000C RID: 12
		public AlertStatusSubjectSelector _alertStatusSubjectSelector;

		// Token: 0x0400000D RID: 13
		public readonly List<StatusAlertFragmentRow> _rows = new List<StatusAlertFragmentRow>();

		// Token: 0x0400000E RID: 14
		public readonly Dictionary<string, float> _blinkingRow = new Dictionary<string, float>();

		// Token: 0x0400000F RID: 15
		public readonly Dictionary<string, float> _infiniteBlinkingRow = new Dictionary<string, float>();

		// Token: 0x04000010 RID: 16
		public VisualElement _root;
	}
}
