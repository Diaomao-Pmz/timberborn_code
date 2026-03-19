using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.TimeSystemUI
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class TimeSystemUIConfigurator : Configurator
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002928 File Offset: 0x00000B28
		public override void Configure()
		{
			base.Bind<ClockPanel>().AsSingleton();
			base.Bind<TimeScaleDebuggingPanel>().AsSingleton();
			base.Bind<ClockDebuggingPanel>().AsSingleton();
			base.MultiBind<IDevModule>().To<SpeedControlPanel>().AsSingleton();
			base.MultiBind<IDevModule>().To<TimeFastForwarderDevModule>().AsSingleton();
			base.MultiBind<IDevModule>().To<StopwatchDevModule>().AsSingleton();
		}
	}
}
