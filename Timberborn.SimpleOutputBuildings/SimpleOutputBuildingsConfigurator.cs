using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.SimpleOutputBuildings
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class SimpleOutputBuildingsConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public override void Configure()
		{
			base.Bind<SimpleOutputInventory>().AsTransient();
			base.Bind<SimpleOutputInventoryHaulBehaviorProvider>().AsTransient();
			base.Bind<SimpleOutputInventoryInitializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<SimpleOutputBuildingsTemplateModuleProvider>().AsSingleton();
		}
	}
}
