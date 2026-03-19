using System;
using Bindito.Core;
using Timberborn.GameCycleSystem;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	[Context("MapEditor")]
	public class HazardousWeatherSystemConfigurator : Configurator
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002C3C File Offset: 0x00000E3C
		public override void Configure()
		{
			base.Bind<HazardousWeatherService>().AsSingleton();
			base.Bind<DroughtWeather>().AsSingleton();
			base.Bind<BadtideWeather>().AsSingleton();
			base.Bind<HazardousWeatherRandomizer>().AsSingleton();
			base.Bind<HazardousWeatherHistory>().AsSingleton();
			base.Bind<HazardousWeatherHistoryDataSerializer>().AsSingleton();
			base.MultiBind<ICycleDuration>().ToExisting<HazardousWeatherService>();
		}
	}
}
