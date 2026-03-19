using System;
using Bindito.Core;
using Timberborn.BuildingRange;
using Timberborn.Demolishing;
using Timberborn.Emptying;
using Timberborn.Hauling;
using Timberborn.LaborSystem;
using Timberborn.SimpleOutputBuildings;
using Timberborn.SoilContaminationSystem;
using Timberborn.SoilMoistureSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.Ruins
{
	// Token: 0x02000011 RID: 17
	[Context("Game")]
	[Context("MapEditor")]
	public class RuinsConfigurator : Configurator
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002DBC File Offset: 0x00000FBC
		public override void Configure()
		{
			base.Bind<ScavengerWorkplaceBehavior>().AsTransient();
			base.Bind<Ruin>().AsTransient();
			base.Bind<RuinModels>().AsTransient();
			base.Bind<RuinModelUpdater>().AsTransient();
			base.Bind<RuinsRemoveYieldStrategy>().AsTransient();
			base.Bind<ScavengerYielderRetriever>().AsTransient();
			base.Bind<RuinReplacer>().AsSingleton();
			base.Bind<RuinModelFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RuinsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E48 File Offset: 0x00001048
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<RuinSpec, Ruin>();
			builder.AddDecorator<Ruin, AccessibleDemolishableReacher>();
			builder.AddDecorator<Ruin, RuinModels>();
			builder.AddDecorator<Ruin, RuinModelUpdater>();
			builder.AddDecorator<Ruin, DryObject>();
			builder.AddDecorator<Ruin, ContaminatedObject>();
			builder.AddDecorator<Ruin, RuinsRemoveYieldStrategy>();
			builder.AddDecorator<ScavengerSpec, BuildingWithTerrainRange>();
			builder.AddDecorator<ScavengerSpec, AutoEmptiable>();
			builder.AddDecorator<ScavengerSpec, Emptiable>();
			builder.AddDecorator<ScavengerSpec, HaulCandidate>();
			builder.AddDecorator<ScavengerSpec, SimpleOutputInventoryHaulBehaviorProvider>();
			builder.AddDecorator<ScavengerSpec, ScavengerYielderRetriever>();
			RuinsConfigurator.AddDecoratingBehaviors(builder);
			return builder.Build();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002EB3 File Offset: 0x000010B3
		public static void AddDecoratingBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<ScavengerSpec, RemoveUnwantedStockWorkplaceBehavior>();
			builder.AddDecorator<ScavengerSpec, ScavengerWorkplaceBehavior>();
			builder.AddDecorator<ScavengerSpec, EmptyOutputWorkplaceBehavior>();
			builder.AddDecorator<ScavengerSpec, EmptyInventoriesWorkplaceBehavior>();
			builder.AddDecorator<ScavengerSpec, LaborWorkplaceBehavior>();
			builder.AddDecorator<ScavengerSpec, WaitInsideIdlyWorkplaceBehavior>();
		}
	}
}
