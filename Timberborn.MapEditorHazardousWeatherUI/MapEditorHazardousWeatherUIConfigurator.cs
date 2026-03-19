using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000007 RID: 7
	[Context("MapEditor")]
	public class MapEditorHazardousWeatherUIConfigurator : Configurator
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002368 File Offset: 0x00000568
		public override void Configure()
		{
			base.Bind<MapEditorWaterContaminationController>().AsTransient();
			base.Bind<MapEditorWaterStrengthModifier>().AsTransient();
			base.Bind<MapEditorHazardousWeatherWaterSource>().AsTransient();
			base.Bind<MapEditorHazardousWeatherSetter>().AsSingleton();
			base.Bind<HazardousWeatherToggleFactory>().AsSingleton();
			base.Bind<MapEditorHazardousWeatherPanel>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorHazardousWeatherUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023DA File Offset: 0x000005DA
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WaterSource, MapEditorWaterStrengthModifier>();
			builder.AddDecorator<WaterSourceContamination, MapEditorWaterContaminationController>();
			builder.AddDecorator<HazardousWeatherWaterSourceSpec, MapEditorHazardousWeatherWaterSource>();
			return builder.Build();
		}
	}
}
