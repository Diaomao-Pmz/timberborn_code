using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Carrying
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	[Context("MapEditor")]
	public class CarryingConfigurator : Configurator
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000026F8 File Offset: 0x000008F8
		public override void Configure()
		{
			base.Bind<CarryRootBehavior>().AsTransient();
			base.Bind<BackpackCarrier>().AsTransient();
			base.Bind<CarrierInventoryFinder>().AsTransient();
			base.Bind<GoodCarrier>().AsTransient();
			base.Bind<GoodCarrierAnimator>().AsTransient();
			base.Bind<GoodCarrierCapacityReserver>().AsTransient();
			base.Bind<GoodCarrierModel>().AsTransient();
			base.Bind<Overburdenable>().AsTransient();
			base.Bind<CarryAmountCalculator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CarryingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000278E File Offset: 0x0000098E
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GoodCarrierSpec, GoodCarrier>();
			builder.AddDecorator<GoodCarrier, BackpackCarrier>();
			builder.AddDecorator<GoodCarrier, GoodCarrierAnimator>();
			builder.AddDecorator<GoodCarrier, CarrierInventoryFinder>();
			builder.AddDecorator<GoodCarrier, GoodCarrierCapacityReserver>();
			builder.AddDecorator<OverburdenableSpec, Overburdenable>();
			builder.AddDecorator<GoodCarrierModelSpec, GoodCarrierModel>();
			return builder.Build();
		}
	}
}
