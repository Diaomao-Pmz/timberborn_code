using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.DeteriorationSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class DeteriorationSystemConfigurator : Configurator
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000023A6 File Offset: 0x000005A6
		public override void Configure()
		{
			base.Bind<Deteriorable>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(DeteriorationSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023D1 File Offset: 0x000005D1
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DeteriorableSpec, Deteriorable>();
			return builder.Build();
		}
	}
}
