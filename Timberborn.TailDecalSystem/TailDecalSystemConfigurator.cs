using System;
using Bindito.Core;
using Timberborn.Beavers;
using Timberborn.TemplateInstantiation;

namespace Timberborn.TailDecalSystem
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class TailDecalSystemConfigurator : Configurator
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002500 File Offset: 0x00000700
		public override void Configure()
		{
			base.Bind<EnterableTailDecalApplier>().AsTransient();
			base.Bind<TailDecalApplier>().AsTransient();
			base.Bind<TailDecalTextureSetter>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(TailDecalSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000254E File Offset: 0x0000074E
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BeaverSpec, TailDecalApplier>();
			builder.AddDecorator<TailDecalApplier, TailDecalTextureSetter>();
			builder.AddDecorator<EnterableTailDecalApplierSpec, EnterableTailDecalApplier>();
			return builder.Build();
		}
	}
}
