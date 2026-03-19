using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.LinkedBuildingSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class LinkedBuildingSystemConfigurator : Configurator
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000023A0 File Offset: 0x000005A0
		public override void Configure()
		{
			base.Bind<LinkedBuilding>().AsTransient();
			base.Bind<LinkedConstructionSite>().AsTransient();
			base.Bind<LinkedConstructionSiteReachability>().AsTransient();
			base.Bind<LinkedConstructionSiteRecoverableGoodMultiplier>().AsTransient();
			base.Bind<LinkedPausableConstructionSite>().AsTransient();
			base.Bind<LinkedInventories>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(LinkedBuildingSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002412 File Offset: 0x00000612
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<LinkedBuildingSpec, LinkedBuilding>();
			builder.AddDecorator<LinkedBuilding, LinkedConstructionSite>();
			builder.AddDecorator<LinkedConstructionSite, LinkedConstructionSiteReachability>();
			builder.AddDecorator<LinkedConstructionSite, LinkedPausableConstructionSite>();
			builder.AddDecorator<LinkedConstructionSite, LinkedInventories>();
			builder.AddDecorator<LinkedConstructionSite, LinkedConstructionSiteRecoverableGoodMultiplier>();
			return builder.Build();
		}
	}
}
