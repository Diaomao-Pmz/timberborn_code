using System;
using Bindito.Core;
using Timberborn.AlertPanelSystem;
using Timberborn.EntityPanelSystem;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000029 RID: 41
	[Context("Game")]
	public class WellbeingUIConfigurator : Configurator
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00004C6C File Offset: 0x00002E6C
		public override void Configure()
		{
			base.Bind<NeedViewFactory>().AsSingleton();
			base.Bind<WellbeingFragment>().AsSingleton();
			base.Bind<PopulationWellbeingBox>().AsSingleton();
			base.Bind<WellbeingServiceDistrictSwitcher>().AsSingleton();
			base.Bind<GoalRowFactory>().AsSingleton();
			base.Bind<BasicStatisticsPanel>().AsSingleton();
			base.Bind<WellbeingHighscoreAlertFragment>().AsSingleton();
			base.Bind<NeedGroupViewFactory>().AsSingleton();
			base.Bind<WellbeingBatchControlRowItemFactory>().AsSingleton();
			base.Bind<PopulationWellbeingCounterGroupFactory>().AsSingleton();
			base.Bind<NeedEffectDescriptionService>().AsSingleton();
			base.Bind<WellbeingSummaryFactory>().AsSingleton();
			base.Bind<WellbeingSummaryBonusFactory>().AsSingleton();
			base.Bind<WellbeingBonusTooltipFactory>().AsSingleton();
			base.Bind<WellbeingNameHelper>().AsSingleton();
			base.Bind<BasicStatisticsPanelFactory>().AsSingleton();
			base.Bind<PopulationWellbeingGoals>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<WellbeingUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<AlertPanelModule>().ToProvider<WellbeingUIConfigurator.AlertPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200002A RID: 42
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x060000DA RID: 218 RVA: 0x00004D6F File Offset: 0x00002F6F
			public EntityPanelModuleProvider(WellbeingFragment wellbeingFragment)
			{
				this._wellbeingFragment = wellbeingFragment;
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00004D7E File Offset: 0x00002F7E
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._wellbeingFragment, 0);
				return builder.Build();
			}

			// Token: 0x040000C7 RID: 199
			public readonly WellbeingFragment _wellbeingFragment;
		}

		// Token: 0x0200002B RID: 43
		public class AlertPanelModuleProvider : IProvider<AlertPanelModule>
		{
			// Token: 0x060000DC RID: 220 RVA: 0x00004D97 File Offset: 0x00002F97
			public AlertPanelModuleProvider(WellbeingHighscoreAlertFragment wellbeingHighscoreAlertFragment)
			{
				this._wellbeingHighscoreAlertFragment = wellbeingHighscoreAlertFragment;
			}

			// Token: 0x060000DD RID: 221 RVA: 0x00004DA6 File Offset: 0x00002FA6
			public AlertPanelModule Get()
			{
				AlertPanelModule.Builder builder = new AlertPanelModule.Builder();
				builder.AddAlertFragment(this._wellbeingHighscoreAlertFragment, 1);
				return builder.Build();
			}

			// Token: 0x040000C8 RID: 200
			public readonly WellbeingHighscoreAlertFragment _wellbeingHighscoreAlertFragment;
		}
	}
}
