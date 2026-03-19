using System;
using Bindito.Core;
using Timberborn.Characters;
using Timberborn.TemplateInstantiation;

namespace Timberborn.CharacterNavigation
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class CharacterNavigationConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public override void Configure()
		{
			base.Bind<Navigator>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CharacterNavigationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E6 File Offset: 0x000002E6
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, Navigator>();
			return builder.Build();
		}
	}
}
