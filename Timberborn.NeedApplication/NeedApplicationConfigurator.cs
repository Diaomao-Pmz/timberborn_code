using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000012 RID: 18
	[Context("Game")]
	public class NeedApplicationConfigurator : Configurator
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002CAC File Offset: 0x00000EAC
		public override void Configure()
		{
			base.Bind<AreaNeedApplier>().AsTransient();
			base.Bind<WorkshopRandomNeedApplier>().AsTransient();
			base.Bind<YieldRemoverNeedApplier>().AsTransient();
			base.Bind<DemolisherNeedApplier>().AsTransient();
			base.Bind<EffectProbabilityService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NeedApplicationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D12 File Offset: 0x00000F12
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<AreaNeedApplierSpec, AreaNeedApplier>();
			builder.AddDecorator<WorkshopRandomNeedApplierSpec, WorkshopRandomNeedApplier>();
			builder.AddDecorator<YieldRemoverNeedApplierSpec, YieldRemoverNeedApplier>();
			builder.AddDecorator<DemolisherNeedApplierSpec, DemolisherNeedApplier>();
			return builder.Build();
		}
	}
}
