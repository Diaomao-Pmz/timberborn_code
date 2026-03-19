using System;
using Bindito.Core;
using Timberborn.Autosaving;
using Timberborn.BlockSystem;

namespace Timberborn.GameStartup
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class GameStartupConfigurator : Configurator
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002378 File Offset: 0x00000578
		public override void Configure()
		{
			base.Bind<StartingBuildingToolDescriber>().AsSingleton();
			base.Bind<StartingBuildingToolShower>().AsSingleton();
			base.Bind<StartingGoodsProvider>().AsSingleton();
			base.Bind<GameStarter>().AsSingleton();
			base.Bind<GameInitializer>().AsSingleton();
			base.Bind<StartingBuildingInitializer>().AsSingleton();
			base.Bind<StartingBeaversInitializer>().AsSingleton();
			base.Bind<StartingBuildingSpawner>().AsSingleton();
			base.Bind<StartingBuildingToolFactory>().AsSingleton();
			base.Bind<StartingBuildingPlacer>().AsSingleton();
			base.MultiBind<IBlockObjectValidator>().To<StartingBuildingPlacementValidator>().AsSingleton();
			base.MultiBind<IAutosaveBlocker>().To<GameStartupAutosaveBlocker>().AsSingleton();
		}
	}
}
