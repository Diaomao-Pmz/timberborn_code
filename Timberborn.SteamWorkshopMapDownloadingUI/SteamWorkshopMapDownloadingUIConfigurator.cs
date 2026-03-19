using System;
using Bindito.Core;

namespace Timberborn.SteamWorkshopMapDownloadingUI
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("MapEditor")]
	public class SteamWorkshopMapDownloadingUIConfigurator : Configurator
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002113 File Offset: 0x00000313
		public override void Configure()
		{
			base.Bind<SteamWorkshopMapDownloader>().AsSingleton();
		}
	}
}
