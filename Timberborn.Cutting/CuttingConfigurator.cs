using System;
using Bindito.Core;
using Timberborn.GoodStackSystem;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Cutting
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	[Context("MapEditor")]
	public class CuttingConfigurator : Configurator
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000026E3 File Offset: 0x000008E3
		public override void Configure()
		{
			base.Bind<Cuttable>().AsTransient();
			base.Bind<EmptyDeadNaturalResourceOverrider>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CuttingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000271A File Offset: 0x0000091A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<CuttableSpec, Cuttable>();
			builder.AddDecorator<Cuttable, GoodStack>();
			builder.AddDecorator<LivingNaturalResource, EmptyDeadNaturalResourceOverrider>();
			return builder.Build();
		}
	}
}
