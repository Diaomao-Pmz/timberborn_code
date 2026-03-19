using System;
using Bindito.Core;
using Timberborn.AchievementSystem;
using Timberborn.Beavers;
using Timberborn.Explosions;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Achievements
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class AchievementsConfigurator : Configurator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public override void Configure()
		{
			base.Bind<PlaceDynamiteAtBottomTracker>().AsTransient();
			base.Bind<InjuredJustBornBeaverTracker>().AsTransient();
			base.Bind<TreePlantingCounter>().AsSingleton();
			base.Bind<PlaceDynamiteAtBottomAchievement>().AsSingleton();
			base.Bind<InjuredJustBornBeaverAchievement>().AsSingleton();
			base.MultiBind<Achievement>().ToExisting<PlaceDynamiteAtBottomAchievement>();
			base.MultiBind<Achievement>().ToExisting<InjuredJustBornBeaverAchievement>();
			base.MultiBind<Achievement>().To<BadtideStreakAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BuildCampfireAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BuildDamAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BatteryChargeStorageAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ExplodeUnitWithDynamiteAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<Cycle5SurvivalAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<Cycle10SurvivalAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<Cycle20SurvivalAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<Cycle50SurvivalAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachBuildHeightLimitAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BuildWonderBeforeCycleAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ExplodeDynamiteInSingleDayAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BuildBotAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BuildBotAfterBeaverExtinctionAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<WorkAllDayForWeekAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<SurviveBadtideAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<SurviveDroughtAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BuildEveryStructureFolktailsAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BuildEveryStructureIronTeethAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<UnlockIronTeethAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BuildManyHedgesAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachAverageWellbeing4Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachAverageWellbeing10Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachAverageWellbeing20Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachAverageWellbeing30Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachAverageWellbeing40Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachAverageWellbeing50Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachAverageWellbeing60Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachMaxAverageWellbeingAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachBeaverPopulation100Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachBeaverPopulation250Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachBeaverPopulation500Achievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BuildStackedHydroponicGardensAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<Plant1000TreesAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<Plant5000TreesAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<Plant10000TreesAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<GeneratePowerWithWaterWheelsOnlyAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<GeneratePowerWithPowerWheelsOnlyAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<GeneratePowerWithWindTurbinesOnlyAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachPopulationWithoutDwellingsAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ActivateWonderFolktailsAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ActivateWonderIronTeethAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ActivateMultipleWondersAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ReachMaxAverageWellbeingPopulatedAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ZiplineNetworkLengthAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<CureContaminatedBeaverAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<LargeTubewayNetworkAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BornBeaverAfterBeaverExtinctionAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<PlugAnyBadwaterSourceAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<PlugAllBadwaterSourcesAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<FloodBuildingAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<ProducePlanksInDayAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BeaverStungByBeeAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<DemolishAndRebuildAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<WorkingRefineryForEachRecipeAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<BeaverDiesMiserableAchievement>().AsSingleton();
			base.MultiBind<Achievement>().To<MaplePastryOnlyAchievement>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(AchievementsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002517 File Offset: 0x00000717
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Dynamite, PlaceDynamiteAtBottomTracker>();
			builder.AddDecorator<AdultSpec, InjuredJustBornBeaverTracker>();
			return builder.Build();
		}
	}
}
