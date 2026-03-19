using System;
using Bindito.Core;

namespace Timberborn.SteamWorkshopUI
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("MapEditor")]
	public class SteamWorkshopUIConfigurator : Configurator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000020C0 File Offset: 0x000002C0
		public override void Configure()
		{
			base.Bind<SteamWorkshopUploadPanel>().AsSingleton();
			base.Bind<VisibilityDropdownProvider>().AsSingleton();
			base.Bind<UploadPanelElements>().AsSingleton();
			base.Bind<SteamWorkshopUploadProgress>().AsSingleton();
			base.Bind<UploadPanelTags>().AsSingleton();
		}
	}
}
