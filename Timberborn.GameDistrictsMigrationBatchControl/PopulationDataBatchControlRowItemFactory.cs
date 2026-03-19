using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.GameDistricts;
using Timberborn.Population;
using Timberborn.PopulationUI;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x0200001B RID: 27
	public class PopulationDataBatchControlRowItemFactory
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000034DA File Offset: 0x000016DA
		public PopulationDataBatchControlRowItemFactory(HousingDataRowFactory housingDataRowFactory, PopulationDataCollector populationDataCollector, VisualElementLoader visualElementLoader, WorkplaceDataRowFactory workplaceDataRowFactory)
		{
			this._housingDataRowFactory = housingDataRowFactory;
			this._populationDataCollector = populationDataCollector;
			this._visualElementLoader = visualElementLoader;
			this._workplaceDataRowFactory = workplaceDataRowFactory;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003500 File Offset: 0x00001700
		public IBatchControlRowItem CreateHousingDataRowItem(DistrictCenter districtCenter)
		{
			VisualElement visualElement = this.CreateRoot();
			PopulationData populationData = this.CreatePopulationData(districtCenter);
			HousingDataRow housingDataRow = this._housingDataRowFactory.Create(UQueryExtensions.Q<VisualElement>(visualElement, "Content", null), () => populationData);
			housingDataRow.UpdateData();
			return new PopulationDataBatchControlRowItem(this._populationDataCollector, visualElement, districtCenter, populationData, housingDataRow);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003568 File Offset: 0x00001768
		public IBatchControlRowItem CreateBeaverWorkplaceRowItem(DistrictCenter districtCenter)
		{
			VisualElement visualElement = this.CreateRoot();
			PopulationData populationData = this.CreatePopulationData(districtCenter);
			WorkplaceDataRow workplaceDataRow = this._workplaceDataRowFactory.CreateBeaverWorkplaceDataRow(UQueryExtensions.Q<VisualElement>(visualElement, "Content", null), () => populationData);
			workplaceDataRow.UpdateData();
			return new PopulationDataBatchControlRowItem(this._populationDataCollector, visualElement, districtCenter, populationData, workplaceDataRow);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000035D0 File Offset: 0x000017D0
		public IBatchControlRowItem CreateBotWorkplaceRowItem(DistrictCenter districtCenter)
		{
			VisualElement visualElement = this.CreateRoot();
			PopulationData populationData = this.CreatePopulationData(districtCenter);
			WorkplaceDataRow workplaceDataRow = this._workplaceDataRowFactory.CreateBotWorkplaceDataRow(UQueryExtensions.Q<VisualElement>(visualElement, "Content", null), () => populationData);
			workplaceDataRow.UpdateData();
			return new PopulationDataBatchControlRowItem(this._populationDataCollector, visualElement, districtCenter, populationData, workplaceDataRow);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003638 File Offset: 0x00001838
		public VisualElement CreateRoot()
		{
			string elementName = "Game/BatchControl/PopulationDataBatchControlRowItem";
			return this._visualElementLoader.LoadVisualElement(elementName);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003658 File Offset: 0x00001858
		public PopulationData CreatePopulationData(DistrictCenter districtCenter)
		{
			PopulationData populationData = new PopulationData();
			this._populationDataCollector.CollectData(districtCenter, populationData);
			return populationData;
		}

		// Token: 0x0400006E RID: 110
		public readonly HousingDataRowFactory _housingDataRowFactory;

		// Token: 0x0400006F RID: 111
		public readonly PopulationDataCollector _populationDataCollector;

		// Token: 0x04000070 RID: 112
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000071 RID: 113
		public readonly WorkplaceDataRowFactory _workplaceDataRowFactory;
	}
}
