using System;
using Bindito.Core;
using Timberborn.BlockingSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000024 RID: 36
	[Context("Game")]
	[Context("MapEditor")]
	public class WaterSourceSystemConfigurator : Configurator
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00003F04 File Offset: 0x00002104
		public override void Configure()
		{
			base.Bind<BadtideWaterSourceContaminationController>().AsTransient();
			base.Bind<WaterSource>().AsTransient();
			base.Bind<DroughtWaterStrengthModifier>().AsTransient();
			base.Bind<WaterDepthStrengthModifier>().AsTransient();
			base.Bind<WaterSourceContamination>().AsTransient();
			base.Bind<WaterSourceDisabler>().AsTransient();
			base.Bind<WaterSourceDischarger>().AsTransient();
			base.Bind<WaterSourceRegulator>().AsTransient();
			base.Bind<WaterSourceRegulatorAnimationController>().AsTransient();
			base.Bind<UnderlyingWaterSource>().AsTransient();
			base.Bind<HazardousWeatherObserver>().AsTransient();
			base.Bind<RegulatedWaterSourceBlocker>().AsTransient();
			base.Bind<DirectionalWaterSource>().AsTransient();
			base.Bind<WaterStrengthService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WaterSourceSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003FD8 File Offset: 0x000021D8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WaterSourceSpec, WaterSource>();
			builder.AddDecorator<WaterSource, DroughtWaterStrengthModifier>();
			builder.AddDecorator<WaterSourceRegulator, UnderlyingWaterSource>();
			builder.AddDecorator<WaterSourceDisablerSpec, WaterSourceDisabler>();
			builder.AddDecorator<WaterSourceDisabler, UnderlyingWaterSource>();
			builder.AddDecorator<WaterSourceDischargerSpec, WaterSourceDischarger>();
			builder.AddDecorator<WaterSourceDischarger, UnderlyingWaterSource>();
			builder.AddDecorator<WaterSourceContaminationSpec, WaterSourceContamination>();
			builder.AddDecorator<WaterSourceRegulatorSpec, WaterSourceRegulator>();
			builder.AddDecorator<WaterSourceRegulatorAnimationControllerSpec, WaterSourceRegulatorAnimationController>();
			builder.AddDecorator<BadtideWaterSourceContaminationControllerSpec, BadtideWaterSourceContaminationController>();
			builder.AddDecorator<BadtideWaterSourceContaminationController, HazardousWeatherObserver>();
			builder.AddDecorator<WaterDepthStrengthModifierSpec, WaterDepthStrengthModifier>();
			builder.AddDecorator<RegulatedWaterSourceBlockerSpec, RegulatedWaterSourceBlocker>();
			builder.AddDecorator<RegulatedWaterSourceBlocker, BlockObjectBelowBlocker>();
			builder.AddDecorator<DirectionalWaterSourceSpec, DirectionalWaterSource>();
			return builder.Build();
		}
	}
}
