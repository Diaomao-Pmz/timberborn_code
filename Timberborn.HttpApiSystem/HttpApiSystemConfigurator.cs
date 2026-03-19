using System;
using Bindito.Core;
using Timberborn.Automation;
using Timberborn.EntityNaming;
using Timberborn.TemplateInstantiation;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200001E RID: 30
	[Context("Game")]
	public class HttpApiSystemConfigurator : Configurator
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000044EC File Offset: 0x000026EC
		public override void Configure()
		{
			base.Bind<HttpAdapter>().AsTransient();
			base.Bind<HttpLever>().AsTransient();
			base.Bind<HttpApiController>().AsTransient();
			base.Bind<HttpApi>().AsSingleton();
			base.Bind<HttpApiIntermediary>().AsSingleton();
			base.Bind<HttpWebhookCaller>().AsSingleton();
			base.Bind<HttpWebhookRegistry>().AsSingleton();
			base.Bind<HttpApiCacheBuster>().AsSingleton();
			base.Bind<HttpApiUrlGenerator>().AsTransient();
			base.MultiBind<IHttpApiEndpoint>().To<IndexHtmlEndpoint>().AsSingleton();
			base.MultiBind<IHttpApiEndpoint>().To<StaticFilesEndpoint>().AsSingleton();
			base.MultiBind<IHttpApiEndpoint>().To<HttpAdaptersJsonEndpoint>().AsSingleton();
			base.MultiBind<IHttpApiEndpoint>().To<HttpLeverJsonEndpoint>().AsSingleton();
			base.MultiBind<IHttpApiPageSection>().To<HttpLeversPageSection>().AsSingleton();
			base.MultiBind<IHttpApiPageSection>().To<HttpAdapterPageSection>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(HttpApiSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000045E8 File Offset: 0x000027E8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<HttpAdapterSpec, HttpAdapter>();
			builder.AddDecorator<HttpAdapter, HttpApiController>();
			builder.AddDecorator<HttpAdapter, AutomatorIlluminator>();
			builder.AddDecorator<HttpAdapter, NumberedEntityNamer>();
			builder.AddDecorator<HttpAdapter, UniquelyNamedEntity>();
			builder.AddDecorator<HttpAdapter, Automatable>();
			builder.AddDecorator<HttpLeverSpec, HttpLever>();
			builder.AddDecorator<HttpLever, HttpApiController>();
			builder.AddDecorator<HttpLever, AutomatorIlluminator>();
			builder.AddDecorator<HttpLever, UniquelyNamedEntity>();
			return builder.Build();
		}
	}
}
