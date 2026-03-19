using System;
using Bindito.Core;
using Timberborn.Rendering;
using Timberborn.Stockpiles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x0200001F RID: 31
	[Context("Game")]
	[Context("MapEditor")]
	public class StockpileVisualizationConfigurator : Configurator
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00004704 File Offset: 0x00002904
		public override void Configure()
		{
			base.Bind<GoodVisualization>().AsTransient();
			base.Bind<StockpileBannerSetter>().AsTransient();
			base.Bind<StockpileGoodColumnVisualizer>().AsTransient();
			base.Bind<StockpileGoodPileVisualizer>().AsTransient();
			base.Bind<StockpilePlaneVisualizer>().AsTransient();
			base.Bind<StockpileVisualizers>().AsTransient();
			base.Bind<StockpileVisualizationUpdater>().AsTransient();
			base.Bind<GoodVisualizationSpecService>().AsSingleton();
			base.Bind<GoodColumnVariantsService>().AsSingleton();
			base.Bind<GoodPileVariantsService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(StockpileVisualizationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000047A6 File Offset: 0x000029A6
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Stockpile, StockpileBannerSetter>();
			builder.AddDecorator<Stockpile, StockpileVisualizationUpdater>();
			builder.AddDecorator<IStockpileVisualizer, StockpileVisualizers>();
			builder.AddDecorator<IStockpileVisualizer, EntityMaterials>();
			builder.AddDecorator<StockpileVisualizers, GoodVisualization>();
			builder.AddDecorator<StockpileGoodColumnVisualizerSpec, StockpileGoodColumnVisualizer>();
			builder.AddDecorator<StockpileGoodPileVisualizerSpec, StockpileGoodPileVisualizer>();
			builder.AddDecorator<StockpilePlaneVisualizerSpec, StockpilePlaneVisualizer>();
			return builder.Build();
		}
	}
}
