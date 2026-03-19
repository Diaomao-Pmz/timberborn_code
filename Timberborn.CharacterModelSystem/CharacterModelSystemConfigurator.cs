using System;
using Bindito.Core;
using Timberborn.StatusSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class CharacterModelSystemConfigurator : Configurator
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002868 File Offset: 0x00000A68
		public override void Configure()
		{
			base.Bind<AnimateExecutor>().AsTransient();
			base.Bind<CharacterAnimator>().AsTransient();
			base.Bind<CharacterModel>().AsTransient();
			base.Bind<CharacterStatusIconCyclerPositioner>().AsTransient();
			base.Bind<CharacterStatusInitializer>().AsTransient();
			base.Bind<CharacterTextureSetter>().AsTransient();
			base.Bind<VisibleSelectedCharacterModel>().AsTransient();
			base.Bind<CharacterModelHider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CharacterModelSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028F2 File Offset: 0x00000AF2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<CharacterModelSpec, CharacterModel>();
			builder.AddDecorator<CharacterModel, CharacterAnimator>();
			builder.AddDecorator<CharacterModel, VisibleSelectedCharacterModel>();
			builder.AddDecorator<CharacterModel, CharacterStatusIconCyclerPositioner>();
			builder.AddDecorator<CharacterModel, CharacterStatusInitializer>();
			builder.AddDecorator<CharacterStatusInitializer, StatusSubject>();
			builder.AddDecorator<CharacterStatusInitializer, StatusIconCycler>();
			builder.AddDecorator<CharacterTextureSetterSpec, CharacterTextureSetter>();
			return builder.Build();
		}
	}
}
