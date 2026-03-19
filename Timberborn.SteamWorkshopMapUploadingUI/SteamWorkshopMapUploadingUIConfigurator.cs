using System;
using Bindito.Core;

namespace Timberborn.SteamWorkshopMapUploadingUI
{
	// Token: 0x02000006 RID: 6
	[Context("MapEditor")]
	public class SteamWorkshopMapUploadingUIConfigurator : Configurator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000023D1 File Offset: 0x000005D1
		public override void Configure()
		{
			base.Bind<SteamWorkshopUploadableMapFactory>().AsSingleton();
			base.Bind<SteamWorkshopUploadMapPanelOpener>().AsSingleton();
			base.Bind<SteamWorkshopMapDataService>().AsSingleton();
		}
	}
}
