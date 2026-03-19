using System;
using Bindito.Core;

namespace Timberborn.TitleScreenUI
{
	// Token: 0x02000008 RID: 8
	[Context("MainMenu")]
	public class TitleScreenUIConfigurator : Configurator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002358 File Offset: 0x00000558
		public override void Configure()
		{
			base.Bind<TitleScreen>().AsSingleton();
			base.Bind<TitleScreenFooter>().AsSingleton();
			base.Bind<ChangeLanguageButtonInitializer>().AsSingleton();
			base.Bind<MacOsRosettaWarningInitializer>().AsSingleton();
		}
	}
}
