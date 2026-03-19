using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.PowerManagement;
using Timberborn.TemplateInstantiation;

namespace Timberborn.PowerManagementUI
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class PowerManagementUIConfigurator : Configurator
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002690 File Offset: 0x00000890
		public override void Configure()
		{
			base.Bind<GravityBatteryDescriber>().AsTransient();
			base.Bind<ClutchFragment>().AsSingleton();
			base.Bind<ClutchModeToggleFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PowerManagementUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<PowerManagementUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026EF File Offset: 0x000008EF
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GravityBattery, GravityBatteryDescriber>();
			return builder.Build();
		}

		// Token: 0x0200000A RID: 10
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600002D RID: 45 RVA: 0x00002709 File Offset: 0x00000909
			public EntityPanelModuleProvider(ClutchFragment clutchFragment)
			{
				this._clutchFragment = clutchFragment;
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00002718 File Offset: 0x00000918
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._clutchFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000029 RID: 41
			public readonly ClutchFragment _clutchFragment;
		}
	}
}
