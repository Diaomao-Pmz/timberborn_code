using System;
using Bindito.Core;
using Timberborn.NaturalResources;
using Timberborn.SoilContaminationSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NaturalResourcesContamination
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class NaturalResourcesContaminationConfigurator : Configurator
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000024A8 File Offset: 0x000006A8
		public override void Configure()
		{
			base.Bind<ContaminatedNaturalResource>().AsTransient();
			base.MultiBind<ISpawnValidator>().To<ContaminatedNaturalResourceSpawnValidator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NaturalResourcesContaminationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024E4 File Offset: 0x000006E4
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ContaminatedNaturalResourceSpec, ContaminatedNaturalResource>();
			builder.AddDecorator<ContaminatedNaturalResource, ContaminatedObject>();
			return builder.Build();
		}
	}
}
