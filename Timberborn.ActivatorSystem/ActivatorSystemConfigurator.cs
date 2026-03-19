using System;
using Bindito.Core;
using Timberborn.Particles;
using Timberborn.StatusSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ActivatorSystem
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	[Context("MapEditor")]
	public class ActivatorSystemConfigurator : Configurator
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000029B4 File Offset: 0x00000BB4
		public override void Configure()
		{
			base.Bind<TimedComponentActivator>().AsTransient();
			base.Bind<ActivationWarningStatus>().AsTransient();
			base.Bind<ActivationProgressParticles>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ActivatorSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A02 File Offset: 0x00000C02
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<TimedComponentActivatorSpec, TimedComponentActivator>();
			builder.AddDecorator<ActivationWarningStatusSpec, ActivationWarningStatus>();
			builder.AddDecorator<ActivationWarningStatus, StatusSubject>();
			builder.AddDecorator<ActivationProgressParticlesSpec, ActivationProgressParticles>();
			builder.AddDecorator<ActivationProgressParticles, ParticlesCache>();
			return builder.Build();
		}
	}
}
