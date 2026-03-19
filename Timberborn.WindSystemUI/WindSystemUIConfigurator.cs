using System;
using Bindito.Core;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;

namespace Timberborn.WindSystemUI
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class WindSystemUIConfigurator : Configurator
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000022DB File Offset: 0x000004DB
		public override void Configure()
		{
			base.Bind<WeathervaneFragment>().AsSingleton();
			base.MultiBind<IDevModule>().To<DebugWindDevModule>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<WindSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000007 RID: 7
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000E RID: 14 RVA: 0x00002313 File Offset: 0x00000513
			public EntityPanelModuleProvider(WeathervaneFragment weathervaneFragment)
			{
				this._weathervaneFragment = weathervaneFragment;
			}

			// Token: 0x0600000F RID: 15 RVA: 0x00002322 File Offset: 0x00000522
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._weathervaneFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000010 RID: 16
			public readonly WeathervaneFragment _weathervaneFragment;
		}
	}
}
