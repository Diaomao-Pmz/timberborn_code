using System;
using Bindito.Core;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x0200000A RID: 10
	[Context("MainMenu")]
	[Context("MapEditor")]
	public class MapRepositorySystemUIConfigurator : Configurator
	{
		// Token: 0x0600001F RID: 31 RVA: 0x0000243C File Offset: 0x0000063C
		public override void Configure()
		{
			base.Bind<SelectedMapPanel>().AsTransient();
			base.Bind<MapSelection>().AsTransient();
			base.Bind<NewMapBox>().AsSingleton();
			base.Bind<LoadMapBox>().AsSingleton();
			base.Bind<MapValidator>().AsSingleton();
			base.Bind<MapVersionCompatibilityService>().AsSingleton();
			base.Bind<DevModeMapRepositoryChangeNotifier>().AsSingleton();
			base.Bind<MapDownloader>().AsSingleton();
			base.MultiBind<IMapLoadValidator>().To<MapVersionValidator>().AsSingleton();
		}
	}
}
