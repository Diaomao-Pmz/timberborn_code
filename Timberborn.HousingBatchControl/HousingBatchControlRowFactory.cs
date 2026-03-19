using System;
using Timberborn.AutomationUI;
using Timberborn.BatchControl;
using Timberborn.BuildingsUI;
using Timberborn.ConstructionSitesUI;
using Timberborn.CoreUI;
using Timberborn.DwellingSystemUI;
using Timberborn.EntitySystem;
using Timberborn.HaulingUI;
using Timberborn.ReproductionUI;
using Timberborn.StatusSystemUI;

namespace Timberborn.HousingBatchControl
{
	// Token: 0x02000006 RID: 6
	public class HousingBatchControlRowFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000211C File Offset: 0x0000031C
		public HousingBatchControlRowFactory(VisualElementLoader visualElementLoader, BuildingBatchControlRowItemFactory buildingBatchControlRowItemFactory, DwellingBatchControlRowItemFactory dwellingBatchControlRowItemFactory, HaulCandidateBatchControlRowItemFactory haulCandidateBatchControlRowItemFactory, BreedingPodBatchControlRowItemFactory breedingPodBatchControlRowItemFactory, StatusBatchControlRowItemFactory statusBatchControlRowItemFactory, ConstructionSitePriorityBatchControlRowItemFactory constructionSitePriorityBatchControlRowItemFactory, BreedingPodInventoryBatchControlRowItemFactory breedingPodInventoryBatchControlRowItemFactory, AutomatableBatchControlRowItemFactory automatableBatchControlRowItemFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._buildingBatchControlRowItemFactory = buildingBatchControlRowItemFactory;
			this._dwellingBatchControlRowItemFactory = dwellingBatchControlRowItemFactory;
			this._haulCandidateBatchControlRowItemFactory = haulCandidateBatchControlRowItemFactory;
			this._breedingPodBatchControlRowItemFactory = breedingPodBatchControlRowItemFactory;
			this._statusBatchControlRowItemFactory = statusBatchControlRowItemFactory;
			this._constructionSitePriorityBatchControlRowItemFactory = constructionSitePriorityBatchControlRowItemFactory;
			this._breedingPodInventoryBatchControlRowItemFactory = breedingPodInventoryBatchControlRowItemFactory;
			this._automatableBatchControlRowItemFactory = automatableBatchControlRowItemFactory;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002174 File Offset: 0x00000374
		public BatchControlRow Create(EntityComponent entity)
		{
			return new BatchControlRow(this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlRow"), entity, new IBatchControlRowItem[]
			{
				this._buildingBatchControlRowItemFactory.Create(entity),
				this._dwellingBatchControlRowItemFactory.Create(entity),
				this._haulCandidateBatchControlRowItemFactory.Create(entity),
				this._breedingPodBatchControlRowItemFactory.Create(entity),
				this._breedingPodInventoryBatchControlRowItemFactory.Create(entity),
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
		public readonly DwellingBatchControlRowItemFactory _dwellingBatchControlRowItemFactory;

		// Token: 0x0400000A RID: 10
		public readonly HaulCandidateBatchControlRowItemFactory _haulCandidateBatchControlRowItemFactory;

		// Token: 0x0400000B RID: 11
		public readonly BreedingPodBatchControlRowItemFactory _breedingPodBatchControlRowItemFactory;

		// Token: 0x0400000C RID: 12
		public readonly StatusBatchControlRowItemFactory _statusBatchControlRowItemFactory;

		// Token: 0x0400000D RID: 13
		public readonly ConstructionSitePriorityBatchControlRowItemFactory _constructionSitePriorityBatchControlRowItemFactory;

		// Token: 0x0400000E RID: 14
		public readonly BreedingPodInventoryBatchControlRowItemFactory _breedingPodInventoryBatchControlRowItemFactory;

		// Token: 0x0400000F RID: 15
		public readonly AutomatableBatchControlRowItemFactory _automatableBatchControlRowItemFactory;
	}
}
