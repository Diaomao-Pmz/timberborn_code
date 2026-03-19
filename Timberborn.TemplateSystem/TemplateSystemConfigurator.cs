using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.TemplateSystem
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class TemplateSystemConfigurator : Configurator
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000270C File Offset: 0x0000090C
		public override void Configure()
		{
			base.Bind<InstantiatedTemplate>().AsTransient();
			base.Bind<TemplateNameRetriever>().AsSingleton();
			base.Bind<TemplateNameMapper>().AsSingleton();
			base.Bind<TemplateInstantiationOrderService>().AsSingleton();
			base.Bind<TemplateService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(TemplateSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002772 File Offset: 0x00000972
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<TemplateSpec, InstantiatedTemplate>();
			return builder.Build();
		}
	}
}
