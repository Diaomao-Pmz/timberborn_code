using System;
using Bindito.Core;
using Timberborn.BatchControl;

namespace Timberborn.AttractionsBatchControl
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class AttractionsBatchControlConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<AttractionsBatchControlTab>().AsSingleton();
			base.Bind<AttractionsBatchControlRowFactory>().AsSingleton();
			base.Bind<GoodConsumingAttractionBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<BatchControlModule>().ToProvider<AttractionsBatchControlConfigurator.BatchControlModuleProvider>().AsSingleton();
		}

		// Token: 0x02000005 RID: 5
		public class BatchControlModuleProvider : IProvider<BatchControlModule>
		{
			// Token: 0x06000005 RID: 5 RVA: 0x000020FD File Offset: 0x000002FD
			public BatchControlModuleProvider(AttractionsBatchControlTab attractionsBatchControlTab)
			{
				this._attractionsBatchControlTab = attractionsBatchControlTab;
			}

			// Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
			public BatchControlModule Get()
			{
				BatchControlModule.Builder builder = new BatchControlModule.Builder();
				builder.AddTab(this._attractionsBatchControlTab, 6);
				return builder.Build();
			}

			// Token: 0x04000006 RID: 6
			public readonly AttractionsBatchControlTab _attractionsBatchControlTab;
		}
	}
}
