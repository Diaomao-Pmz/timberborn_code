using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.CarryingUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class CarryingUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<GoodCarrierFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<CarryingUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000005 RID: 5
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000005 RID: 5 RVA: 0x000020E5 File Offset: 0x000002E5
			public EntityPanelModuleProvider(GoodCarrierFragment goodCarrierFragment)
			{
				this._goodCarrierFragment = goodCarrierFragment;
			}

			// Token: 0x06000006 RID: 6 RVA: 0x000020F4 File Offset: 0x000002F4
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddBottomFragment(this._goodCarrierFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000006 RID: 6
			public readonly GoodCarrierFragment _goodCarrierFragment;
		}
	}
}
