using System;
using Bindito.Core;
using Timberborn.BlockSystemNavigation;
using Timberborn.BuildingRange;
using Timberborn.Buildings;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	public class BuildingsNavigationConfigurator : Configurator
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002ECC File Offset: 0x000010CC
		public override void Configure()
		{
			base.Bind<BuildingCachingFlowField>().AsTransient();
			base.Bind<BuildingNavMesh>().AsTransient();
			base.Bind<BuildingRangeDrawer>().AsTransient();
			base.Bind<BuildingTerrainRange>().AsTransient();
			base.Bind<ConstructionSiteAccessible>().AsTransient();
			base.Bind<DistrictPathNavRangeDrawer>().AsTransient();
			base.Bind<DistrictPathNavRangeDrawerRegistrar>().AsTransient();
			base.Bind<PathDistrictRetriever>().AsTransient();
			base.Bind<PathMeshHider>().AsTransient();
			base.Bind<PathRangeDrawer>().AsTransient();
			base.Bind<BoundsNavRangeDrawingService>().AsSingleton();
			base.Bind<PathNavRangeDrawerInvalidator>().AsSingleton();
			base.Bind<PathMeshDrawerFactory>().AsSingleton();
			base.Bind<BoundsNavRangeCalculator>().AsSingleton();
			base.Bind<BoundsNavRangeDrawer>().AsSingleton();
			base.Bind<DistanceToColorConverter>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BuildingsNavigationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FB8 File Offset: 0x000011B8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuildingSpec, BuildingNavMesh>();
			builder.AddDecorator<BuildingAccessible, BuildingCachingFlowField>();
			builder.AddDecorator<BuildingSpec, ConstructionSiteAccessible>();
			builder.AddDecorator<DistrictBuilding, BuildingRangeDrawer>();
			builder.AddDecorator<BuildingWithTerrainRange, BuildingTerrainRange>();
			builder.AddDecorator<BlockObjectWithPathRangeSpec, PathRangeDrawer>();
			builder.AddDecorator<BlockObjectWithPathRangeSpec, PathDistrictRetriever>();
			builder.AddDecorator<DistrictCenter, DistrictPathNavRangeDrawer>();
			builder.AddDecorator<DistrictCenter, PathMeshHider>();
			builder.AddDecorator<DistrictPathNavRangeDrawer, DistrictPathNavRangeDrawerRegistrar>();
			return builder.Build();
		}
	}
}
