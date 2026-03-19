using System;
using Bindito.Core;

namespace Timberborn.SteamWorkshopModDownloadingUI
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	public class SteamWorkshopModDownloadingUIConfigurator : Configurator
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002113 File Offset: 0x00000313
		public override void Configure()
		{
			base.Bind<SteamWorkshopModDownloader>().AsSingleton();
		}
	}
}
