using System;
using Bindito.Core;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BlockingSystem
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class BlockingSystemConfigurator : Configurator
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002874 File Offset: 0x00000A74
		public override void Configure()
		{
			base.Bind<BlockableObject>().AsTransient();
			base.Bind<BlockableObjectAnimationController>().AsTransient();
			base.Bind<BlockableObjectParticleController>().AsTransient();
			base.Bind<BlockableObjectVisualizer>().AsTransient();
			base.Bind<BlockObjectBelowBlocker>().AsTransient();
			base.Bind<FinishedBlockObjectBelowBlocker>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BlockingSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000028E6 File Offset: 0x00000AE6
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BlockableObjectVisualizerSpec, BlockableObjectVisualizer>();
			builder.AddDecorator<BlockableObjectParticleControllerSpec, BlockableObjectParticleController>();
			builder.AddDecorator<BlockableObjectParticleController, BlockableObject>();
			builder.AddDecorator<BlockableObjectParticleController, ParticlesCache>();
			builder.AddDecorator<BlockableObjectAnimationControllerSpec, BlockableObjectAnimationController>();
			builder.AddDecorator<FinishedBlockObjectBelowBlockerSpec, FinishedBlockObjectBelowBlocker>();
			builder.AddDecorator<FinishedBlockObjectBelowBlocker, BlockObjectBelowBlocker>();
			return builder.Build();
		}
	}
}
