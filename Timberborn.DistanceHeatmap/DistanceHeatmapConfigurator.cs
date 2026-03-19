using System;
using Bindito.Core;
using Timberborn.BlockSystemNavigation;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.DistanceHeatmap
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class DistanceHeatmapConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<DistanceHeatmapShower>().AsTransient();
			base.Bind<DistanceHeatmapEnabler>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(DistanceHeatmapConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F5 File Offset: 0x000002F5
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DistrictCenter, DistanceHeatmapShower>();
			builder.AddDecorator<BlockObjectWithPathRangeSpec, DistanceHeatmapEnabler>();
			return builder.Build();
		}
	}
}
