using System;
using Bindito.Core;
using Timberborn.MapItemsUI;

namespace Timberborn.SteamWorkshopMapDownloading
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class SteamWorkshopMapDownloadingConfigurator : Configurator
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020EF File Offset: 0x000002EF
		public override void Configure()
		{
			base.MultiBind<ICustomMapItemFactory>().To<SteamWorkshopMapItemFactory>().AsSingleton();
			base.Bind<SteamMapRepositoryChangeNotifier>().AsSingleton();
		}
	}
}
