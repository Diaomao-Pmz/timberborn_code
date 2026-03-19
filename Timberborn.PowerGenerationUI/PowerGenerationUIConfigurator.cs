using System;
using Bindito.Core;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;
using Timberborn.Particles;
using Timberborn.PowerGeneration;
using Timberborn.TemplateInstantiation;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class PowerGenerationUIConfigurator : Configurator
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000240C File Offset: 0x0000060C
		public override void Configure()
		{
			base.Bind<GoodPoweredGeneratorAnimator>().AsTransient();
			base.Bind<PowerGeneratorParticleController>().AsTransient();
			base.Bind<WindPoweredGeneratorAnimator>().AsTransient();
			base.Bind<WaterPoweredGeneratorAnimator>().AsTransient();
			base.Bind<WaterPoweredGeneratorPreview>().AsTransient();
			base.Bind<AdjustableStrengthPowerGeneratorFragment>().AsSingleton();
			base.Bind<WaterPoweredGeneratorSpeedCalculator>().AsSingleton();
			base.Bind<WaterPoweredGeneratorPreviewPanel>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PowerGenerationUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<PowerGenerationUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<IDevModule>().To<WaterPoweredGeneratorSpeedChanger>().AsSingleton();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024B8 File Offset: 0x000006B8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WaterPoweredGenerator, WaterPoweredGeneratorAnimator>();
			builder.AddDecorator<PowerGeneratorParticleControllerSpec, PowerGeneratorParticleController>();
			builder.AddDecorator<PowerGeneratorParticleController, ParticlesCache>();
			builder.AddDecorator<GoodPoweredGeneratorAnimatorSpec, GoodPoweredGeneratorAnimator>();
			builder.AddDecorator<WindPoweredGeneratorAnimatorSpec, WindPoweredGeneratorAnimator>();
			builder.AddDecorator<WaterPoweredGenerator, WaterPoweredGeneratorPreview>();
			return builder.Build();
		}

		// Token: 0x0200000B RID: 11
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000026 RID: 38 RVA: 0x000024F0 File Offset: 0x000006F0
			public EntityPanelModuleProvider(AdjustableStrengthPowerGeneratorFragment sliderFragment)
			{
				this._sliderFragment = sliderFragment;
			}

			// Token: 0x06000027 RID: 39 RVA: 0x000024FF File Offset: 0x000006FF
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddBottomFragment(this._sliderFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000011 RID: 17
			public readonly AdjustableStrengthPowerGeneratorFragment _sliderFragment;
		}
	}
}
