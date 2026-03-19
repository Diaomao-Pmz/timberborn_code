using System;
using Bindito.Core;
using Timberborn.DuplicationSystem;
using Timberborn.Emptying;
using Timberborn.Hauling;
using Timberborn.InventoryNeedSystem;
using Timberborn.Stockpiles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.GameStockpiles
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class GameStockpilesConfigurator : Configurator
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002109 File Offset: 0x00000309
		public override void Configure()
		{
			base.Bind<FixedStockpileRemover>().AsTransient();
			base.Bind<UnreachableFixedStockpileStatus>().AsTransient();
			base.Bind<StockpileInventoryBehaviorInitializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<GameStockpilesConfigurator.TemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x02000006 RID: 6
		public class TemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x06000008 RID: 8 RVA: 0x00002148 File Offset: 0x00000348
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				builder.AddDecorator<Stockpile, Emptiable>();
				builder.AddDecorator<Stockpile, HaulCandidate>();
				builder.AddDecorator<Stockpile, InventoryNeedBehavior>();
				builder.AddDecorator<Stockpile, EmptyInventoriesWorkplaceBehavior>();
				builder.AddDecorator<Stockpile, RemoveUnwantedStockWorkplaceBehavior>();
				builder.AddDecorator<FixedStockpileSpec, FixedStockpileRemover>();
				builder.AddDecorator<FixedStockpileSpec, UnreachableFixedStockpileStatus>();
				builder.AddDecorator<FixedStockpileSpec, DuplicationBlocker>();
				return builder.Build();
			}
		}
	}
}
