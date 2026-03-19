using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.WaterSourceSystem;

namespace Timberborn.GameWaterSourceSystem
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class GameWaterSourceSystemConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public override void Configure()
		{
			base.Bind<UndergroundWaterSource>().AsTransient();
			base.Bind<UndergroundWaterSourceDrill>().AsTransient();
			base.Bind<WaterSourceActivator>().AsTransient();
			base.Bind<WaterSourceActivatorOverrider>().AsTransient();
			base.Bind<HazardousWeatherWaterSource>().AsTransient();
			base.Bind<UndergroundWaterSourceDrillSounds>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(GameWaterSourceSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002174 File Offset: 0x00000374
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<UndergroundWaterSourceDrillSpec, UndergroundWaterSourceDrill>();
			builder.AddDecorator<UndergroundWaterSourceDrill, UnderlyingWaterSource>();
			builder.AddDecorator<UndergroundWaterSourceDrill, HazardousWeatherObserver>();
			builder.AddDecorator<UndergroundWaterSourceDrill, UndergroundWaterSourceDrillSounds>();
			builder.AddDecorator<UndergroundWaterSourceSpec, UndergroundWaterSource>();
			builder.AddDecorator<UndergroundWaterSource, HazardousWeatherObserver>();
			builder.AddDecorator<WaterSource, WaterSourceActivator>();
			builder.AddDecorator<WaterSourceDischargerSpec, WaterSourceActivatorOverrider>();
			builder.AddDecorator<HazardousWeatherWaterSourceSpec, HazardousWeatherWaterSource>();
			return builder.Build();
		}
	}
}
