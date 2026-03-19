using System;
using Bindito.Core;
using Timberborn.Beavers;
using Timberborn.Bots;
using Timberborn.TemplateInstantiation;
using Timberborn.Wonders;

namespace Timberborn.WonderPlanes
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	public class WonderPlanesConfigurator : Configurator
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000038A8 File Offset: 0x00001AA8
		public override void Configure()
		{
			base.Bind<Pilot>().AsTransient();
			base.Bind<BotPilotHelmet>().AsTransient();
			base.Bind<Plane>().AsTransient();
			base.Bind<PlaneCatapult>().AsTransient();
			base.Bind<PlaneLauncher>().AsTransient();
			base.Bind<PlaneLauncherRotator>().AsTransient();
			base.Bind<PlaneSpawner>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WonderPlanesConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003928 File Offset: 0x00001B28
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<AdultSpec, Pilot>();
			builder.AddDecorator<BotSpec, Pilot>();
			builder.AddDecorator<BotSpec, BotPilotHelmet>();
			builder.AddDecorator<PlaneLauncherSpec, PlaneLauncher>();
			builder.AddDecorator<PlaneLauncher, NotEnoughWorkersWonderBlocker>();
			builder.AddDecorator<PlaneSpec, Plane>();
			builder.AddDecorator<PlaneSpawnerSpec, PlaneSpawner>();
			builder.AddDecorator<PlaneLauncherRotatorSpec, PlaneLauncherRotator>();
			builder.AddDecorator<PlaneCatapultSpec, PlaneCatapult>();
			return builder.Build();
		}
	}
}
