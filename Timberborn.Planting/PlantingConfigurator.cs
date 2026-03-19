using System;
using Bindito.Core;
using Timberborn.NaturalResourcesReproduction;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.Planting
{
	// Token: 0x0200001B RID: 27
	[Context("Game")]
	[Context("MapEditor")]
	public class PlantingConfigurator : Configurator
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000033E0 File Offset: 0x000015E0
		public override void Configure()
		{
			base.Bind<PlantBehavior>().AsTransient();
			base.Bind<PlanterWorkplaceBehavior>().AsTransient();
			base.Bind<PlantExecutor>().AsTransient();
			base.Bind<Plantable>().AsTransient();
			base.Bind<InRangePlantingCoordinates>().AsTransient();
			base.Bind<PlantablePrioritizer>().AsTransient();
			base.Bind<PlantableReproductionBlocker>().AsTransient();
			base.Bind<Planter>().AsTransient();
			base.Bind<PlanterBuilding>().AsTransient();
			base.Bind<PlanterBuildingStatusUpdater>().AsTransient();
			base.Bind<PlantingSpotFinder>().AsTransient();
			base.Bind<PlantingService>().AsSingleton();
			base.Bind<PlantingAreaValidator>().AsSingleton();
			base.Bind<PlantingSoilValidator>().AsSingleton();
			base.Bind<PlantingMapSerializer>().AsSingleton();
			base.Bind<PlantableReproductionBlockerService>().AsSingleton();
			base.Bind<PlantingCoordinatesUnsetter>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PlantingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000034D6 File Offset: 0x000016D6
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Worker, Planter>();
			builder.AddDecorator<Worker, PlantBehavior>();
			builder.AddDecorator<Reproducible, PlantableReproductionBlocker>();
			builder.AddDecorator<PlantablePrioritizer, PlantingSpotFinder>();
			builder.AddDecorator<PlanterBuildingSpec, PlanterBuilding>();
			builder.AddDecorator<PlanterBuilding, PlanterBuildingStatusUpdater>();
			builder.AddDecorator<PlanterBuilding, InRangePlantingCoordinates>();
			builder.AddDecorator<PlantableSpec, Plantable>();
			return builder.Build();
		}
	}
}
