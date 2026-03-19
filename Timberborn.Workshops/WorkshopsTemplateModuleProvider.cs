using System;
using Bindito.Core;
using Timberborn.Emptying;
using Timberborn.Hauling;
using Timberborn.InventoryNeedSystem;
using Timberborn.InventorySystem;
using Timberborn.LaborSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000031 RID: 49
	public class WorkshopsTemplateModuleProvider : IProvider<TemplateModule>
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00005A99 File Offset: 0x00003C99
		public WorkshopsTemplateModuleProvider(ManufactoryInventoryInitializer manufactoryInventoryInitializer)
		{
			this._manufactoryInventoryInitializer = manufactoryInventoryInitializer;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public TemplateModule Get()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDedicatedDecorator<Manufactory, Inventory>(this._manufactoryInventoryInitializer);
			builder.AddDecorator<ManufactorySpec, Manufactory>();
			builder.AddDecorator<Manufactory, AutoEmptiable>();
			builder.AddDecorator<Manufactory, Emptiable>();
			builder.AddDecorator<Manufactory, HaulCandidate>();
			builder.AddDecorator<Manufactory, ManufactoryHaulBehaviorProvider>();
			builder.AddDecorator<Manufactory, ManufactoryInputChecker>();
			builder.AddDecorator<ManufactoryInputChecker, LackOfResourcesStatus>();
			builder.AddDecorator<Manufactory, NoRecipeStatus>();
			builder.AddDecorator<Manufactory, RecipeGoodDisallower>();
			builder.AddDecorator<Manufactory, InventoryNeedBehavior>();
			builder.AddDecorator<WorkshopSpec, Workshop>();
			builder.AddDecorator<Workshop, WorkshopProductivityCounter>();
			builder.AddDecorator<Workshop, WorkplacePowerConsumptionSwitch>();
			builder.AddDecorator<Worker, WorkplaceWorkStarter>();
			builder.AddDecorator<ManufactoryTogglableRecipesSpec, ManufactoryTogglableRecipes>();
			builder.AddDecorator<ProductionResetterSpec, ProductionResetter>();
			builder.AddDecorator<ProductionIncreaserSpec, ProductionIncreaser>();
			WorkshopsTemplateModuleProvider.InitializeBehaviors(builder);
			return builder.Build();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005B37 File Offset: 0x00003D37
		public static void InitializeBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<SimpleManufactoryBehaviorsSpec, ProduceWorkplaceBehavior>();
			builder.AddDecorator<SimpleManufactoryBehaviorsSpec, EmptyOutputWorkplaceBehavior>();
			builder.AddDecorator<SimpleManufactoryBehaviorsSpec, FillInputWorkplaceBehavior>();
			builder.AddDecorator<SimpleManufactoryBehaviorsSpec, EmptyInventoriesWorkplaceBehavior>();
			builder.AddDecorator<SimpleManufactoryBehaviorsSpec, RemoveUnwantedStockWorkplaceBehavior>();
			builder.AddDecorator<SimpleManufactoryBehaviorsSpec, LaborWorkplaceBehavior>();
			builder.AddDecorator<SimpleManufactoryBehaviorsSpec, WaitInsideIdlyWorkplaceBehavior>();
		}

		// Token: 0x040000A6 RID: 166
		public readonly ManufactoryInventoryInitializer _manufactoryInventoryInitializer;
	}
}
