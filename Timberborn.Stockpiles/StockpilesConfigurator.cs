using System;
using Bindito.Core;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Stockpiles
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	[Context("MapEditor")]
	public class StockpilesConfigurator : Configurator
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000023FE File Offset: 0x000005FE
		public override void Configure()
		{
			base.Bind<Stockpile>().AsTransient();
			base.Bind<FixedStockpile>().AsTransient();
			base.Bind<StockpileInventoryInitializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<StockpilesConfigurator.TemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000C RID: 12
		public class TemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x06000029 RID: 41 RVA: 0x0000243D File Offset: 0x0000063D
			public TemplateModuleProvider(StockpileInventoryInitializer stockpileInventoryInitializer)
			{
				this._stockpileInventoryInitializer = stockpileInventoryInitializer;
			}

			// Token: 0x0600002A RID: 42 RVA: 0x0000244C File Offset: 0x0000064C
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				builder.AddDecorator<StockpileSpec, Stockpile>();
				builder.AddDecorator<Stockpile, SingleGoodAllower>();
				builder.AddDecorator<FixedStockpileSpec, FixedStockpile>();
				builder.AddDedicatedDecorator<Stockpile, Inventory>(this._stockpileInventoryInitializer);
				return builder.Build();
			}

			// Token: 0x04000011 RID: 17
			public readonly StockpileInventoryInitializer _stockpileInventoryInitializer;
		}
	}
}
