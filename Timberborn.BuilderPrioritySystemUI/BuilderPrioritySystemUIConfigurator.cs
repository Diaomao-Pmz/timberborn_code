using System;
using Bindito.Core;
using Timberborn.BottomBarSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	public class BuilderPrioritySystemUIConfigurator : Configurator
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000025D4 File Offset: 0x000007D4
		public override void Configure()
		{
			base.Bind<BuilderPrioritizableHighlightUpdater>().AsTransient();
			base.Bind<BuilderPrioritiesButton>().AsSingleton();
			base.Bind<BuilderPrioritiesButtonFactory>().AsSingleton();
			base.Bind<BuilderPrioritizableHighlighter>().AsSingleton();
			base.Bind<BuilderPrioritySpriteLoader>().AsSingleton();
			base.Bind<BuilderPriorityToggleGroupFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BuilderPrioritySystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<BuilderPrioritySystemUIConfigurator.BottomBarModuleProvider>().AsSingleton();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002657 File Offset: 0x00000857
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuilderPrioritizable, BuilderPrioritizableHighlightUpdater>();
			return builder.Build();
		}

		// Token: 0x0200000E RID: 14
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x0600002C RID: 44 RVA: 0x00002671 File Offset: 0x00000871
			public BottomBarModuleProvider(BuilderPrioritiesButton builderPrioritiesButton)
			{
				this._builderPrioritiesButton = builderPrioritiesButton;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002680 File Offset: 0x00000880
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._builderPrioritiesButton, 60);
				return builder.Build();
			}

			// Token: 0x04000025 RID: 37
			public readonly BuilderPrioritiesButton _builderPrioritiesButton;
		}
	}
}
