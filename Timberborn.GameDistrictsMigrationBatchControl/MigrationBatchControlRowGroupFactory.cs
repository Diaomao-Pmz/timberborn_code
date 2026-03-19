using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsBatchControl;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000017 RID: 23
	public class MigrationBatchControlRowGroupFactory
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000030B8 File Offset: 0x000012B8
		public MigrationBatchControlRowGroupFactory(DistrictCenterRowItemFactory districtCenterRowItemFactory, DistrictMigrationSetterRowItemFactory districtMigrationSetterRowItemFactory, MigrationBatchControlRowFactory migrationBatchControlRowFactory, PopulationDataBatchControlRowItemFactory populationDataBatchControlRowItemFactory, VisualElementLoader visualElementLoader, BatchControlRowGroupFactory batchControlRowGroupFactory)
		{
			this._districtCenterRowItemFactory = districtCenterRowItemFactory;
			this._districtMigrationSetterRowItemFactory = districtMigrationSetterRowItemFactory;
			this._migrationBatchControlRowFactory = migrationBatchControlRowFactory;
			this._populationDataBatchControlRowItemFactory = populationDataBatchControlRowItemFactory;
			this._visualElementLoader = visualElementLoader;
			this._batchControlRowGroupFactory = batchControlRowGroupFactory;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030F0 File Offset: 0x000012F0
		public BatchControlRowGroup Create(DistrictCenter districtCenter)
		{
			BatchControlRowGroup batchControlRowGroup = this.CreateBatchControlRowGroup(districtCenter);
			batchControlRowGroup.AddRow(this._migrationBatchControlRowFactory.CreateAdultRow(districtCenter));
			batchControlRowGroup.AddRow(this._migrationBatchControlRowFactory.CreateChildRow(districtCenter));
			batchControlRowGroup.AddRow(this._migrationBatchControlRowFactory.CreateContaminatedRow(districtCenter));
			batchControlRowGroup.AddRow(this._migrationBatchControlRowFactory.CreateBotRow(districtCenter));
			batchControlRowGroup.Root.AddToClassList(MigrationBatchControlRowGroupFactory.MarginBottomClass);
			batchControlRowGroup.UpdateVisibleRows(districtCenter);
			return batchControlRowGroup;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003164 File Offset: 0x00001364
		public BatchControlRowGroup CreateBatchControlRowGroup(DistrictCenter districtCenter)
		{
			string elementName = "Game/BatchControl/BatchControlRow";
			VisualElement root = this._visualElementLoader.LoadVisualElement(elementName);
			IBatchControlRowItem batchControlRowItem = this._districtCenterRowItemFactory.Create(districtCenter);
			IBatchControlRowItem batchControlRowItem2 = this._districtMigrationSetterRowItemFactory.Create(districtCenter);
			IBatchControlRowItem batchControlRowItem3 = this._populationDataBatchControlRowItemFactory.CreateHousingDataRowItem(districtCenter);
			EntityComponent component = districtCenter.GetComponent<EntityComponent>();
			return this._batchControlRowGroupFactory.CreateUnsorted(new BatchControlRow(root, component, new IBatchControlRowItem[]
			{
				batchControlRowItem,
				batchControlRowItem2,
				batchControlRowItem3
			}));
		}

		// Token: 0x04000056 RID: 86
		public static readonly string MarginBottomClass = "migration-batch-control-row-group__margin-bottom";

		// Token: 0x04000057 RID: 87
		public readonly DistrictCenterRowItemFactory _districtCenterRowItemFactory;

		// Token: 0x04000058 RID: 88
		public readonly DistrictMigrationSetterRowItemFactory _districtMigrationSetterRowItemFactory;

		// Token: 0x04000059 RID: 89
		public readonly MigrationBatchControlRowFactory _migrationBatchControlRowFactory;

		// Token: 0x0400005A RID: 90
		public readonly PopulationDataBatchControlRowItemFactory _populationDataBatchControlRowItemFactory;

		// Token: 0x0400005B RID: 91
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400005C RID: 92
		public readonly BatchControlRowGroupFactory _batchControlRowGroupFactory;
	}
}
