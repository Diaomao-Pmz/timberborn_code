using System;
using Bindito.Core;
using Timberborn.Emptying;
using Timberborn.Hauling;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;
using Timberborn.Workshops;

namespace Timberborn.GoodConsumingBuildingSystem
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	public class GoodConsumingBuildingSystemConfigurator : Configurator
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002CCC File Offset: 0x00000ECC
		public override void Configure()
		{
			base.Bind<GoodConsumingBuilding>().AsTransient();
			base.Bind<PoweredGoodConsumingBuilding>().AsTransient();
			base.Bind<GoodConsumingBuildingStatusInitializer>().AsTransient();
			base.Bind<GoodConsumingBuildingInventoryInitializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<GoodConsumingBuildingSystemConfigurator.TemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x02000010 RID: 16
		public class TemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x0600005C RID: 92 RVA: 0x00002D22 File Offset: 0x00000F22
			public TemplateModuleProvider(GoodConsumingBuildingInventoryInitializer goodConsumingBuildingInventoryInitializer)
			{
				this._goodConsumingBuildingInventoryInitializer = goodConsumingBuildingInventoryInitializer;
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00002D34 File Offset: 0x00000F34
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				builder.AddDecorator<GoodConsumingBuildingSpec, GoodConsumingBuilding>();
				builder.AddDecorator<GoodConsumingBuilding, AutoEmptiable>();
				builder.AddDecorator<GoodConsumingBuilding, Emptiable>();
				builder.AddDecorator<GoodConsumingBuilding, FillInputHaulBehaviorProvider>();
				builder.AddDecorator<GoodConsumingBuilding, GoodConsumingBuildingStatusInitializer>();
				builder.AddDecorator<GoodConsumingBuildingStatusInitializer, LackOfResourcesStatus>();
				builder.AddDecorator<GoodConsumingBuildingStatusInitializer, NoHaulingPostStatus>();
				builder.AddDecorator<PoweredGoodConsumingBuildingSpec, PoweredGoodConsumingBuilding>();
				builder.AddDedicatedDecorator<GoodConsumingBuilding, Inventory>(this._goodConsumingBuildingInventoryInitializer);
				GoodConsumingBuildingSystemConfigurator.TemplateModuleProvider.InitializeBehaviors(builder);
				return builder.Build();
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00002D8D File Offset: 0x00000F8D
			public static void InitializeBehaviors(TemplateModule.Builder builder)
			{
				builder.AddDecorator<GoodConsumingBuilding, EmptyInventoriesWorkplaceBehavior>();
				builder.AddDecorator<GoodConsumingBuilding, FillInputWorkplaceBehavior>();
				builder.AddDecorator<GoodConsumingBuilding, RemoveUnwantedStockWorkplaceBehavior>();
			}

			// Token: 0x04000026 RID: 38
			public readonly GoodConsumingBuildingInventoryInitializer _goodConsumingBuildingInventoryInitializer;
		}
	}
}
