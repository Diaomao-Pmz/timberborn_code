using System;
using Bindito.Core;
using Timberborn.NaturalResourcesContamination;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NaturalResourcesContaminationUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class NaturalResourcesContaminationUIConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021BE File Offset: 0x000003BE
		public override void Configure()
		{
			base.Bind<ContaminatedNaturalResourceStatus>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NaturalResourcesContaminationUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E9 File Offset: 0x000003E9
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ContaminatedNaturalResource, ContaminatedNaturalResourceStatus>();
			return builder.Build();
		}
	}
}
