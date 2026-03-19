using System;
using Bindito.Core;

namespace Timberborn.TimeSpeedButtonSystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class TimeSpeedButtonSystemConfigurator : Configurator
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002586 File Offset: 0x00000786
		public override void Configure()
		{
			base.Bind<TimeSpeedButtonGroup>().AsTransient();
			base.Bind<TimeSpeedButtonFactory>().AsSingleton();
		}
	}
}
