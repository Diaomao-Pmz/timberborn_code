using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.GoodStackSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class GoodStackSystemUIConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002193 File Offset: 0x00000393
		public override void Configure()
		{
			base.Bind<GoodStackFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<GoodStackSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000006 RID: 6
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000A RID: 10 RVA: 0x000021BA File Offset: 0x000003BA
			public EntityPanelModuleProvider(GoodStackFragment goodStackFragment)
			{
				this._goodStackFragment = goodStackFragment;
			}

			// Token: 0x0600000B RID: 11 RVA: 0x000021C9 File Offset: 0x000003C9
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddBottomFragment(this._goodStackFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400000B RID: 11
			public readonly GoodStackFragment _goodStackFragment;
		}
	}
}
