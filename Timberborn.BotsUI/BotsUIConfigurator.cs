using System;
using Bindito.Core;
using Timberborn.Bots;
using Timberborn.BottomBarSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BotsUI
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	public class BotsUIConfigurator : Configurator
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002684 File Offset: 0x00000884
		public override void Configure()
		{
			base.Bind<BotEntityBadge>().AsTransient();
			base.Bind<BotSelectionSound>().AsTransient();
			base.Bind<BotGeneratorTool>().AsSingleton();
			base.Bind<BotGeneratorButton>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BotsUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<BotsUIConfigurator.BottomBarModuleProvider>().AsSingleton();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026EF File Offset: 0x000008EF
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BotSpec, BotEntityBadge>();
			builder.AddDecorator<BotSelectionSoundSpec, BotSelectionSound>();
			return builder.Build();
		}

		// Token: 0x0200000F RID: 15
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x0600003B RID: 59 RVA: 0x0000270F File Offset: 0x0000090F
			public BottomBarModuleProvider(BotGeneratorButton botGeneratorButton)
			{
				this._botGeneratorButton = botGeneratorButton;
			}

			// Token: 0x0600003C RID: 60 RVA: 0x0000271E File Offset: 0x0000091E
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._botGeneratorButton, 80);
				return builder.Build();
			}

			// Token: 0x04000026 RID: 38
			public readonly BotGeneratorButton _botGeneratorButton;
		}
	}
}
