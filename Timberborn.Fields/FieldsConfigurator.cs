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

namespace Timberborn.Fields
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	public class FieldsConfigurator : Configurator
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002718 File Offset: 0x00000918
		public override void Configure()
		{
			base.Bind<FarmHouseGoodStackRetrieverWorkplaceBehavior>().AsTransient();
			base.Bind<FarmHouseWorkplaceBehavior>().AsTransient();
			base.Bind<Crop>().AsTransient();
			base.Bind<FarmHouse>().AsTransient();
			base.Bind<FarmHouseYielderRetriever>().AsTransient();
			base.Bind<HarvestStarter>().AsTransient();
			base.Bind<GoodStackService<FarmHouse>>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(FieldsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002798 File Offset: 0x00000998
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<CropSpec, Crop>();
			builder.AddDecorator<FarmHouseSpec, FarmHouse>();
			builder.AddDecorator<FarmHouse, PlantablePrioritizer>();
			builder.AddDecorator<FarmHouse, BuildingWithTerrainRange>();
			builder.AddDecorator<FarmHouse, AutoEmptiable>();
			builder.AddDecorator<FarmHouse, Emptiable>();
			builder.AddDecorator<FarmHouse, HaulCandidate>();
			builder.AddDecorator<FarmHouse, SimpleOutputInventoryHaulBehaviorProvider>();
			builder.AddDecorator<FarmHouse, FarmHouseYielderRetriever>();
			builder.AddDecorator<Worker, HarvestStarter>();
			FieldsConfigurator.InitializeBehaviors(builder);
			return builder.Build();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027F1 File Offset: 0x000009F1
		public static void InitializeBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<FarmHouse, RemoveUnwantedStockWorkplaceBehavior>();
			builder.AddDecorator<FarmHouse, FarmHouseGoodStackRetrieverWorkplaceBehavior>();
			builder.AddDecorator<FarmHouse, FarmHouseWorkplaceBehavior>();
			builder.AddDecorator<FarmHouse, EmptyOutputWorkplaceBehavior>();
			builder.AddDecorator<FarmHouse, EmptyInventoriesWorkplaceBehavior>();
			builder.AddDecorator<FarmHouse, LaborWorkplaceBehavior>();
			builder.AddDecorator<FarmHouse, WaitInsideIdlyWorkplaceBehavior>();
		}
	}
}
