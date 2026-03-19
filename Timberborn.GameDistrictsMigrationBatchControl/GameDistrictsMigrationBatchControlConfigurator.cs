using System;
using Bindito.Core;
using Timberborn.BatchControl;
using Timberborn.GameDistrictsMigration;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class GameDistrictsMigrationBatchControlConfigurator : Configurator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002224 File Offset: 0x00000424
		public override void Configure()
		{
			base.Bind<CurrentPopulationBatchControlRowItemFactory>().AsSingleton();
			base.Bind<DistrictMigrationSetterRowItemFactory>().AsSingleton();
			base.Bind<ManualMigrationBlocker>().AsSingleton();
			base.Bind<ManualMigrationDistrictColumnFactory>().AsSingleton();
			base.Bind<ManualMigrationDistrictSetter>().AsSingleton();
			base.Bind<ManualMigrationPanelFactory>().AsSingleton();
			base.Bind<ManualMigrationPopulationRowFactory>().AsSingleton();
			base.Bind<MigrationBatchControlRowFactory>().AsSingleton();
			base.Bind<MigrationBatchControlRowGroupFactory>().AsSingleton();
			base.Bind<MigrationBatchControlTab>().AsSingleton();
			base.Bind<PopulationDataBatchControlRowItemFactory>().AsSingleton();
			base.Bind<PopulationDistributorBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<BatchControlModule>().ToProvider<GameDistrictsMigrationBatchControlConfigurator.BatchControlModuleProvider>().AsSingleton();
		}

		// Token: 0x02000009 RID: 9
		public class BatchControlModuleProvider : IProvider<BatchControlModule>
		{
			// Token: 0x0600000F RID: 15 RVA: 0x000022DA File Offset: 0x000004DA
			public BatchControlModuleProvider(MigrationBatchControlTab migrationBatchControlTab)
			{
				this._migrationBatchControlTab = migrationBatchControlTab;
			}

			// Token: 0x06000010 RID: 16 RVA: 0x000022E9 File Offset: 0x000004E9
			public BatchControlModule Get()
			{
				BatchControlModule.Builder builder = new BatchControlModule.Builder();
				builder.AddTab(this._migrationBatchControlTab, 7);
				return builder.Build();
			}

			// Token: 0x0400000E RID: 14
			public readonly MigrationBatchControlTab _migrationBatchControlTab;
		}
	}
}
