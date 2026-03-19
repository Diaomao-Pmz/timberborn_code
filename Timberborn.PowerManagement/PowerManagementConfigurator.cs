using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.PowerManagement
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	public class PowerManagementConfigurator : Configurator
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000029EC File Offset: 0x00000BEC
		public override void Configure()
		{
			base.Bind<GravityBattery>().AsTransient();
			base.Bind<Clutch>().AsTransient();
			base.Bind<ClutchModel>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PowerManagementConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A3A File Offset: 0x00000C3A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GravityBatterySpec, GravityBattery>();
			builder.AddDecorator<ClutchSpec, Clutch>();
			builder.AddDecorator<ClutchModelSpec, ClutchModel>();
			return builder.Build();
		}
	}
}
