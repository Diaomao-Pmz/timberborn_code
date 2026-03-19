using System;
using Bindito.Core;
using Timberborn.Beavers;
using Timberborn.Forestry;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ForestryEffects
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class ForestryEffectsConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public override void Configure()
		{
			base.Bind<TreeCutterParticleController>().AsTransient();
			base.Bind<TreeCutterSideRandomizer>().AsTransient();
			base.Bind<TreeCutterSwimmingBlocker>().AsTransient();
			base.Bind<TreeShaker>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ForestryEffectsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000215A File Offset: 0x0000035A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<TreeComponent, TreeShaker>();
			builder.AddDecorator<TreeCutter, TreeCutterSwimmingBlocker>();
			builder.AddDecorator<AdultSpec, TreeCutterSideRandomizer>();
			builder.AddDecorator<TreeCutterParticleControllerSpec, TreeCutterParticleController>();
			builder.AddDecorator<TreeCutterParticleController, ParticlesCache>();
			return builder.Build();
		}
	}
}
