using System;
using Bindito.Core;
using Timberborn.Modding;

namespace Timberborn.SteamWorkshopModDownloading
{
	// Token: 0x02000004 RID: 4
	[Context("Bootstrapper")]
	public class SteamWorkshopModDownloadingConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.MultiBind<IModsProvider>().To<SteamWorkshopModsProvider>().AsSingleton();
		}
	}
}
