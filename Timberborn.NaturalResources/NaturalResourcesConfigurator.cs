using System;
using Bindito.Core;
using Timberborn.Demolishing;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NaturalResources
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	[Context("MapEditor")]
	public class NaturalResourcesConfigurator : Configurator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000023E0 File Offset: 0x000005E0
		public override void Configure()
		{
			base.Bind<NaturalResource>().AsTransient();
			base.Bind<CoordinatesOffsetter>().AsTransient();
			base.Bind<SpawnValidationService>().AsSingleton();
			base.Bind<NaturalResourceFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NaturalResourcesConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000243A File Offset: 0x0000063A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<NaturalResourceSpec, NaturalResource>();
			builder.AddDecorator<NaturalResourceSpec, Demolishable>();
			builder.AddDecorator<NaturalResourceSpec, CoordinatesOffsetter>();
			builder.AddDecorator<NaturalResourceSpec, LivingNaturalResource>();
			builder.AddDecorator<NaturalResourceSpec, DyingNaturalResource>();
			return builder.Build();
		}
	}
}
