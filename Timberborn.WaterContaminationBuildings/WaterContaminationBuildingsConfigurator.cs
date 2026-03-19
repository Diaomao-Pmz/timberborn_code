using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.WaterBuildings;

namespace Timberborn.WaterContaminationBuildings
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class WaterContaminationBuildingsConfigurator : Configurator
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000022D7 File Offset: 0x000004D7
		public override void Configure()
		{
			base.Bind<ContaminationBlockableBuilding>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WaterContaminationBuildingsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002302 File Offset: 0x00000502
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<IWaterNeedingBuilding, ContaminationBlockableBuilding>();
			return builder.Build();
		}
	}
}
