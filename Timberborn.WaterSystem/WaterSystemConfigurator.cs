using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200003D RID: 61
	[Context("Game")]
	[Context("MapEditor")]
	public class WaterSystemConfigurator : Configurator
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00007D4C File Offset: 0x00005F4C
		public override void Configure()
		{
			base.Bind<WaterMapBoundary>().AsTransient();
			base.Bind<WaterSimulator>().AsSingleton();
			base.Bind<ThreadSafeWaterMap>().AsSingleton();
			base.Bind<IThreadSafeWaterMap>().ToExisting<ThreadSafeWaterMap>();
			base.Bind<WaterSourceRegistry>().AsSingleton();
			base.Bind<IWaterService>().To<WaterService>().AsSingleton();
			base.Bind<INonThreadSafeWaterService>().To<NonThreadSafeWaterService>().AsSingleton();
			base.Bind<WaterChangeService>().AsSingleton();
			base.Bind<WaterMapLoader>().AsSingleton();
			base.Bind<ColumnOutflowsPackedListSerializer>().AsSingleton();
			base.Bind<WaterColumnPackedListSerializer>().AsSingleton();
			base.Bind<FlowVectorCalculator>().AsSingleton();
			base.Bind<FlowLimiterService>().AsSingleton();
			base.Bind<IFlowLimiterService>().ToExisting<FlowLimiterService>();
			base.Bind<WaterMapBoundaryService>().AsSingleton();
			base.Bind<WaterColumnRetriever>().AsSingleton();
			base.Bind<WaterFlowRetriever>().AsSingleton();
			base.Bind<FlowLimitCalculator>().AsSingleton();
			base.Bind<WaterSimulationTaskStarter>().AsSingleton();
			base.Bind<WaterDepthSetter>().AsSingleton();
			base.Bind<MutableWaterColumnRetriever>().AsSingleton();
			base.Bind<WaterSimulationMigrator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WaterSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007E88 File Offset: 0x00006088
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<IWaterSource, WaterMapBoundary>();
			return builder.Build();
		}
	}
}
