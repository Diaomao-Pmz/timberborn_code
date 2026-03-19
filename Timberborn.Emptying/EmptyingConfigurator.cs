using System;
using Bindito.Core;
using Timberborn.Carrying;
using Timberborn.GameDistricts;
using Timberborn.Hauling;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Emptying
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class EmptyingConfigurator : Configurator
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002664 File Offset: 0x00000864
		public override void Configure()
		{
			base.Bind<EmptyInventoriesLaborBehavior>().AsTransient();
			base.Bind<EmptyInventoriesWorkplaceBehavior>().AsTransient();
			base.Bind<EmptyOutputWorkplaceBehavior>().AsTransient();
			base.Bind<RemoveUnwantedStockLaborBehavior>().AsTransient();
			base.Bind<RemoveUnwantedStockWorkplaceBehavior>().AsTransient();
			base.Bind<AutoEmptiable>().AsTransient();
			base.Bind<DistrictEmptiableInventoriesRegistry>().AsTransient();
			base.Bind<DistrictUnwantedStockInventoryRegistry>().AsTransient();
			base.Bind<Emptiable>().AsTransient();
			base.Bind<EmptiableHaulBehaviorProvider>().AsTransient();
			base.Bind<EmptyingStarter>().AsTransient();
			base.Bind<UnwantedStockHaulBehaviorProvider>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(EmptyingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000271E File Offset: 0x0000091E
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GoodCarrier, EmptyingStarter>();
			builder.AddDecorator<Emptiable, HaulCandidate>();
			builder.AddDecorator<Emptiable, EmptiableHaulBehaviorProvider>();
			builder.AddDecorator<Emptiable, UnwantedStockHaulBehaviorProvider>();
			builder.AddDecorator<DistrictBuildingRegistry, DistrictEmptiableInventoriesRegistry>();
			builder.AddDecorator<DistrictInventoryRegistry, DistrictUnwantedStockInventoryRegistry>();
			return builder.Build();
		}
	}
}
