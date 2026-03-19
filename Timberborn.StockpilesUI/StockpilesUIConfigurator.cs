using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Illumination;
using Timberborn.Stockpiles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200002A RID: 42
	[Context("Game")]
	public class StockpilesUIConfigurator : Configurator
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00004C40 File Offset: 0x00002E40
		public override void Configure()
		{
			base.Bind<StockpileDropdownProvider>().AsTransient();
			base.Bind<StockpileOverlayItemAdder>().AsTransient();
			base.Bind<NoGoodAllowedStatus>().AsTransient();
			base.Bind<UnwantedStockStatus>().AsTransient();
			base.Bind<StockpileDescriber>().AsTransient();
			base.Bind<StockpileInventoryFragment>().AsSingleton();
			base.Bind<StockpileBatchControlRowItemFactory>().AsSingleton();
			base.Bind<IGoodSelectionController>().To<GoodSelectionController>().AsSingleton();
			base.Bind<StockpileOverlay>().AsSingleton();
			base.Bind<StockpileOverlayShower>().AsSingleton();
			base.Bind<StockpileGoodSelectionBoxFactory>().AsSingleton();
			base.Bind<StockpileOverlayTogglePanel>().AsSingleton();
			base.Bind<StockpileOverlayHider>().AsSingleton();
			base.Bind<StockpileInventoryDebugFragment>().AsSingleton();
			base.Bind<GoodSelectionBoxRowFactory>().AsSingleton();
			base.Bind<GoodSelectionBoxItemFactory>().AsSingleton();
			base.Bind<StockpileOptionsService>().AsSingleton();
			base.Bind<StockpileGoodSelectionBoxItemsFactory>().AsSingleton();
			base.Bind<GoodStockpilesTooltipFactory>().AsSingleton();
			base.Bind<OverlayGoodSelectionController>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(StockpilesUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<StockpilesUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004D70 File Offset: 0x00002F70
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Stockpile, StockpileDropdownProvider>();
			builder.AddDecorator<Stockpile, StockpileOverlayItemAdder>();
			builder.AddDecorator<Stockpile, StockpileDescriber>();
			builder.AddDecorator<Stockpile, NoGoodAllowedStatus>();
			builder.AddDecorator<Stockpile, UnwantedStockStatus>();
			builder.AddDecorator<StockpileIlluminatorSpec, Illuminator>();
			return builder.Build();
		}

		// Token: 0x0200002B RID: 43
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000101 RID: 257 RVA: 0x00004DA8 File Offset: 0x00002FA8
			public EntityPanelModuleProvider(StockpileInventoryFragment stockpileInventoryFragment, StockpileInventoryDebugFragment stockpileInventoryDebugFragment)
			{
				this._stockpileInventoryFragment = stockpileInventoryFragment;
				this._stockpileInventoryDebugFragment = stockpileInventoryDebugFragment;
			}

			// Token: 0x06000102 RID: 258 RVA: 0x00004DBE File Offset: 0x00002FBE
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddBottomFragment(this._stockpileInventoryFragment, 0);
				builder.AddDiagnosticFragment(this._stockpileInventoryDebugFragment);
				return builder.Build();
			}

			// Token: 0x040000C4 RID: 196
			public readonly StockpileInventoryFragment _stockpileInventoryFragment;

			// Token: 0x040000C5 RID: 197
			public readonly StockpileInventoryDebugFragment _stockpileInventoryDebugFragment;
		}
	}
}
