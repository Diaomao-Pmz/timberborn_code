using System;
using Bindito.Core;
using Timberborn.BuildingRange;
using Timberborn.TemplateInstantiation;

namespace Timberborn.RangedEffectBuildingUI
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class RangedEffectBuildingUIConfigurator : Configurator
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000261C File Offset: 0x0000081C
		public override void Configure()
		{
			base.Bind<BuildingWithRangePreviewUpdater>().AsTransient();
			base.Bind<BuildingWithRangeUpdateService>().AsSingleton();
			base.Bind<RangeTileMarkerService>().AsSingleton();
			base.Bind<RangeObjectHighlighterService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RangedEffectBuildingUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002676 File Offset: 0x00000876
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<IBuildingWithRange, BuildingWithRangePreviewUpdater>();
			return builder.Build();
		}
	}
}
