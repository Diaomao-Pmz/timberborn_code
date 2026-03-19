using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.NaturalResourcesLifecycleUI
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	[Context("MapEditor")]
	public class NaturalResourcesLifecycleUIConfigurator : Configurator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002363 File Offset: 0x00000563
		public override void Configure()
		{
			base.Bind<DyingNaturalResourceFragment>().AsSingleton();
			base.Bind<DeadNaturalResourceDescriber>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<NaturalResourcesLifecycleUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000007 RID: 7
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000F RID: 15 RVA: 0x00002396 File Offset: 0x00000596
			public EntityPanelModuleProvider(DyingNaturalResourceFragment dyingNaturalResourceFragment)
			{
				this._dyingNaturalResourceFragment = dyingNaturalResourceFragment;
			}

			// Token: 0x06000010 RID: 16 RVA: 0x000023A5 File Offset: 0x000005A5
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._dyingNaturalResourceFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000016 RID: 22
			public readonly DyingNaturalResourceFragment _dyingNaturalResourceFragment;
		}
	}
}
