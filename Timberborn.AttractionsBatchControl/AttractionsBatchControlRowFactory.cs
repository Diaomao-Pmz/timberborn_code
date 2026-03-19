using System;
using Timberborn.AttractionsUI;
using Timberborn.AutomationUI;
using Timberborn.BatchControl;
using Timberborn.BuildingsUI;
using Timberborn.ConstructionSitesUI;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.HaulingUI;
using Timberborn.StatusSystemUI;

namespace Timberborn.AttractionsBatchControl
{
	// Token: 0x02000006 RID: 6
	public class AttractionsBatchControlRowFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
		public AttractionsBatchControlRowFactory(VisualElementLoader visualElementLoader, BuildingBatchControlRowItemFactory buildingBatchControlRowItemFactory, StatusBatchControlRowItemFactory statusBatchControlRowItemFactory, ConstructionSitePriorityBatchControlRowItemFactory constructionSitePriorityBatchControlRowItemFactory, AttractionBatchControlRowItemFactory attractionBatchControlRowItemFactory, AttractionLoadRateBatchControlRowItemFactory attractionLoadRateBatchControlRowItemFactory, GoodConsumingAttractionBatchControlRowItemFactory goodConsumingAttractionBatchControlRowItemFactory, HaulCandidateBatchControlRowItemFactory haulCandidateBatchControlRowItemFactory, AutomatableBatchControlRowItemFactory automatableBatchControlRowItemFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._buildingBatchControlRowItemFactory = buildingBatchControlRowItemFactory;
			this._statusBatchControlRowItemFactory = statusBatchControlRowItemFactory;
			this._constructionSitePriorityBatchControlRowItemFactory = constructionSitePriorityBatchControlRowItemFactory;
			this._attractionBatchControlRowItemFactory = attractionBatchControlRowItemFactory;
			this._attractionLoadRateBatchControlRowItemFactory = attractionLoadRateBatchControlRowItemFactory;
			this._goodConsumingAttractionBatchControlRowItemFactory = goodConsumingAttractionBatchControlRowItemFactory;
			this._haulCandidateBatchControlRowItemFactory = haulCandidateBatchControlRowItemFactory;
			this._automatableBatchControlRowItemFactory = automatableBatchControlRowItemFactory;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002180 File Offset: 0x00000380
		public BatchControlRow Create(EntityComponent entity)
		{
			return new BatchControlRow(this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlRow"), entity, new IBatchControlRowItem[]
			{
				this._buildingBatchControlRowItemFactory.Create(entity),
				this._attractionBatchControlRowItemFactory.Create(entity),
				this._attractionLoadRateBatchControlRowItemFactory.Create(entity),
				this._haulCandidateBatchControlRowItemFactory.Create(entity),
				this._constructionSitePriorityBatchControlRowItemFactory.Create(entity),
				this._goodConsumingAttractionBatchControlRowItemFactory.Create(entity),
				this._automatableBatchControlRowItemFactory.Create(entity),
				this._statusBatchControlRowItemFactory.Create(entity)
			});
		}

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public readonly BuildingBatchControlRowItemFactory _buildingBatchControlRowItemFactory;

		// Token: 0x04000009 RID: 9
		public readonly StatusBatchControlRowItemFactory _statusBatchControlRowItemFactory;

		// Token: 0x0400000A RID: 10
		public readonly ConstructionSitePriorityBatchControlRowItemFactory _constructionSitePriorityBatchControlRowItemFactory;

		// Token: 0x0400000B RID: 11
		public readonly AttractionBatchControlRowItemFactory _attractionBatchControlRowItemFactory;

		// Token: 0x0400000C RID: 12
		public readonly AttractionLoadRateBatchControlRowItemFactory _attractionLoadRateBatchControlRowItemFactory;

		// Token: 0x0400000D RID: 13
		public readonly GoodConsumingAttractionBatchControlRowItemFactory _goodConsumingAttractionBatchControlRowItemFactory;

		// Token: 0x0400000E RID: 14
		public readonly HaulCandidateBatchControlRowItemFactory _haulCandidateBatchControlRowItemFactory;

		// Token: 0x0400000F RID: 15
		public readonly AutomatableBatchControlRowItemFactory _automatableBatchControlRowItemFactory;
	}
}
