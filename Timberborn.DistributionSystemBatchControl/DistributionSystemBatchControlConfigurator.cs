using System;
using Bindito.Core;
using Timberborn.BatchControl;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class DistributionSystemBatchControlConfigurator : Configurator
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002574 File Offset: 0x00000774
		public override void Configure()
		{
			base.Bind<DistributionBatchControlRowGroupFactory>().AsSingleton();
			base.Bind<DistributionBatchControlTab>().AsSingleton();
			base.Bind<DistributionSettingGroupFactory>().AsSingleton();
			base.Bind<DistributionSettingsRowItemFactory>().AsSingleton();
			base.Bind<DistrictDistributionControlRowItemFactory>().AsSingleton();
			base.Bind<GoodDistributionSettingItemFactory>().AsSingleton();
			base.Bind<ImportToggleFactory>().AsSingleton();
			base.Bind<ExportThresholdSliderFactory>().AsSingleton();
			base.MultiBind<BatchControlModule>().ToProvider<DistributionSystemBatchControlConfigurator.BatchControlModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000B RID: 11
		public class BatchControlModuleProvider : IProvider<BatchControlModule>
		{
			// Token: 0x06000021 RID: 33 RVA: 0x000025FA File Offset: 0x000007FA
			public BatchControlModuleProvider(DistributionBatchControlTab distributionBatchControlTab)
			{
				this._distributionBatchControlTab = distributionBatchControlTab;
			}

			// Token: 0x06000022 RID: 34 RVA: 0x00002609 File Offset: 0x00000809
			public BatchControlModule Get()
			{
				BatchControlModule.Builder builder = new BatchControlModule.Builder();
				builder.AddTab(this._distributionBatchControlTab, 8);
				return builder.Build();
			}

			// Token: 0x04000019 RID: 25
			public readonly DistributionBatchControlTab _distributionBatchControlTab;
		}
	}
}
