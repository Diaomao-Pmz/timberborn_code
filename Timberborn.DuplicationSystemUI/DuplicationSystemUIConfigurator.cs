using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.DuplicationSystemUI
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	[Context("MapEditor")]
	public class DuplicationSystemUIConfigurator : Configurator
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002864 File Offset: 0x00000A64
		public override void Configure()
		{
			base.Bind<DuplicateSettingsFragment>().AsSingleton();
			base.Bind<DuplicateObjectFragment>().AsSingleton();
			base.Bind<DuplicateSettingsTool>().AsSingleton();
			base.Bind<DuplicationInputProcessor>().AsSingleton();
			base.Bind<DuplicationValidator>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<DuplicationSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000D RID: 13
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000035 RID: 53 RVA: 0x000028C6 File Offset: 0x00000AC6
			public EntityPanelModuleProvider(DuplicateSettingsFragment duplicateSettingsFragment, DuplicateObjectFragment duplicateObjectFragment)
			{
				this._duplicateSettingsFragment = duplicateSettingsFragment;
				this._duplicateObjectFragment = duplicateObjectFragment;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x000028DC File Offset: 0x00000ADC
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddLeftHeaderFragment(this._duplicateSettingsFragment, 10);
				builder.AddLeftHeaderFragment(this._duplicateObjectFragment, 20);
				return builder.Build();
			}

			// Token: 0x04000036 RID: 54
			public readonly DuplicateSettingsFragment _duplicateSettingsFragment;

			// Token: 0x04000037 RID: 55
			public readonly DuplicateObjectFragment _duplicateObjectFragment;
		}
	}
}
