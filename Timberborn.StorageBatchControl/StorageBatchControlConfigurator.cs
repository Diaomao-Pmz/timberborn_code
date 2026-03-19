using System;
using Bindito.Core;
using Timberborn.BatchControl;

namespace Timberborn.StorageBatchControl
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class StorageBatchControlConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<StorageBatchControlTab>().AsSingleton();
			base.Bind<StorageBatchControlRowFactory>().AsSingleton();
			base.MultiBind<BatchControlModule>().ToProvider<StorageBatchControlConfigurator.BatchControlModuleProvider>().AsSingleton();
		}

		// Token: 0x02000005 RID: 5
		public class BatchControlModuleProvider : IProvider<BatchControlModule>
		{
			// Token: 0x06000005 RID: 5 RVA: 0x000020F1 File Offset: 0x000002F1
			public BatchControlModuleProvider(StorageBatchControlTab storageBatchControlTab)
			{
				this._storageBatchControlTab = storageBatchControlTab;
			}

			// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
			public BatchControlModule Get()
			{
				BatchControlModule.Builder builder = new BatchControlModule.Builder();
				builder.AddTab(this._storageBatchControlTab, 4);
				return builder.Build();
			}

			// Token: 0x04000006 RID: 6
			public readonly StorageBatchControlTab _storageBatchControlTab;
		}
	}
}
