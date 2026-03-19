using System;
using Bindito.Core;
using Timberborn.Debugging;
using Timberborn.NaturalResources;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NaturalResourcesReproduction
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class NaturalResourcesReproductionConfigurator : Configurator
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000268C File Offset: 0x0000088C
		public override void Configure()
		{
			base.Bind<DyingNaturalResourceReproducible>().AsTransient();
			base.Bind<LivingReproducible>().AsTransient();
			base.Bind<Reproducible>().AsTransient();
			base.Bind<NaturalResourceReproducer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NaturalResourcesReproductionConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<IDevModule>().To<PotentialSpotsToggler>().AsSingleton();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026F7 File Offset: 0x000008F7
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<NaturalResourceSpec, Reproducible>();
			builder.AddDecorator<DyingNaturalResource, DyingNaturalResourceReproducible>();
			builder.AddDecorator<LivingNaturalResource, LivingReproducible>();
			return builder.Build();
		}
	}
}
