using System;
using Bindito.Core;
using Timberborn.Buildings;
using Timberborn.ConstructionSites;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class BuildingsReachabilityConfigurator : Configurator
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000223C File Offset: 0x0000043C
		public override void Configure()
		{
			base.Bind<EntityReachabilityStatus>().AsTransient();
			base.Bind<BlockableEntranceBuilding>().AsTransient();
			base.Bind<ConstructionSiteEntranceBlockedPreviewValidator>().AsTransient();
			base.Bind<ReachableConstructionSite>().AsTransient();
			base.Bind<UnconnectedBuildingStatus>().AsTransient();
			base.Bind<UnconnectedBuildingBlocker>().AsTransient();
			base.Bind<ReachabilityPreviewValidator>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BuildingsReachabilityConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022BA File Offset: 0x000004BA
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ConstructionSite, ReachableConstructionSite>();
			builder.AddDecorator<ConstructionSite, ReachabilityPreviewValidator>();
			builder.AddDecorator<ConstructionSite, ConstructionSiteEntranceBlockedPreviewValidator>();
			builder.AddDecorator<BuildingAccessible, UnconnectedBuildingStatus>();
			builder.AddDecorator<BuildingSpec, BlockableEntranceBuilding>();
			builder.AddDecorator<IUnreachableEntity, EntityReachabilityStatus>();
			builder.AddDecorator<UnconnectedBuildingBlockerSpec, UnconnectedBuildingBlocker>();
			return builder.Build();
		}
	}
}
