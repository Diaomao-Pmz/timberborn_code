using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.DeteriorationSystemUI
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class DeteriorationSystemUIConfigurator : Configurator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023C9 File Offset: 0x000005C9
		public override void Configure()
		{
			base.Bind<DeteriorableFragment>().AsSingleton();
			base.Bind<DeteriorableBatchControlRowItemFactory>().AsSingleton();
			base.Bind<DeteriorableDebugFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<DeteriorationSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000A RID: 10
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600001A RID: 26 RVA: 0x00002408 File Offset: 0x00000608
			public EntityPanelModuleProvider(DeteriorableFragment deteriorableFragment, DeteriorableDebugFragment deteriorableDebugFragment)
			{
				this._deteriorableFragment = deteriorableFragment;
				this._deteriorableDebugFragment = deteriorableDebugFragment;
			}

			// Token: 0x0600001B RID: 27 RVA: 0x0000241E File Offset: 0x0000061E
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._deteriorableFragment, 0);
				builder.AddDiagnosticFragment(this._deteriorableDebugFragment);
				return builder.Build();
			}

			// Token: 0x04000019 RID: 25
			public readonly DeteriorableFragment _deteriorableFragment;

			// Token: 0x0400001A RID: 26
			public readonly DeteriorableDebugFragment _deteriorableDebugFragment;
		}
	}
}
