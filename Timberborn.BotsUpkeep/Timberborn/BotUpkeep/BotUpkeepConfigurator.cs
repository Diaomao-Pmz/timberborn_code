using System;
using Bindito.Core;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BotUpkeep
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class BotUpkeepConfigurator : Configurator
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000299F File Offset: 0x00000B9F
		public override void Configure()
		{
			base.Bind<BotManufactoryAnimationController>().AsTransient();
			base.Bind<BotManufactory>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BotUpkeepConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029D6 File Offset: 0x00000BD6
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BotManufactoryAnimationControllerSpec, BotManufactoryAnimationController>();
			builder.AddDecorator<BotManufactoryAnimationController, ParticlesCache>();
			builder.AddDecorator<BotManufactorySpec, BotManufactory>();
			return builder.Build();
		}
	}
}
