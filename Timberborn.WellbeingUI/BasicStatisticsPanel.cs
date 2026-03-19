using System;
using Timberborn.BatchControl;
using Timberborn.GameDistricts;
using Timberborn.InputSystem;
using Timberborn.Population;
using Timberborn.ScienceSystem;
using Timberborn.SingletonSystem;
using Timberborn.UIFormatters;
using Timberborn.UILayoutSystem;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000007 RID: 7
	public class BasicStatisticsPanel : ILoadableSingleton, IUpdatableSingleton, IInputProcessor
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public BasicStatisticsPanel(UILayout uiLayout, WellbeingService wellbeingService, DistrictContextService districtContextService, ScienceService scienceService, IBatchControlBox batchControlBox, InputService inputService, PopulationService populationService, BasicStatisticsPanelFactory basicStatisticsPanelFactory, EventBus eventBus)
		{
			this._uiLayout = uiLayout;
			this._wellbeingService = wellbeingService;
			this._districtContextService = districtContextService;
			this._scienceService = scienceService;
			this._batchControlBox = batchControlBox;
			this._inputService = inputService;
			this._populationService = populationService;
			this._basicStatisticsPanelFactory = basicStatisticsPanelFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002158 File Offset: 0x00000358
		public void Load()
		{
			this._root = this._basicStatisticsPanelFactory.Create();
			this._wellbeingButton = UQueryExtensions.Q<Button>(this._root, "Wellbeing", null);
			this._wellbeingCount = UQueryExtensions.Q<Label>(this._root, "WellbeingCount", null);
			this._scienceCount = UQueryExtensions.Q<Label>(this._root, "ScienceCount", null);
			this._eventBus.Register(this);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C7 File Offset: 0x000003C7
		public void UpdateSingleton()
		{
			this.UpdateWellbeing();
			this.UpdateScience();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021D5 File Offset: 0x000003D5
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(BasicStatisticsPanel.ToggleBatchControlBoxKey))
			{
				this._batchControlBox.OpenBatchControlBox();
				return true;
			}
			return false;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021F7 File Offset: 0x000003F7
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._inputService.AddInputProcessor(this);
			this._uiLayout.AddTopLeft(this._root, 1);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002217 File Offset: 0x00000417
		public void ToggleWellbeingButtonHighlight(bool state)
		{
			this._wellbeingButton.EnableInClassList(BasicStatisticsPanel.HighlightClass, state);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222C File Offset: 0x0000042C
		public void UpdateWellbeing()
		{
			this._wellbeingButton.RemoveFromClassList(BasicStatisticsPanel.BeaversPerishedClass);
			this._wellbeingButton.RemoveFromClassList(BasicStatisticsPanel.AllPerishedClass);
			if (this._populationService.OnlyBotsAlive)
			{
				this._wellbeingCount.text = "";
				this._wellbeingButton.AddToClassList(BasicStatisticsPanel.BeaversPerishedClass);
				this._wellbeingButton.RemoveFromClassList(BasicStatisticsPanel.NegativeWellbeingClass);
				return;
			}
			if (this._populationService.AllDead)
			{
				this._wellbeingCount.text = "";
				this._wellbeingButton.AddToClassList(BasicStatisticsPanel.AllPerishedClass);
				this._wellbeingButton.AddToClassList(BasicStatisticsPanel.NegativeWellbeingClass);
				return;
			}
			int num = this._districtContextService.SelectedDistrict ? this._wellbeingService.AverageDistrictWellbeing : this._wellbeingService.AverageGlobalWellbeing;
			this._wellbeingCount.text = num.ToString();
			this._wellbeingButton.EnableInClassList(BasicStatisticsPanel.NegativeWellbeingClass, num < 0);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002326 File Offset: 0x00000526
		public void UpdateScience()
		{
			this._scienceCount.text = NumberFormatter.Format(this._scienceService.SciencePoints);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string BeaversPerishedClass = "basic-statistics__bot";

		// Token: 0x04000009 RID: 9
		public static readonly string AllPerishedClass = "basic-statistics__death";

		// Token: 0x0400000A RID: 10
		public static readonly string NegativeWellbeingClass = "basic-statistics__negative";

		// Token: 0x0400000B RID: 11
		public static readonly string HighlightClass = "highlight";

		// Token: 0x0400000C RID: 12
		public static readonly string ToggleBatchControlBoxKey = "ToggleBatchControlBox";

		// Token: 0x0400000D RID: 13
		public readonly UILayout _uiLayout;

		// Token: 0x0400000E RID: 14
		public readonly WellbeingService _wellbeingService;

		// Token: 0x0400000F RID: 15
		public readonly DistrictContextService _districtContextService;

		// Token: 0x04000010 RID: 16
		public readonly ScienceService _scienceService;

		// Token: 0x04000011 RID: 17
		public readonly IBatchControlBox _batchControlBox;

		// Token: 0x04000012 RID: 18
		public readonly InputService _inputService;

		// Token: 0x04000013 RID: 19
		public readonly PopulationService _populationService;

		// Token: 0x04000014 RID: 20
		public readonly BasicStatisticsPanelFactory _basicStatisticsPanelFactory;

		// Token: 0x04000015 RID: 21
		public readonly EventBus _eventBus;

		// Token: 0x04000016 RID: 22
		public Button _wellbeingButton;

		// Token: 0x04000017 RID: 23
		public Label _wellbeingCount;

		// Token: 0x04000018 RID: 24
		public Label _scienceCount;

		// Token: 0x04000019 RID: 25
		public VisualElement _root;
	}
}
