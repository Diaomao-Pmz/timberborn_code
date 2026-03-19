using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.WaterContaminationBuildings;

namespace Timberborn.WaterContaminationBuildingsUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class WaterContaminationBuildingsUIConfigurator : Configurator
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021F2 File Offset: 0x000003F2
		public override void Configure()
		{
			base.Bind<BlockedByContaminationBuildingStatus>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WaterContaminationBuildingsUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221D File Offset: 0x0000041D
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ContaminationBlockableBuilding, BlockedByContaminationBuildingStatus>();
			return builder.Build();
		}
	}
}
