using System;
using Bindito.Core;
using Timberborn.BatchControl;

namespace Timberborn.PowerBatchControl
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class PowerBatchControlConfigurator : Configurator
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000253B File Offset: 0x0000073B
		public override void Configure()
		{
			base.Bind<MechanicalBatchControlTab>().AsSingleton();
			base.Bind<MechanicalBatchControlRowFactory>().AsSingleton();
			base.MultiBind<BatchControlModule>().ToProvider<PowerBatchControlConfigurator.BatchControlModuleProvider>().AsSingleton();
		}

		// Token: 0x02000009 RID: 9
		public class BatchControlModuleProvider : IProvider<BatchControlModule>
		{
			// Token: 0x06000020 RID: 32 RVA: 0x0000256E File Offset: 0x0000076E
			public BatchControlModuleProvider(MechanicalBatchControlTab mechanicalBatchControlTab)
			{
				this._mechanicalBatchControlTab = mechanicalBatchControlTab;
			}

			// Token: 0x06000021 RID: 33 RVA: 0x0000257D File Offset: 0x0000077D
			public BatchControlModule Get()
			{
				BatchControlModule.Builder builder = new BatchControlModule.Builder();
				builder.AddTab(this._mechanicalBatchControlTab, 5);
				return builder.Build();
			}

			// Token: 0x04000018 RID: 24
			public readonly MechanicalBatchControlTab _mechanicalBatchControlTab;
		}
	}
}
