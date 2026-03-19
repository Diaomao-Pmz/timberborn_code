using System;
using Bindito.Core;
using Timberborn.Metrics;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BehaviorSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class BehaviorSystemConfigurator : Configurator
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002610 File Offset: 0x00000810
		public override void Configure()
		{
			base.Bind<TimerMetricCache<RootBehavior>>().AsTransient();
			base.Bind<WaitExecutor>().AsTransient();
			base.Bind<BehaviorManager>().AsTransient();
			base.Bind<BehaviorAgent>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BehaviorSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000266A File Offset: 0x0000086A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BehaviorManager, BehaviorAgent>();
			return builder.Build();
		}
	}
}
