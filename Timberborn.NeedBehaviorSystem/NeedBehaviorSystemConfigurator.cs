using System;
using Bindito.Core;
using Timberborn.GameDistricts;
using Timberborn.NeedSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200001C RID: 28
	[Context("Game")]
	public class NeedBehaviorSystemConfigurator : Configurator
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003198 File Offset: 0x00001398
		public override void Configure()
		{
			base.Bind<NeederRootBehavior>().AsTransient();
			base.Bind<ApplyEffectExecutor>().AsTransient();
			base.Bind<CriticalNeedActionStatusRegistrar>().AsTransient();
			base.Bind<CriticalNeedStateStatusRegistrar>().AsTransient();
			base.Bind<ActionDurationCalculator>().AsTransient();
			base.Bind<Appraiser>().AsTransient();
			base.Bind<CriticalNeedStateAnimation>().AsTransient();
			base.Bind<DistrictNeedBehaviorService>().AsTransient();
			base.Bind<NeedPenaltyManager>().AsTransient();
			base.Bind<CriticalNeederRootBehavior>().AsTransient();
			base.Bind<NeedBehaviorKeyGenerator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NeedBehaviorSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003246 File Offset: 0x00001446
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DistrictCenter, DistrictNeedBehaviorService>();
			builder.AddDecorator<NeedManager, Appraiser>();
			builder.AddDecorator<NeedManager, ActionDurationCalculator>();
			builder.AddDecorator<NeedManager, CriticalNeedActionStatusRegistrar>();
			builder.AddDecorator<NeedManager, CriticalNeedStateStatusRegistrar>();
			builder.AddDecorator<NeedManager, NeedPenaltyManager>();
			builder.AddDecorator<CriticalNeedStateAnimationSpec, CriticalNeedStateAnimation>();
			return builder.Build();
		}
	}
}
