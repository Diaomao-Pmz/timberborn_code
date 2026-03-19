using System;
using Bindito.Core;
using Timberborn.Characters;
using Timberborn.TemplateInstantiation;

namespace Timberborn.CharacterControlSystem
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class CharacterControlSystemConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000215E File Offset: 0x0000035E
		public override void Configure()
		{
			base.Bind<CharacterControlRootBehavior>().AsTransient();
			base.Bind<ControllableCharacter>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CharacterControlSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002195 File Offset: 0x00000395
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, ControllableCharacter>();
			return builder.Build();
		}
	}
}
