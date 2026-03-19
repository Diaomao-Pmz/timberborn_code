using System;
using Bindito.Core;

namespace Timberborn.HazardousWeatherSystemUI
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	public class HazardousWeatherSystemUIConfigurator : Configurator
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000026E4 File Offset: 0x000008E4
		public override void Configure()
		{
			base.Bind<HazardousWeatherUIHelper>().AsSingleton();
			base.Bind<DroughtWeatherUISpecification>().AsSingleton();
			base.Bind<BadtideWeatherUISpecification>().AsSingleton();
			base.Bind<HazardousWeatherNotificationPanel>().AsSingleton();
			base.Bind<HazardousWeatherApproachingTimer>().AsSingleton();
			base.Bind<HazardousWeatherSoundPlayer>().AsSingleton();
		}
	}
}
