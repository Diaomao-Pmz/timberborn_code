using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Growing
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	[Context("MapEditor")]
	public class GrowingConfigurator : Configurator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002572 File Offset: 0x00000772
		public override void Configure()
		{
			base.Bind<Growable>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(GrowingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000259D File Offset: 0x0000079D
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GrowableSpec, Growable>();
			return builder.Build();
		}
	}
}
