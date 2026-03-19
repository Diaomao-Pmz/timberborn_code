using System;
using Bindito.Core;
using Timberborn.NaturalResources;
using Timberborn.TemplateInstantiation;

namespace Timberborn.UncuttableYielding
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class UncuttableYieldingConfigurator : Configurator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002148 File Offset: 0x00000348
		public override void Configure()
		{
			base.Bind<UncuttableReacher>().AsTransient();
			base.Bind<UncuttableRemoveYieldStrategy>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(UncuttableYieldingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000217F File Offset: 0x0000037F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<NaturalResourceSpec, UncuttableReacher>();
			builder.AddDecorator<NaturalResourceSpec, UncuttableRemoveYieldStrategy>();
			return builder.Build();
		}
	}
}
