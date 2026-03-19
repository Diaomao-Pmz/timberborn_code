using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.GrowingUI
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class GrowingUIConfigurator : Configurator
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000025C4 File Offset: 0x000007C4
		public override void Configure()
		{
			base.Bind<GrowableFragment>().AsSingleton();
			base.Bind<GrowableToolPanelItemFactory>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<GrowingUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000007 RID: 7
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000010 RID: 16 RVA: 0x000025F7 File Offset: 0x000007F7
			public EntityPanelModuleProvider(GrowableFragment growableFragment)
			{
				this._growableFragment = growableFragment;
			}

			// Token: 0x06000011 RID: 17 RVA: 0x00002606 File Offset: 0x00000806
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._growableFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000020 RID: 32
			public readonly GrowableFragment _growableFragment;
		}
	}
}
