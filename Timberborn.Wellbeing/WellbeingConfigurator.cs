using System;
using Bindito.Core;
using Timberborn.Beavers;
using Timberborn.BonusSystem;
using Timberborn.Characters;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Wellbeing
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class WellbeingConfigurator : Configurator
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002158 File Offset: 0x00000358
		public override void Configure()
		{
			base.Bind<WellbeingTierManager>().AsTransient();
			base.Bind<WellbeingTracker>().AsTransient();
			base.Bind<WellbeingTrackerRegistrar>().AsTransient();
			base.Bind<DistrictWellbeingTrackerRegistry>().AsTransient();
			base.Bind<WellbeingService>().AsSingleton();
			base.Bind<WellbeingHighscore>().AsSingleton();
			base.Bind<IWellbeingTierService>().To<WellbeingTierService>().AsSingleton();
			base.Bind<WellbeingLimitService>().AsSingleton();
			base.Bind<GlobalWellbeingTrackerRegistry>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WellbeingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F3 File Offset: 0x000003F3
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BonusManager, WellbeingTierManager>();
			builder.AddDecorator<Character, WellbeingTracker>();
			builder.AddDecorator<DistrictCenter, DistrictWellbeingTrackerRegistry>();
			builder.AddDecorator<Beaver, WellbeingTrackerRegistrar>();
			return builder.Build();
		}
	}
}
