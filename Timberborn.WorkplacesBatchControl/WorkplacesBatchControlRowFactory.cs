using System;
using Timberborn.AutomationUI;
using Timberborn.BatchControl;
using Timberborn.BuildingsUI;
using Timberborn.ConstructionSitesUI;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.FieldsUI;
using Timberborn.ForestryUI;
using Timberborn.GatheringUI;
using Timberborn.HaulingUI;
using Timberborn.PlantingUI;
using Timberborn.StatusSystemUI;
using Timberborn.WorkshopsUI;
using Timberborn.WorkSystemUI;

namespace Timberborn.WorkplacesBatchControl
{
	// Token: 0x02000006 RID: 6
	public class WorkplacesBatchControlRowFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000211C File Offset: 0x0000031C
		public WorkplacesBatchControlRowFactory(VisualElementLoader visualElementLoader, BuildingBatchControlRowItemFactory buildingBatchControlRowItemFactory, WorkplacePriorityBatchControlRowItemFactory workplacePriorityBatchControlRowItemFactory, WorkplaceBatchControlRowItemFactory workplaceBatchControlRowItemFactory, PlantablePrioritizerBatchControlRowItemFactory plantablePrioritizerBatchControlRowItemFactory, FarmHouseBatchControlRowItemFactory farmHouseBatchControlRowItemFactory, GatherablePrioritizerBatchControlRowItemFactory gatherablePrioritizerBatchControlRowItemFactory, ManufactoryBatchControlRowItemFactory manufactoryBatchControlRowItemFactory, HaulCandidateBatchControlRowItemFactory haulCandidateBatchControlRowItemFactory, ConstructionSitePriorityBatchControlRowItemFactory constructionSitePriorityBatchControlRowItemFactory, StatusBatchControlRowItemFactory statusBatchControlRowItemFactory, WorkplaceWorkerTypeBatchControlRowItemFactory workplaceWorkerTypeBatchControlRowItemFactory, ProductivityBatchControlRowItemFactory productivityBatchControlRowItemFactory, ManufactoryTogglableRecipesBatchControlRowItemFactory manufactoryTogglableRecipesBatchControlRowItemFactory, ForesterBatchControlRowItemFactory foresterBatchControlRowItemFactory, AutomatableBatchControlRowItemFactory automatableBatchControlRowItemFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._buildingBatchControlRowItemFactory = buildingBatchControlRowItemFactory;
			this._workplacePriorityBatchControlRowItemFactory = workplacePriorityBatchControlRowItemFactory;
			this._workplaceBatchControlRowItemFactory = workplaceBatchControlRowItemFactory;
			this._plantablePrioritizerBatchControlRowItemFactory = plantablePrioritizerBatchControlRowItemFactory;
			this._farmHouseBatchControlRowItemFactory = farmHouseBatchControlRowItemFactory;
			this._gatherablePrioritizerBatchControlRowItemFactory = gatherablePrioritizerBatchControlRowItemFactory;
			this._manufactoryBatchControlRowItemFactory = manufactoryBatchControlRowItemFactory;
			this._haulCandidateBatchControlRowItemFactory = haulCandidateBatchControlRowItemFactory;
			this._constructionSitePriorityBatchControlRowItemFactory = constructionSitePriorityBatchControlRowItemFactory;
			this._statusBatchControlRowItemFactory = statusBatchControlRowItemFactory;
			this._workplaceWorkerTypeBatchControlRowItemFactory = workplaceWorkerTypeBatchControlRowItemFactory;
			this._productivityBatchControlRowItemFactory = productivityBatchControlRowItemFactory;
			this._manufactoryTogglableRecipesBatchControlRowItemFactory = manufactoryTogglableRecipesBatchControlRowItemFactory;
			this._foresterBatchControlRowItemFactory = foresterBatchControlRowItemFactory;
			this._automatableBatchControlRowItemFactory = automatableBatchControlRowItemFactory;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021AC File Offset: 0x000003AC
		public BatchControlRow Create(EntityComponent entity)
		{
			return new BatchControlRow(this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlRow"), entity, new IBatchControlRowItem[]
			{
				this._buildingBatchControlRowItemFactory.Create(entity),
				this._workplacePriorityBatchControlRowItemFactory.Create(entity),
				this._workplaceWorkerTypeBatchControlRowItemFactory.Create(entity),
				this._workplaceBatchControlRowItemFactory.Create(entity),
				this._haulCandidateBatchControlRowItemFactory.Create(entity),
				this._productivityBatchControlRowItemFactory.Create(entity),
				this._farmHouseBatchControlRowItemFactory.Create(entity),
				this._plantablePrioritizerBatchControlRowItemFactory.Create(entity),
				this._foresterBatchControlRowItemFactory.Create(entity),
				this._gatherablePrioritizerBatchControlRowItemFactory.Create(entity),
				this._manufactoryBatchControlRowItemFactory.Create(entity),
				this._manufactoryTogglableRecipesBatchControlRowItemFactory.Create(entity),
				this._constructionSitePriorityBatchControlRowItemFactory.Create(entity),
				this._automatableBatchControlRowItemFactory.Create(entity),
				this._statusBatchControlRowItemFactory.Create(entity)
			});
		}

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public readonly BuildingBatchControlRowItemFactory _buildingBatchControlRowItemFactory;

		// Token: 0x04000009 RID: 9
		public readonly WorkplacePriorityBatchControlRowItemFactory _workplacePriorityBatchControlRowItemFactory;

		// Token: 0x0400000A RID: 10
		public readonly WorkplaceBatchControlRowItemFactory _workplaceBatchControlRowItemFactory;

		// Token: 0x0400000B RID: 11
		public readonly PlantablePrioritizerBatchControlRowItemFactory _plantablePrioritizerBatchControlRowItemFactory;

		// Token: 0x0400000C RID: 12
		public readonly FarmHouseBatchControlRowItemFactory _farmHouseBatchControlRowItemFactory;

		// Token: 0x0400000D RID: 13
		public readonly GatherablePrioritizerBatchControlRowItemFactory _gatherablePrioritizerBatchControlRowItemFactory;

		// Token: 0x0400000E RID: 14
		public readonly ManufactoryBatchControlRowItemFactory _manufactoryBatchControlRowItemFactory;

		// Token: 0x0400000F RID: 15
		public readonly HaulCandidateBatchControlRowItemFactory _haulCandidateBatchControlRowItemFactory;

		// Token: 0x04000010 RID: 16
		public readonly ConstructionSitePriorityBatchControlRowItemFactory _constructionSitePriorityBatchControlRowItemFactory;

		// Token: 0x04000011 RID: 17
		public readonly StatusBatchControlRowItemFactory _statusBatchControlRowItemFactory;

		// Token: 0x04000012 RID: 18
		public readonly WorkplaceWorkerTypeBatchControlRowItemFactory _workplaceWorkerTypeBatchControlRowItemFactory;

		// Token: 0x04000013 RID: 19
		public readonly ProductivityBatchControlRowItemFactory _productivityBatchControlRowItemFactory;

		// Token: 0x04000014 RID: 20
		public readonly ManufactoryTogglableRecipesBatchControlRowItemFactory _manufactoryTogglableRecipesBatchControlRowItemFactory;

		// Token: 0x04000015 RID: 21
		public readonly ForesterBatchControlRowItemFactory _foresterBatchControlRowItemFactory;

		// Token: 0x04000016 RID: 22
		public readonly AutomatableBatchControlRowItemFactory _automatableBatchControlRowItemFactory;
	}
}
