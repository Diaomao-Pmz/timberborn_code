using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.CharactersGame
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class CharactersGameConfigurator : Configurator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002291 File Offset: 0x00000491
		public override void Configure()
		{
			base.Bind<CharacterBirthNotifier>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CharactersGameConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022BC File Offset: 0x000004BC
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<CharacterBirthNotifierSpec, CharacterBirthNotifier>();
			return builder.Build();
		}
	}
}
