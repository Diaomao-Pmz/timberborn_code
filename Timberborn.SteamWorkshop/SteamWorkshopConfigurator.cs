using System;
using Bindito.Core;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x02000006 RID: 6
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class SteamWorkshopConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002126 File Offset: 0x00000326
		public override void Configure()
		{
			base.Bind<SteamWorkshopItemCreator>().AsSingleton();
			base.Bind<SteamWorkshopItemUpdater>().AsSingleton();
			base.Bind<ItemInstalledNotifier>().AsSingleton();
			base.Bind<SteamWorkshopItemSerializer>().AsSingleton();
		}
	}
}
