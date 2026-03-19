using System;
using Bindito.Core;
using Timberborn.Particles;
using Timberborn.Planting;
using Timberborn.TemplateInstantiation;

namespace Timberborn.PlantingEffects
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class PlantingEffectsConfigurator : Configurator
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000216F File Offset: 0x0000036F
		public override void Configure()
		{
			base.Bind<PlantingParticleController>().AsTransient();
			base.Bind<PlantingAnimationController>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PlantingEffectsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A6 File Offset: 0x000003A6
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Planter, PlantingAnimationController>();
			builder.AddDecorator<PlantingParticleControllerSpec, PlantingParticleController>();
			builder.AddDecorator<PlantingParticleController, ParticlesCache>();
			return builder.Build();
		}
	}
}
