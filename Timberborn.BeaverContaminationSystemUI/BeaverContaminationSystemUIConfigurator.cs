using System;
using Bindito.Core;
using Timberborn.BeaverContaminationSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BeaverContaminationSystemUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class BeaverContaminationSystemUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<ContaminationIncubatorStatus>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BeaverContaminationSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ContaminationIncubator, ContaminationIncubatorStatus>();
			return builder.Build();
		}
	}
}
