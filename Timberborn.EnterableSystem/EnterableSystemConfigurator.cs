using System;
using Bindito.Core;
using Timberborn.Characters;
using Timberborn.Illumination;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	public class EnterableSystemConfigurator : Configurator
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002EF0 File Offset: 0x000010F0
		public override void Configure()
		{
			base.Bind<Enterable>().AsTransient();
			base.Bind<EnterableAnimationController>().AsTransient();
			base.Bind<EnterableIlluminator>().AsTransient();
			base.Bind<EnterableParticleController>().AsTransient();
			base.Bind<EnterableSounds>().AsTransient();
			base.Bind<Enterer>().AsTransient();
			base.Bind<EntererBoundsScaler>().AsTransient();
			base.Bind<EntererStatusIconHider>().AsTransient();
			base.Bind<RangeEnterableHighlighter>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(EnterableSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002F88 File Offset: 0x00001188
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, Enterer>();
			builder.AddDecorator<Enterer, EntererStatusIconHider>();
			builder.AddDecorator<EnterableSpec, Enterable>();
			builder.AddDecorator<EnterableIlluminatorSpec, EnterableIlluminator>();
			builder.AddDecorator<EnterableIlluminator, Illuminator>();
			builder.AddDecorator<EnterableAnimationControllerSpec, EnterableAnimationController>();
			builder.AddDecorator<EnterableParticleControllerSpec, EnterableParticleController>();
			builder.AddDecorator<EnterableParticleController, ParticlesCache>();
			builder.AddDecorator<EntererBoundsScalerSpec, EntererBoundsScaler>();
			return builder.Build();
		}
	}
}
