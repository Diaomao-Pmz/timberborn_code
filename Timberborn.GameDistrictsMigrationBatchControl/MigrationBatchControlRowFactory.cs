using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsMigration;
using Timberborn.Population;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000016 RID: 22
	public class MigrationBatchControlRowFactory
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002E6A File Offset: 0x0000106A
		public MigrationBatchControlRowFactory(CurrentPopulationBatchControlRowItemFactory currentPopulationBatchControlRowItemFactory, PopulationDataBatchControlRowItemFactory populationDataBatchControlRowItemFactory, PopulationDistributorBatchControlRowItemFactory populationDistributorBatchControlRowItemFactory, PopulationDistributorRetriever populationDistributorRetriever, VisualElementLoader visualElementLoader, PopulationService populationService)
		{
			this._currentPopulationBatchControlRowItemFactory = currentPopulationBatchControlRowItemFactory;
			this._populationDataBatchControlRowItemFactory = populationDataBatchControlRowItemFactory;
			this._populationDistributorBatchControlRowItemFactory = populationDistributorBatchControlRowItemFactory;
			this._populationDistributorRetriever = populationDistributorRetriever;
			this._visualElementLoader = visualElementLoader;
			this._populationService = populationService;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EA0 File Offset: 0x000010A0
		public BatchControlRow CreateAdultRow(DistrictCenter districtCenter)
		{
			VisualElement root = this.CreateRoot();
			PopulationDistributor populationDistributor = this._populationDistributorRetriever.GetPopulationDistributor<AdultsDistributorTemplate>(districtCenter);
			return new BatchControlRow(root, populationDistributor.DistrictCenter.GetComponent<EntityComponent>(), new IBatchControlRowItem[]
			{
				this._currentPopulationBatchControlRowItemFactory.Create(populationDistributor, MigrationBatchControlRowFactory.AdultIcon),
				this._populationDistributorBatchControlRowItemFactory.Create(populationDistributor),
				this._populationDataBatchControlRowItemFactory.CreateBeaverWorkplaceRowItem(populationDistributor.DistrictCenter)
			});
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F10 File Offset: 0x00001110
		public BatchControlRow CreateChildRow(DistrictCenter districtCenter)
		{
			VisualElement root = this.CreateRoot();
			PopulationDistributor populationDistributor = this._populationDistributorRetriever.GetPopulationDistributor<ChildrenDistributorTemplate>(districtCenter);
			return new BatchControlRow(root, populationDistributor.DistrictCenter.GetComponent<EntityComponent>(), new IBatchControlRowItem[]
			{
				this._currentPopulationBatchControlRowItemFactory.Create(populationDistributor, MigrationBatchControlRowFactory.ChildIcon),
				this._populationDistributorBatchControlRowItemFactory.Create(populationDistributor)
			});
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F6C File Offset: 0x0000116C
		public BatchControlRow CreateBotRow(DistrictCenter districtCenter)
		{
			VisualElement root = this.CreateRoot();
			PopulationDistributor populationDistributor = this._populationDistributorRetriever.GetPopulationDistributor<BotsDistributorTemplate>(districtCenter);
			return new BatchControlRow(root, populationDistributor.DistrictCenter.GetComponent<EntityComponent>(), () => this._populationService.BotCreated, new IBatchControlRowItem[]
			{
				this._currentPopulationBatchControlRowItemFactory.Create(populationDistributor, MigrationBatchControlRowFactory.BotIcon),
				this._populationDistributorBatchControlRowItemFactory.Create(populationDistributor),
				this._populationDataBatchControlRowItemFactory.CreateBotWorkplaceRowItem(populationDistributor.DistrictCenter)
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FE8 File Offset: 0x000011E8
		public BatchControlRow CreateContaminatedRow(DistrictCenter districtCenter)
		{
			VisualElement root = this.CreateRoot();
			PopulationDistributor populationDistributor = this._populationDistributorRetriever.GetPopulationDistributor<ContaminatedDistributorTemplate>(districtCenter);
			return new BatchControlRow(root, populationDistributor.DistrictCenter.GetComponent<EntityComponent>(), () => this._populationService.IsAnyoneContaminated, new IBatchControlRowItem[]
			{
				this._currentPopulationBatchControlRowItemFactory.Create(populationDistributor, MigrationBatchControlRowFactory.ContaminatedIcon),
				this._populationDistributorBatchControlRowItemFactory.Create(populationDistributor)
			});
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000304D File Offset: 0x0000124D
		public VisualElement CreateRoot()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlRow");
			visualElement.AddToClassList(MigrationBatchControlRowFactory.MarginLeftClass);
			return visualElement;
		}

		// Token: 0x0400004B RID: 75
		public static readonly string AdultIcon = "population-counter__icon--adult";

		// Token: 0x0400004C RID: 76
		public static readonly string ChildIcon = "population-counter__icon--child";

		// Token: 0x0400004D RID: 77
		public static readonly string BotIcon = "population-counter__icon--bot";

		// Token: 0x0400004E RID: 78
		public static readonly string ContaminatedIcon = "population-counter__icon--contamination";

		// Token: 0x0400004F RID: 79
		public static readonly string MarginLeftClass = "migration-batch-control-row__margin-left";

		// Token: 0x04000050 RID: 80
		public readonly CurrentPopulationBatchControlRowItemFactory _currentPopulationBatchControlRowItemFactory;

		// Token: 0x04000051 RID: 81
		public readonly PopulationDataBatchControlRowItemFactory _populationDataBatchControlRowItemFactory;

		// Token: 0x04000052 RID: 82
		public readonly PopulationDistributorBatchControlRowItemFactory _populationDistributorBatchControlRowItemFactory;

		// Token: 0x04000053 RID: 83
		public readonly PopulationDistributorRetriever _populationDistributorRetriever;

		// Token: 0x04000054 RID: 84
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000055 RID: 85
		public readonly PopulationService _populationService;
	}
}
