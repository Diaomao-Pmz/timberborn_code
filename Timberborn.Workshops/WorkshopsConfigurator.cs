using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Workshops
{
	// Token: 0x0200002E RID: 46
	[Context("Game")]
	public class WorkshopsConfigurator : Configurator
	{
		// Token: 0x06000170 RID: 368 RVA: 0x000058A0 File Offset: 0x00003AA0
		public override void Configure()
		{
			base.Bind<RecipeGoodDisallower>().AsTransient();
			base.Bind<FillInputWorkplaceBehavior>().AsTransient();
			base.Bind<ProduceWorkplaceBehavior>().AsTransient();
			base.Bind<WorkWorkplaceBehavior>().AsTransient();
			base.Bind<ProduceExecutor>().AsTransient();
			base.Bind<WorkExecutor>().AsTransient();
			base.Bind<LackOfResourcesStatus>().AsTransient();
			base.Bind<ProductionIncreaser>().AsTransient();
			base.Bind<ProductionResetter>().AsTransient();
			base.Bind<WorkplacePowerConsumptionSwitch>().AsTransient();
			base.Bind<WorkshopProductivityCounter>().AsTransient();
			base.Bind<WorkplaceWorkStarter>().AsTransient();
			base.Bind<AutomaticRecipeManufactory>().AsTransient();
			base.Bind<FillInputHaulBehaviorProvider>().AsTransient();
			base.Bind<Manufactory>().AsTransient();
			base.Bind<ManufactoryHaulBehaviorProvider>().AsTransient();
			base.Bind<ManufactoryInputChecker>().AsTransient();
			base.Bind<ManufactoryTogglableRecipes>().AsTransient();
			base.Bind<NoRecipeStatus>().AsTransient();
			base.Bind<Workshop>().AsTransient();
			base.Bind<RecipeSpecService>().AsSingleton();
			base.Bind<ManufactoryInventoryInitializer>().AsSingleton();
			base.Bind<DailyProductivitySerializer>().AsSingleton();
			base.Bind<HourlyProductivitySerializer>().AsSingleton();
			base.Bind<RecipeGoodsProcessor>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<WorkshopsTemplateModuleProvider>().AsSingleton();
		}
	}
}
