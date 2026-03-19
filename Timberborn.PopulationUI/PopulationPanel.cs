using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.GameDistricts;
using Timberborn.Population;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.PopulationUI
{
	// Token: 0x0200000B RID: 11
	public class PopulationPanel : ILoadableSingleton
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000026A4 File Offset: 0x000008A4
		public PopulationPanel(VisualElementLoader visualElementLoader, UILayout uiLayout, PopulationService populationService, DistrictContextService districtContextService, EventBus eventBus, IBatchControlBox batchControlBox, PopulationDataRowFactory populationDataRowFactory, HousingDataRowFactory housingDataRowFactory, WorkplaceDataRowFactory workplaceDataRowFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._populationService = populationService;
			this._districtContextService = districtContextService;
			this._eventBus = eventBus;
			this._batchControlBox = batchControlBox;
			this._populationDataRowFactory = populationDataRowFactory;
			this._housingDataRowFactory = housingDataRowFactory;
			this._workplaceDataRowFactory = workplaceDataRowFactory;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026FC File Offset: 0x000008FC
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/Population/PopulationPanel");
			this.AddPopulationRow();
			this.AddHousingRow();
			this.AddBeaverWorkplaceRow();
			this.AddBotWorkplaceRow();
			this._eventBus.Register(this);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002738 File Offset: 0x00000938
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopLeft(this._root, 2);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000274C File Offset: 0x0000094C
		[OnEvent]
		public void OnPopulationChangedEvent(PopulationChangedEvent populationChangedEvent)
		{
			this.UpdateCounters();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000274C File Offset: 0x0000094C
		[OnEvent]
		public void OnDistrictSelected(DistrictSelectedEvent districtSelectedEvent)
		{
			this.UpdateCounters();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000274C File Offset: 0x0000094C
		[OnEvent]
		public void OnDistrictUnselected(DistrictUnselectedEvent districtUnselectedEvent)
		{
			this.UpdateCounters();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002754 File Offset: 0x00000954
		public void AddPopulationRow()
		{
			Button button = UQueryExtensions.Q<Button>(this._root, "PopulationData", null);
			this._populationDataRow = this._populationDataRowFactory.Create(button, new Func<PopulationData>(this.GetContextualPopulationData));
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._batchControlBox.OpenCharactersTab();
			}, 0);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027A4 File Offset: 0x000009A4
		public void AddHousingRow()
		{
			Button button = UQueryExtensions.Q<Button>(this._root, "HousingData", null);
			this._housingDataRow = this._housingDataRowFactory.Create(button, new Func<PopulationData>(this.GetContextualPopulationData));
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._batchControlBox.OpenHousingTab();
			}, 0);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027F4 File Offset: 0x000009F4
		public void AddBeaverWorkplaceRow()
		{
			Button button = UQueryExtensions.Q<Button>(this._root, "BeaverWorkplaceData", null);
			this._beaverWorkplaceData = this._workplaceDataRowFactory.CreateBeaverWorkplaceDataRow(button, new Func<PopulationData>(this.GetContextualPopulationData));
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._batchControlBox.OpenWorkplacesTab();
			}, 0);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002844 File Offset: 0x00000A44
		public void AddBotWorkplaceRow()
		{
			this._botWorkplaceDataButton = UQueryExtensions.Q<Button>(this._root, "BotWorkplaceData", null);
			this._botWorkplaceData = this._workplaceDataRowFactory.CreateBotWorkplaceDataRow(this._botWorkplaceDataButton, new Func<PopulationData>(this.GetContextualPopulationData));
			this._botWorkplaceDataButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._batchControlBox.OpenWorkplacesTab();
			}, 0);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000028A3 File Offset: 0x00000AA3
		public PopulationData GetContextualPopulationData()
		{
			if (!this._districtContextService.SelectedDistrict)
			{
				return this._populationService.GlobalPopulationData;
			}
			return this._populationService.DistrictPopulationData;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000028D0 File Offset: 0x00000AD0
		public void UpdateCounters()
		{
			this._populationDataRow.UpdateData();
			this._housingDataRow.UpdateData();
			this._beaverWorkplaceData.UpdateData();
			this._botWorkplaceData.UpdateData();
			bool botCreated = this._populationService.BotCreated;
			this._botWorkplaceDataButton.ToggleDisplayStyle(botCreated);
			this._root.EnableInClassList(PopulationPanel.PanelDistrictClass, this._districtContextService.SelectedDistrict);
		}

		// Token: 0x04000025 RID: 37
		public static readonly string PanelDistrictClass = "panel--district";

		// Token: 0x04000026 RID: 38
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000027 RID: 39
		public readonly UILayout _uiLayout;

		// Token: 0x04000028 RID: 40
		public readonly PopulationService _populationService;

		// Token: 0x04000029 RID: 41
		public readonly DistrictContextService _districtContextService;

		// Token: 0x0400002A RID: 42
		public readonly EventBus _eventBus;

		// Token: 0x0400002B RID: 43
		public readonly IBatchControlBox _batchControlBox;

		// Token: 0x0400002C RID: 44
		public readonly PopulationDataRowFactory _populationDataRowFactory;

		// Token: 0x0400002D RID: 45
		public readonly HousingDataRowFactory _housingDataRowFactory;

		// Token: 0x0400002E RID: 46
		public readonly WorkplaceDataRowFactory _workplaceDataRowFactory;

		// Token: 0x0400002F RID: 47
		public VisualElement _root;

		// Token: 0x04000030 RID: 48
		public PopulationDataRow _populationDataRow;

		// Token: 0x04000031 RID: 49
		public HousingDataRow _housingDataRow;

		// Token: 0x04000032 RID: 50
		public WorkplaceDataRow _beaverWorkplaceData;

		// Token: 0x04000033 RID: 51
		public WorkplaceDataRow _botWorkplaceData;

		// Token: 0x04000034 RID: 52
		public Button _botWorkplaceDataButton;
	}
}
