using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200001E RID: 30
	[Context("Game")]
	[Context("MapEditor")]
	public class ConstructionSitesConfigurator : Configurator
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00003EE0 File Offset: 0x000020E0
		public override void Configure()
		{
			base.Bind<BuildBehavior>().AsTransient();
			base.Bind<BuildExecutor>().AsTransient();
			base.Bind<ConstructionSite>().AsTransient();
			base.Bind<ConstructionSiteProgressVisualizer>().AsTransient();
			base.Bind<Builder>().AsTransient();
			base.Bind<ConstructionJob>().AsTransient();
			base.Bind<ConstructionRegistrar>().AsTransient();
			base.Bind<ConstructionSiteBuildersLimiter>().AsTransient();
			base.Bind<ConstructionSiteModelUpdater>().AsTransient();
			base.Bind<ConstructionSitePrioritizableEnabler>().AsTransient();
			base.Bind<ConstructionSiteRecoverableGoodMultiplier>().AsTransient();
			base.Bind<ConstructionSiteReservations>().AsTransient();
			base.Bind<ConstructionSiteSlotManager>().AsTransient();
			base.Bind<DeleteOnFinishConstructionSite>().AsTransient();
			base.Bind<GroundedConstructionSite>().AsTransient();
			base.Bind<PhysicallySupportedConstructionSite>().AsTransient();
			base.Bind<PhysicallySupportedConstructionSiteUpdater>().AsTransient();
			base.Bind<ConstructionRegistry>().AsSingleton();
			base.Bind<ConstructionFactory>().AsSingleton();
			base.Bind<ConstructionSiteInventoryInitializer>().AsSingleton();
			base.Bind<ConstructionSiteBuildTimeCalculator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<ConstructionSitesTemplateModuleProvider>().AsSingleton();
		}
	}
}
