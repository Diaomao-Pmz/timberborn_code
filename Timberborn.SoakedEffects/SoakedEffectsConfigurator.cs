using System;
using Bindito.Core;
using Timberborn.NeedSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.SoakedEffects
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class SoakedEffectsConfigurator : Configurator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000022FB File Offset: 0x000004FB
		public override void Configure()
		{
			base.Bind<SoakedEffectApplier>().AsTransient();
			base.Bind<SoakedEffectService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(SoakedEffectsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002332 File Offset: 0x00000532
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<NeedManager, SoakedEffectApplier>();
			return builder.Build();
		}
	}
}
