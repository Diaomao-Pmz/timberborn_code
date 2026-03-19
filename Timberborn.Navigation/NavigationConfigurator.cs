using System;
using Bindito.Core;

namespace Timberborn.Navigation
{
	// Token: 0x0200004B RID: 75
	[Context("Game")]
	public class NavigationConfigurator : Configurator
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00004FE0 File Offset: 0x000031E0
		public override void Configure()
		{
			base.Bind<Accessible>().AsTransient();
			base.Bind<INavigationService>().To<NavigationService>().AsSingleton();
			base.Bind<INavMeshService>().To<NavMeshService>().AsSingleton();
			base.Bind<INavigationCachingService>().To<NavigationCachingService>().AsSingleton();
			base.Bind<INavigationDebuggingService>().To<NavigationDebuggingService>().AsSingleton();
			base.Bind<INavMeshDrawer>().To<NavMeshDrawer>().AsSingleton();
			base.Bind<PathfindingService>().AsSingleton();
			base.Bind<NodeIdService>().AsSingleton();
			base.Bind<HeuristicsCalculator>().AsSingleton();
			base.Bind<NavMeshUpdater>().AsSingleton();
			base.Bind<NavMeshPositionService>().AsSingleton();
			base.Bind<DistanceCalculator>().AsSingleton();
			base.Bind<GlobalReachabilityService>().AsSingleton();
			base.Bind<INavMeshObjectFactory>().To<NavMeshObjectFactory>().AsSingleton();
			base.Bind<NavMeshChangeFactory>().AsSingleton();
			base.Bind<NavMeshUpdateNotifier>().AsSingleton();
			base.Bind<NavMeshUpdateBuilderFactory>().AsSingleton();
			base.Bind<NavMeshListenerSingletonRegistry>().AsSingleton();
			base.Bind<INavMeshListenerEntityRegistry>().To<NavMeshListenerEntityRegistry>().AsSingleton();
			base.Bind<NavigationDistance>().AsSingleton();
			base.Bind<INavigationRangeService>().To<NavigationRangeService>().AsSingleton();
			base.Bind<BinaryHeapFactory>().AsSingleton();
			base.Bind<FlowFieldPathFinder>().AsSingleton();
			base.Bind<FlowFieldPathBuilder>().AsSingleton();
			base.Bind<FlowFieldPathTransformer>().AsSingleton();
			base.Bind<NavigationSynchronizer>().AsSingleton();
			base.Bind<INavigationPhase>().ToExisting<NavigationSynchronizer>();
			base.Bind<RestrictedNodeMap>().AsSingleton();
			base.Bind<RestrictedNodeUpdater>().AsSingleton();
			base.Bind<NavMeshGroupService>().AsSingleton();
			base.Bind<TerrainNavMeshSource>().AsSingleton();
			base.Bind<TerrainNavMeshGraph>().AsSingleton();
			base.Bind<PreviewTerrainNavMeshSource>().AsSingleton();
			base.Bind<PreviewTerrainNavMeshGraph>().AsSingleton();
			base.Bind<InstantTerrainNavMeshSource>().AsSingleton();
			base.Bind<InstantTerrainNavMeshGraph>().AsSingleton();
			base.Bind<TerrainFlowFieldCache>().AsSingleton();
			base.Bind<TerrainFlowFieldGenerator>().AsSingleton();
			base.Bind<TerrainAStarPathfinder>().AsSingleton();
			base.Bind<TerrainNavigationRangeService>().AsSingleton();
			base.Bind<TerrainReachabilityService>().AsSingleton();
			base.Bind<RoadNavMeshSource>().AsSingleton();
			base.Bind<RoadNavMeshGraph>().AsSingleton();
			base.Bind<PreviewRoadNavMeshSource>().AsSingleton();
			base.Bind<PreviewRoadNavMeshGraph>().AsSingleton();
			base.Bind<InstantRoadNavMeshSource>().AsSingleton();
			base.Bind<InstantRoadNavMeshGraph>().AsSingleton();
			base.Bind<RoadFlowFieldCache>().AsSingleton();
			base.Bind<RoadFlowFieldGenerator>().AsSingleton();
			base.Bind<RoadAStarPathfinder>().AsSingleton();
			base.Bind<RoadNavigationRangeService>().AsSingleton();
			base.Bind<DistrictRoadFlowFieldGenerator>().AsSingleton();
			base.Bind<RoadReachabilityService>().AsSingleton();
			base.Bind<RoadSpillFlowFieldGenerator>().AsSingleton();
			base.Bind<RoadSpillNavigationRangeService>().AsSingleton();
			base.Bind<IDistrictService>().To<DistrictService>().AsSingleton();
			base.Bind<DistrictUpdater>().AsSingleton();
			base.Bind<DistrictConflictDetector>().AsSingleton();
			base.Bind<DistrictNavMeshListener>().AsSingleton();
			base.Bind<DistrictMap>().AsSingleton();
			base.Bind<DistrictObstacleService>().AsSingleton();
			base.Bind<InstantDistrictMap>().AsSingleton();
			base.Bind<InstantDistrictObstacleService>().AsSingleton();
			base.Bind<PreviewDistrictMap>().AsSingleton();
			base.Bind<PreviewDistrictObstacleService>().AsSingleton();
			base.Bind<DistrictRandomDestinationPicker>().AsSingleton();
			base.Bind<GateConflictDetector>().AsSingleton();
		}
	}
}
