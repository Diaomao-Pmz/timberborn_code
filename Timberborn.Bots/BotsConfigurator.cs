using System;
using Bindito.Core;
using Timberborn.Characters;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Bots
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	public class BotsConfigurator : Configurator
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000024B0 File Offset: 0x000006B0
		public override void Configure()
		{
			base.Bind<BotIlluminationController>().AsTransient();
			base.Bind<BotLongevity>().AsTransient();
			base.Bind<Bot>().AsTransient();
			base.Bind<BotFactory>().AsSingleton();
			base.Bind<BotPopulation>().AsSingleton();
			base.Bind<BotColors>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BotsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002522 File Offset: 0x00000722
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BotSpec, Bot>();
			builder.AddDecorator<BotSpec, Character>();
			builder.AddDecorator<BotSpec, BotIlluminationController>();
			builder.AddDecorator<BotSpec, BotLongevity>();
			return builder.Build();
		}
	}
}
