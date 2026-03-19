using System;
using Bindito.Core;
using Timberborn.ModdingUI;

namespace Timberborn.MainMenuModdingUI
{
	// Token: 0x02000006 RID: 6
	[Context("MainMenu")]
	public class MainMenuModdingUIConfigurator : Configurator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000024AC File Offset: 0x000006AC
		public override void Configure()
		{
			base.Bind<ModManagerBox>().AsSingleton();
			base.Bind<CreateModBox>().AsSingleton();
			base.Bind<ModCreator>().AsSingleton();
			base.Bind<ModUploaderBox>().AsSingleton();
			base.Bind<IModManagerTooltipRegistrar>().To<ModManagerBoxTooltipRegistrar>().AsSingleton();
			base.Bind<IModItemFactory>().To<MainMenuModItemFactory>().AsSingleton();
			base.Bind<ModTemplateDropdownProvider>().AsSingleton();
		}
	}
}
