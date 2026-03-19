using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.WeatherSystemUI
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class WeatherSystemUIConfigurator : Configurator
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002A2D File Offset: 0x00000C2D
		public override void Configure()
		{
			base.Bind<DatePanel>().AsSingleton();
			base.Bind<WeatherPanel>().AsSingleton();
			base.Bind<WeatherDebuggingPanel>().AsSingleton();
			base.MultiBind<IDevModule>().To<WeatherFastForwarderDevModule>().AsSingleton();
		}
	}
}
