using System;
using Bindito.Core;
using Timberborn.Buildings;
using Timberborn.StatusSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BuildingStatuses
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	[Context("MapEditor")]
	public class BuildingStatusesConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<BuildingStatusIconOffsetter>().AsTransient();
			base.Bind<BuildingStatusIconUpdater>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BuildingStatusesConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F5 File Offset: 0x000002F5
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuildingSpec, BuildingStatusIconOffsetter>();
			builder.AddDecorator<BuildingSpec, StatusSubject>();
			builder.AddDecorator<BuildingSpec, StatusIconCycler>();
			return builder.Build();
		}
	}
}
