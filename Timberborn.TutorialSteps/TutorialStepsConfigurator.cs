using System;
using Bindito.Core;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000056 RID: 86
	[Context("Game")]
	public class TutorialStepsConfigurator : Configurator
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public override void Configure()
		{
			base.Bind<BuiltBuildingService>().AsSingleton();
			base.Bind<PlantableResourceCounter>().AsSingleton();
			base.Bind<CameraMovementService>().AsSingleton();
			base.Bind<FirstbornService>().AsSingleton();
			base.Bind<MissingDamTrigger>().AsSingleton();
			base.Bind<StairsUnlockedTrigger>().AsSingleton();
			base.Bind<SurvivedFirstBadtideTrigger>().AsSingleton();
			base.Bind<SurvivedFirstDroughtTrigger>().AsSingleton();
			base.Bind<PlatformBuiltTrigger>().AsSingleton();
			base.Bind<VisibleLevelChangeService>().AsSingleton();
			base.Bind<UnemployedBeaversTrigger>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<BuildingTutorialStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<ConnectBuildingsTutorialStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<MarkTreesTutorialStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<MarkPlantablesTutorialStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<PowerBuildingsTutorialStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<CameraMovementStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<CameraRotationStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<CameraZoomStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<SetPauseStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<GameSpeedStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<AccumulateScienceForBuildingStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<UnlockBuildingTutorialStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<BeaverBirthStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<SelectEntityStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<OpenWellbeingPanelStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<SelectStockpileGoodTutorialStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<SetWorkingHoursStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<VisibleLevelChangeStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<ChangePausedStateStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<DecreasePriorityStepDeserializer>().AsSingleton();
			base.MultiBind<IStepDeserializer>().To<IncreaseDesiredWorkersStepDeserializer>().AsSingleton();
		}
	}
}
