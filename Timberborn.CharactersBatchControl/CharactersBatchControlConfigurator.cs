using System;
using Bindito.Core;
using Timberborn.BatchControl;

namespace Timberborn.CharactersBatchControl
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class CharactersBatchControlConfigurator : Configurator
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000275F File Offset: 0x0000095F
		public override void Configure()
		{
			base.Bind<CharacterBatchControlTab>().AsSingleton();
			base.Bind<CharacterBatchControlRowFactory>().AsSingleton();
			base.MultiBind<BatchControlModule>().ToProvider<CharactersBatchControlConfigurator.BatchControlModuleProvider>().AsSingleton();
		}

		// Token: 0x02000009 RID: 9
		public class BatchControlModuleProvider : IProvider<BatchControlModule>
		{
			// Token: 0x06000027 RID: 39 RVA: 0x00002792 File Offset: 0x00000992
			public BatchControlModuleProvider(CharacterBatchControlTab characterBatchControlTab)
			{
				this._characterBatchControlTab = characterBatchControlTab;
			}

			// Token: 0x06000028 RID: 40 RVA: 0x000027A1 File Offset: 0x000009A1
			public BatchControlModule Get()
			{
				BatchControlModule.Builder builder = new BatchControlModule.Builder();
				builder.AddTab(this._characterBatchControlTab, 1);
				return builder.Build();
			}

			// Token: 0x04000020 RID: 32
			public readonly CharacterBatchControlTab _characterBatchControlTab;
		}
	}
}
