using System;
using Bindito.Core;

namespace Timberborn.MainMenuScene
{
	// Token: 0x0200000B RID: 11
	[Context("MainMenu")]
	public class MainMenuSceneConfigurator : Configurator
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002588 File Offset: 0x00000788
		public override void Configure()
		{
			base.Bind<AutoStarter>().AsSingleton();
			base.Bind<PlayerDataLoader>().AsSingleton();
			base.Bind<InitialLanguageChooser>().AsSingleton();
			base.Bind<MacOSPermissionsChecker>().AsSingleton();
			base.Bind<WelcomeScreenBox>().AsSingleton();
			base.Bind<MainMenuInitializer>().AsSingleton();
			base.Bind<EditorBuildValidator>().AsSingleton();
			base.Bind<AssetBundleValidator>().AsSingleton();
		}
	}
}
