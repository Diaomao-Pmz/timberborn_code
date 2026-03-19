using System;
using Bindito.Core;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Wandering
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	public class WanderingConfigurator : Configurator
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000279C File Offset: 0x0000099C
		public override void Configure()
		{
			base.Bind<VariedIdleAnimation>().AsTransient();
			base.Bind<StrandedRootBehavior>().AsTransient();
			base.Bind<WanderRootBehavior>().AsTransient();
			base.Bind<RestPlace>().AsTransient();
			base.Bind<StrandedStatus>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WanderingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002802 File Offset: 0x00000A02
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Citizen, StrandedStatus>();
			builder.AddDecorator<VariedIdleAnimationSpec, VariedIdleAnimation>();
			builder.AddDecorator<RestPlaceSpec, RestPlace>();
			return builder.Build();
		}
	}
}
