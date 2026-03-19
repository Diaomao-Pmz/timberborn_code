using System;
using Bindito.Core;

namespace Timberborn.MapItemsUI
{
	// Token: 0x0200000E RID: 14
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class MapItemsUIConfigurator : Configurator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000026E8 File Offset: 0x000008E8
		public override void Configure()
		{
			base.Bind<MapItemProvider>().AsSingleton();
			base.Bind<OfficialMapItemFactory>().AsSingleton();
			base.Bind<UserMapItemFactory>().AsSingleton();
			base.Bind<MapItemElementFactory>().AsSingleton();
			base.Bind<MapItemFactionIconFactory>().AsSingleton();
		}
	}
}
