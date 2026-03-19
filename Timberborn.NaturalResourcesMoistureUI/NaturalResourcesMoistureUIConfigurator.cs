using System;
using Bindito.Core;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NaturalResourcesMoistureUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class NaturalResourcesMoistureUIConfigurator : Configurator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000235C File Offset: 0x0000055C
		public override void Configure()
		{
			base.Bind<FloodableNaturalResourceDescriber>().AsTransient();
			base.Bind<LivingWaterNaturalResourceStatus>().AsTransient();
			base.Bind<WateredNaturalResourceStatus>().AsTransient();
			base.Bind<WateredNaturalResourceDescriber>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NaturalResourcesMoistureUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023B6 File Offset: 0x000005B6
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<FloodableNaturalResourceSpec, FloodableNaturalResourceDescriber>();
			builder.AddDecorator<LivingWaterNaturalResource, LivingWaterNaturalResourceStatus>();
			builder.AddDecorator<WateredNaturalResource, WateredNaturalResourceStatus>();
			builder.AddDecorator<WateredNaturalResourceSpec, WateredNaturalResourceDescriber>();
			return builder.Build();
		}
	}
}
