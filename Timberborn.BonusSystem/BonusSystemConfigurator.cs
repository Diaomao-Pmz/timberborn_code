using System;
using Bindito.Core;
using Timberborn.Characters;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BonusSystem
{
	// Token: 0x0200000C RID: 12
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class BonusSystemConfigurator : Configurator
	{
		// Token: 0x0600003C RID: 60 RVA: 0x0000284C File Offset: 0x00000A4C
		public override void Configure()
		{
			base.Bind<BonusManager>().AsTransient();
			base.Bind<BonusTypeSpecService>().AsSingleton();
			base.Bind<BonusDescriber>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BonusSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000289A File Offset: 0x00000A9A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, BonusManager>();
			return builder.Build();
		}
	}
}
