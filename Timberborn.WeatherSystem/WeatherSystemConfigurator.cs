using System;
using Bindito.Core;
using Timberborn.GameCycleSystem;

namespace Timberborn.WeatherSystem
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class WeatherSystemConfigurator : Configurator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000023E8 File Offset: 0x000005E8
		public override void Configure()
		{
			base.Bind<WeatherService>().AsSingleton();
			base.Bind<TemperateWeatherDurationService>().AsSingleton();
			base.Bind<WeatherFastForwarder>().AsSingleton();
			base.MultiBind<ICycleDuration>().ToExisting<TemperateWeatherDurationService>();
		}
	}
}
