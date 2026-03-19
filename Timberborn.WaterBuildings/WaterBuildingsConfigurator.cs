using System;
using Bindito.Core;
using Timberborn.Automation;
using Timberborn.Buildings;
using Timberborn.Particles;
using Timberborn.PathSystem;
using Timberborn.Rendering;
using Timberborn.TemplateInstantiation;
using Timberborn.WaterObjects;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200002E RID: 46
	[Context("Game")]
	public class WaterBuildingsConfigurator : Configurator
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00006994 File Offset: 0x00004B94
		public override void Configure()
		{
			base.Bind<Sluice>().AsTransient();
			base.Bind<StreamGauge>().AsTransient();
			base.Bind<TickableWaterBuilding>().AsTransient();
			base.Bind<WaterMover>().AsTransient();
			base.Bind<WaterMoverParticleController>().AsTransient();
			base.Bind<WaterMoverPowerConsumptionSwitch>().AsTransient();
			base.Bind<WaterNeeder>().AsTransient();
			base.Bind<AccessibleFloodableBuilding>().AsTransient();
			base.Bind<FloodableBuilding>().AsTransient();
			base.Bind<FloodableFire>().AsTransient();
			base.Bind<FloodedBuildingPreviewValidator>().AsTransient();
			base.Bind<Floodgate>().AsTransient();
			base.Bind<FloodgateAnimationController>().AsTransient();
			base.Bind<FloodgateGateCutoff>().AsTransient();
			base.Bind<PreviewWaterInputPipeBlocker>().AsTransient();
			base.Bind<WaterObstacleController>().AsTransient();
			base.Bind<SluiceState>().AsTransient();
			base.Bind<StreamGaugeAnimationController>().AsTransient();
			base.Bind<Valve>().AsTransient();
			base.Bind<FillValve>().AsTransient();
			base.Bind<WaterInput>().AsTransient();
			base.Bind<WaterInputCoordinates>().AsTransient();
			base.Bind<WaterInputPipe>().AsTransient();
			base.Bind<WaterInputPipeSegmentCreator>().AsTransient();
			base.Bind<WaterOutput>().AsTransient();
			base.Bind<WaterInputPipeValidator>().AsTransient();
			base.Bind<WaterInputObstructedStatus>().AsTransient();
			base.Bind<FloodgateSynchronizer>().AsSingleton();
			base.Bind<PreviewWaterInputPipeBlockerService>().AsSingleton();
			base.Bind<SluiceSynchronizer>().AsSingleton();
			base.Bind<ValveSynchronizer>().AsSingleton();
			base.Bind<FillValveSynchronizer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WaterBuildingsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006B40 File Offset: 0x00004D40
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WaterInputSpec, WaterInput>();
			builder.AddDecorator<WaterInput, WaterInputCoordinates>();
			builder.AddDecorator<WaterInput, WaterInputPipe>();
			builder.AddDecorator<WaterInput, WaterInputPipeSegmentCreator>();
			builder.AddDecorator<WaterInputPipe, EntityMaterials>();
			builder.AddDecorator<BuildingSpec, PreviewWaterInputPipeBlocker>();
			builder.AddDecorator<BuildingAccessible, AccessibleFloodableBuilding>();
			builder.AddDecorator<BuildingAccessible, BlockableFloodableObject>();
			builder.AddDecorator<WaterMoverSpec, WaterMover>();
			builder.AddDecorator<WaterMover, WaterMoverPowerConsumptionSwitch>();
			builder.AddDecorator<FloodgateSpec, Floodgate>();
			builder.AddDecorator<Floodgate, FloodgateGateCutoff>();
			builder.AddDecorator<Floodgate, AutoAutomatableNeeder>();
			builder.AddDecorator<SluiceSpec, Sluice>();
			builder.AddDecorator<Sluice, SluiceState>();
			builder.AddDecorator<Sluice, WaterObstacleController>();
			builder.AddDecorator<ValveSpec, Valve>();
			builder.AddDecorator<Valve, WaterObstacleController>();
			builder.AddDecorator<Valve, AutoAutomatableNeeder>();
			builder.AddDecorator<FillValveSpec, FillValve>();
			builder.AddDecorator<FillValve, WaterObstacleController>();
			builder.AddDecorator<FillValve, AutoAutomatableNeeder>();
			builder.AddDecorator<ImpermeableFloorSpec, DynamicPathModelUpdater>();
			builder.AddDecorator<FloodableBuildingSpec, FloodableBuilding>();
			builder.AddDecorator<AccessibleFloodableBuilding, FloodableBuilding>();
			builder.AddDecorator<FloodgateAnimationControllerSpec, FloodgateAnimationController>();
			builder.AddDecorator<StreamGaugeSpec, StreamGauge>();
			builder.AddDecorator<StreamGaugeAnimationControllerSpec, StreamGaugeAnimationController>();
			builder.AddDecorator<WaterMoverParticleControllerSpec, WaterMoverParticleController>();
			builder.AddDecorator<WaterMoverParticleController, ParticlesCache>();
			builder.AddDecorator<WaterOutputSpec, WaterOutput>();
			builder.AddDecorator<TickableWaterBuildingSpec, TickableWaterBuilding>();
			builder.AddDecorator<FloodableFireSpec, FloodableFire>();
			builder.AddDecorator<WaterNeederSpec, WaterNeeder>();
			builder.AddDecorator<FloodableBuilding, FloodedBuildingPreviewValidator>();
			builder.AddDecorator<FloodableBuilding, FloodableObject>();
			builder.AddDecorator<WaterInputPipe, WaterInputPipeValidator>();
			builder.AddDecorator<WaterInputPipe, WaterInputObstructedStatus>();
			return builder.Build();
		}
	}
}
