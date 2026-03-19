using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Particles
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	[Context("MapEditor")]
	public class ParticlesConfigurator : Configurator
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public override void Configure()
		{
			base.Bind<FinishedStateParticlesSpeedMultiplier>().AsTransient();
			base.Bind<NonLinearParticlesSpeedMultiplier>().AsTransient();
			base.Bind<AnimationParticlesTrigger>().AsTransient();
			base.Bind<ParticlesCache>().AsTransient();
			base.Bind<ParticlesRunnerCreator>().AsTransient();
			base.Bind<ParticlesFastForwarder>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ParticlesConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B3A File Offset: 0x00000D3A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<AnimationParticlesTriggerSpec, AnimationParticlesTrigger>();
			builder.AddDecorator<ParticlesCache, ParticlesRunnerCreator>();
			builder.AddDecorator<ParticlesRunnerCreator, NonLinearParticlesSpeedMultiplier>();
			builder.AddDecorator<ParticlesRunnerCreator, FinishedStateParticlesSpeedMultiplier>();
			builder.AddDecorator<AnimationParticlesTrigger, ParticlesRunnerCreator>();
			return builder.Build();
		}
	}
}
