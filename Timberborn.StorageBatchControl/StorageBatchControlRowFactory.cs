using System;
using Timberborn.AutomationUI;
using Timberborn.BatchControl;
using Timberborn.BuildingsUI;
using Timberborn.ConstructionSitesUI;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.HaulingUI;
using Timberborn.StatusSystemUI;
using Timberborn.StockpilePriorityUISystem;
using Timberborn.StockpilesUI;

namespace Timberborn.StorageBatchControl
{
	// Token: 0x02000006 RID: 6
	public class StorageBatchControlRowFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000211C File Offset: 0x0000031C
		public StorageBatchControlRowFactory(VisualElementLoader visualElementLoader, BuildingBatchControlRowItemFactory buildingBatchControlRowItemFactory, ConstructionSitePriorityBatchControlRowItemFactory constructionSitePriorityBatchControlRowItemFactory, StockpileBatchControlRowItemFactory stockpileBatchControlRowItemFactory, HaulCandidateBatchControlRowItemFactory haulCandidateBatchControlRowItemFactory, StatusBatchControlRowItemFactory statusBatchControlRowItemFactory, StockpilePriorityBatchControlRowItemFactory stockpilePriorityBatchControlRowItemFactory, AutomatableBatchControlRowItemFactory automatableBatchControlRowItemFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._buildingBatchControlRowItemFactory = buildingBatchControlRowItemFactory;
			this._stockpileBatchControlRowItemFactory = stockpileBatchControlRowItemFactory;
			this._constructionSitePriorityBatchControlRowItemFactory = constructionSitePriorityBatchControlRowItemFactory;
			this._haulCandidateBatchControlRowItemFactory = haulCandidateBatchControlRowItemFactory;
			this._statusBatchControlRowItemFactory = statusBatchControlRowItemFactory;
			this._stockpilePriorityBatchControlRowItemFactory = stockpilePriorityBatchControlRowItemFactory;
			this._automatableBatchControlRowItemFactory = automatableBatchControlRowItemFactory;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000216C File Offset: 0x0000036C
		public BatchControlRow Create(EntityComponent entity)
		{
			return new BatchControlRow(this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlRow"), entity, new IBatchControlRowItem[]
			{
				this._buildingBatchControlRowItemFactory.Create(entity),
				this._stockpileBatchControlRowItemFactory.Create(entity),
				this._haulCandidateBatchControlRowItemFactory.Create(entity),
				this._stockpilePriorityBatchControlRowItemFactory.Create(entity),
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
		public readonly ConstructionSitePriorityBatchControlRowItemFactory _constructionSitePriorityBatchControlRowItemFactory;

		// Token: 0x0400000A RID: 10
		public readonly StockpileBatchControlRowItemFactory _stockpileBatchControlRowItemFactory;

		// Token: 0x0400000B RID: 11
		public readonly HaulCandidateBatchControlRowItemFactory _haulCandidateBatchControlRowItemFactory;

		// Token: 0x0400000C RID: 12
		public readonly StockpilePriorityBatchControlRowItemFactory _stockpilePriorityBatchControlRowItemFactory;

		// Token: 0x0400000D RID: 13
		public readonly AutomatableBatchControlRowItemFactory _automatableBatchControlRowItemFactory;

		// Token: 0x0400000E RID: 14
		public readonly StatusBatchControlRowItemFactory _statusBatchControlRowItemFactory;
	}
}
