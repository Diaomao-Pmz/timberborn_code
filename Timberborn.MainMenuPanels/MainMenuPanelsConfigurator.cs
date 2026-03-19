using System;
using Bindito.Core;

namespace Timberborn.MainMenuPanels
{
	// Token: 0x02000008 RID: 8
	[Context("MainMenu")]
	public class MainMenuPanelsConfigurator : Configurator
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003178 File Offset: 0x00001378
		public override void Configure()
		{
			base.Bind<MainMenuPanel>().AsSingleton();
			base.Bind<CreditsBox>().AsSingleton();
			base.Bind<NewGameFactionPanel>().AsSingleton();
			base.Bind<NewGameMapPanel>().AsSingleton();
			base.Bind<NewGameModePanel>().AsSingleton();
			base.Bind<CustomNewGameModeController>().AsSingleton();
			base.Bind<MainMenuSoundController>().AsSingleton();
			base.Bind<TutorialToggleController>().AsSingleton();
		}
	}
}
