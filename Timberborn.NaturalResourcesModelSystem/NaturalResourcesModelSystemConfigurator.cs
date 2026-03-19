using System;
using Bindito.Core;
using Timberborn.NaturalResources;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NaturalResourcesModelSystem
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	[Context("MapEditor")]
	public class NaturalResourcesModelSystemConfigurator : Configurator
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002B34 File Offset: 0x00000D34
		public override void Configure()
		{
			base.Bind<NaturalResourceModelRandomizer>().AsTransient();
			base.Bind<NaturalResourceCenterProvider>().AsTransient();
			base.Bind<NaturalResourceModel>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NaturalResourcesModelSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B82 File Offset: 0x00000D82
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<NaturalResourceSpec, NaturalResourceModel>();
			builder.AddDecorator<NaturalResourceSpec, NaturalResourceCenterProvider>();
			builder.AddDecorator<NaturalResourceModelRandomizerSpec, NaturalResourceModelRandomizer>();
			return builder.Build();
		}
	}
}
