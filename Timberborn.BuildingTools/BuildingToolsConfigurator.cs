using System;
using Bindito.Core;
using Timberborn.BlockObjectTools;
using Timberborn.ToolSystem;

namespace Timberborn.BuildingTools
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class BuildingToolsConfigurator : Configurator
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000266E File Offset: 0x0000086E
		public override void Configure()
		{
			base.Bind<BuildingCostSectionProvider>().AsSingleton();
			base.Bind<UnlockSectionController>().AsSingleton();
			base.MultiBind<IToolLocker>().To<BuildingToolLocker>().AsSingleton();
			base.MultiBind<IBlockObjectPlacer>().To<BuildingPlacer>().AsSingleton();
		}
	}
}
