using System;
using Bindito.Core;

namespace Timberborn.CoreUI
{
	// Token: 0x0200000B RID: 11
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class CoreUIConfigurator : Configurator
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000022D0 File Offset: 0x000004D0
		public override void Configure()
		{
			base.Bind<DialogBoxShower>().AsSingleton();
			base.Bind<InputBoxShower>().AsSingleton();
			base.Bind<IntegerSliderFactory>().AsSingleton();
			base.Bind<RowShader>().AsSingleton();
			base.Bind<UISettings>().AsSingleton();
			base.Bind<VisualElementInitializer>().AsSingleton();
			base.Bind<VisualElementLoader>().AsSingleton();
			base.Bind<ScrollBarInitializationService>().AsSingleton();
			base.Bind<AlternateClickableFactory>().AsSingleton();
			base.Bind<HyperlinkInitializer>().AsSingleton();
			base.Bind<DelayedButtonEnabler>().AsSingleton();
			base.Bind<Underlay>().AsSingleton();
			base.Bind<PanelStack>().AsSingleton();
			base.Bind<UIScaler>().AsSingleton();
			base.Bind<RadioToggleFactory>().AsSingleton();
			base.MultiBind<IVisualElementInitializer>().To<VisualElementLocalizer>().AsSingleton();
			base.MultiBind<IVisualElementInitializer>().To<ButtonClickabilityInitializer>().AsSingleton();
			base.MultiBind<IVisualElementInitializer>().To<UISoundInitializer>().AsSingleton();
			base.MultiBind<IVisualElementInitializer>().To<ScrollBarInitializer>().AsSingleton();
			base.MultiBind<IVisualElementInitializer>().To<TextElementInitializer>().AsSingleton();
		}
	}
}
