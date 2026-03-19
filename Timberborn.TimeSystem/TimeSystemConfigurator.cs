using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000019 RID: 25
	[Context("Game")]
	[Context("MapEditor")]
	public class TimeSystemConfigurator : Configurator
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00002E7C File Offset: 0x0000107C
		public override void Configure()
		{
			base.Bind<ClockHandAnimator>().AsTransient();
			base.Bind<IDayNightCycle>().To<DayNightCycle>().AsSingleton();
			base.Bind<NonlinearAnimationManager>().AsSingleton();
			base.Bind<TimeTriggerService>().AsSingleton();
			base.Bind<SpeedManager>().AsSingleton();
			base.Bind<TimeFastForwarder>().AsSingleton();
			base.Bind<GameSpeedSoundController>().AsSingleton();
			base.Bind<ITimeTriggerFactory>().To<TimeTriggerFactory>().AsSingleton();
			base.Bind<ITickProgressService>().To<TickProgressService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(TimeSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002F21 File Offset: 0x00001121
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ClockHandAnimatorSpec, ClockHandAnimator>();
			return builder.Build();
		}
	}
}
