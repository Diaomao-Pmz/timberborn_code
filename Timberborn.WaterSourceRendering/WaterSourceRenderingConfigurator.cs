using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.WaterSourceRendering
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	[Context("MapEditor")]
	public class WaterSourceRenderingConfigurator : Configurator
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002384 File Offset: 0x00000584
		public override void Configure()
		{
			base.Bind<WaterSourceRenderer>().AsTransient();
			base.Bind<WaterSourceRenderingService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WaterSourceRenderingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023BB File Offset: 0x000005BB
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WaterSourceRendererSpec, WaterSourceRenderer>();
			return builder.Build();
		}
	}
}
