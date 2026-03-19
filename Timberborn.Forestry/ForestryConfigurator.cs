using System;
using Bindito.Core;
using Timberborn.BuildingRange;
using Timberborn.Emptying;
using Timberborn.GoodStackSystem;
using Timberborn.Hauling;
using Timberborn.LaborSystem;
using Timberborn.Planting;
using Timberborn.SimpleOutputBuildings;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.Forestry
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class ForestryConfigurator : Configurator
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002348 File Offset: 0x00000548
		public override void Configure()
		{
			base.Bind<LumberjackFlagWorkplaceBehavior>().AsTransient();
			base.Bind<Forester>().AsTransient();
			base.Bind<LumberjackFlagStatusDeactivator>().AsTransient();
			base.Bind<LumberjackGoodStackAdder>().AsTransient();
			base.Bind<TreeComponent>().AsTransient();
			base.Bind<TreeCutter>().AsTransient();
			base.Bind<TreeRemoveYieldStrategy>().AsTransient();
			base.Bind<TreeReacher>().AsTransient();
			base.Bind<GoodStackService<LumberjackFlagSpec>>().AsSingleton();
			base.Bind<TreeCuttingArea>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ForestryConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023EC File Offset: 0x000005EC
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			ForestryConfigurator.InitializeBehaviors(builder);
			builder.AddDecorator<TreeComponentSpec, TreeComponent>();
			builder.AddDecorator<TreeComponent, LumberjackGoodStackAdder>();
			builder.AddDecorator<TreeComponent, TreeRemoveYieldStrategy>();
			builder.AddDecorator<TreeComponent, TreeReacher>();
			builder.AddDecorator<BushSpec, LumberjackGoodStackAdder>();
			builder.AddDecorator<ForesterSpec, Forester>();
			builder.AddDecorator<Forester, PlantablePrioritizer>();
			builder.AddDecorator<Forester, BuildingWithTerrainRange>();
			builder.AddDecorator<LumberjackFlagSpec, BuildingWithTerrainRange>();
			builder.AddDecorator<LumberjackFlagSpec, AutoEmptiable>();
			builder.AddDecorator<LumberjackFlagSpec, Emptiable>();
			builder.AddDecorator<LumberjackFlagSpec, HaulCandidate>();
			builder.AddDecorator<LumberjackFlagSpec, SimpleOutputInventoryHaulBehaviorProvider>();
			builder.AddDecorator<LumberjackFlagSpec, LumberjackFlagStatusDeactivator>();
			builder.AddDecorator<Worker, TreeCutter>();
			return builder.Build();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002463 File Offset: 0x00000663
		public static void InitializeBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<Forester, PlanterWorkplaceBehavior>();
			builder.AddDecorator<Forester, LaborWorkplaceBehavior>();
			builder.AddDecorator<Forester, WaitInsideIdlyWorkplaceBehavior>();
			builder.AddDecorator<LumberjackFlagSpec, LumberjackFlagWorkplaceBehavior>();
			builder.AddDecorator<LumberjackFlagSpec, EmptyOutputWorkplaceBehavior>();
			builder.AddDecorator<LumberjackFlagSpec, EmptyInventoriesWorkplaceBehavior>();
			builder.AddDecorator<LumberjackFlagSpec, RemoveUnwantedStockWorkplaceBehavior>();
			builder.AddDecorator<LumberjackFlagSpec, LaborWorkplaceBehavior>();
			builder.AddDecorator<LumberjackFlagSpec, WaitInsideIdlyWorkplaceBehavior>();
		}
	}
}
