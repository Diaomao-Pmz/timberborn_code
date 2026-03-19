using System;
using Bindito.Core;
using Timberborn.Hauling;
using Timberborn.Stockpiles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class StockpilePrioritySystemConfigurator : Configurator
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002788 File Offset: 0x00000988
		public override void Configure()
		{
			base.Bind<StockpilePriority>().AsTransient();
			base.Bind<ObtainGoodWorkplaceBehavior>().AsTransient();
			base.Bind<SupplyGoodWorkplaceBehavior>().AsTransient();
			base.Bind<GoodObtainer>().AsTransient();
			base.Bind<GoodObtainerStatusInitializer>().AsTransient();
			base.Bind<GoodSupplier>().AsTransient();
			base.Bind<ObtainGoodHaulBehaviorProvider>().AsTransient();
			base.Bind<StockpilePriorityChangeListener>().AsTransient();
			base.Bind<SupplyGoodHaulBehaviorProvider>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(StockpilePrioritySystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002820 File Offset: 0x00000A20
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Stockpile, StockpilePriority>();
			builder.AddDecorator<StockpilePriority, GoodObtainer>();
			builder.AddDecorator<GoodObtainer, ObtainGoodWorkplaceBehavior>();
			builder.AddDecorator<GoodObtainer, GoodObtainerStatusInitializer>();
			builder.AddDecorator<GoodObtainerStatusInitializer, NoHaulingPostStatus>();
			builder.AddDecorator<ObtainGoodWorkplaceBehavior, ObtainGoodHaulBehaviorProvider>();
			builder.AddDecorator<StockpilePriority, GoodSupplier>();
			builder.AddDecorator<GoodSupplier, SupplyGoodWorkplaceBehavior>();
			builder.AddDecorator<SupplyGoodWorkplaceBehavior, SupplyGoodHaulBehaviorProvider>();
			builder.AddDecorator<Stockpile, StockpilePriorityChangeListener>();
			return builder.Build();
		}
	}
}
