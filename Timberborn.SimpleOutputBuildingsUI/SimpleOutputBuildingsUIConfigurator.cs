using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.SimpleOutputBuildingsUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class SimpleOutputBuildingsUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<SimpleOutputInventoryFragmentEnabler>().AsTransient();
			base.Bind<SimpleOutputInventoryFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<SimpleOutputBuildingsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000005 RID: 5
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000005 RID: 5 RVA: 0x000020F1 File Offset: 0x000002F1
			public EntityPanelModuleProvider(SimpleOutputInventoryFragment simpleOutputInventoryFragment)
			{
				this._simpleOutputInventoryFragment = simpleOutputInventoryFragment;
			}

			// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddBottomFragment(this._simpleOutputInventoryFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000006 RID: 6
			public readonly SimpleOutputInventoryFragment _simpleOutputInventoryFragment;
		}
	}
}
