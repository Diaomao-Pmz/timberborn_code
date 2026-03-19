using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.ActivatorSystemUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	[Context("MapEditor")]
	public class ActivatorSystemUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public override void Configure()
		{
			base.Bind<TimedComponentActivatorFragment>().AsSingleton();
			base.Bind<TimedComponentActivatorSettingsFragment>().AsSingleton();
			base.Bind<TimedActivatorSettingFactory>().AsSingleton();
			base.Bind<TimedActivatorProgressBarFactory>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<ActivatorSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000005 RID: 5
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000005 RID: 5 RVA: 0x00002116 File Offset: 0x00000316
			public EntityPanelModuleProvider(TimedComponentActivatorFragment timedComponentActivatorFragment, TimedComponentActivatorSettingsFragment timedComponentActivatorSettingsFragment)
			{
				this._timedComponentActivatorFragment = timedComponentActivatorFragment;
				this._timedComponentActivatorSettingsFragment = timedComponentActivatorSettingsFragment;
			}

			// Token: 0x06000006 RID: 6 RVA: 0x0000212C File Offset: 0x0000032C
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._timedComponentActivatorFragment, 10);
				builder.AddMiddleFragment(this._timedComponentActivatorSettingsFragment, 11);
				return builder.Build();
			}

			// Token: 0x04000006 RID: 6
			public readonly TimedComponentActivatorFragment _timedComponentActivatorFragment;

			// Token: 0x04000007 RID: 7
			public readonly TimedComponentActivatorSettingsFragment _timedComponentActivatorSettingsFragment;
		}
	}
}
