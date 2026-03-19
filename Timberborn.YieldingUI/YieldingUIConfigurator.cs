using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.Yielding;

namespace Timberborn.YieldingUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class YieldingUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<YieldRemovingBuildingDescriber>().AsTransient();
			base.Bind<YieldTooltipFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(YieldingUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F5 File Offset: 0x000002F5
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<YieldRemovingBuilding, YieldRemovingBuildingDescriber>();
			return builder.Build();
		}
	}
}
