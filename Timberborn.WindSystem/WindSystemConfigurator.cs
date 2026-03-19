using System;
using Bindito.Core;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.WindSystem
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	[Context("MapEditor")]
	public class WindSystemConfigurator : Configurator
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000030E4 File Offset: 0x000012E4
		public override void Configure()
		{
			base.Bind<BlockableBuildingWindAnimator>().AsTransient();
			base.Bind<WindParticleController>().AsTransient();
			base.Bind<WindRotationAnimator>().AsTransient();
			base.Bind<WindService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WindSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000313E File Offset: 0x0000133E
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WindParticleControllerSpec, WindParticleController>();
			builder.AddDecorator<WindParticleControllerSpec, ParticlesCache>();
			builder.AddDecorator<WindRotationAnimatorSpec, WindRotationAnimator>();
			builder.AddDecorator<BlockableBuildingWindAnimatorSpec, BlockableBuildingWindAnimator>();
			return builder.Build();
		}
	}
}
