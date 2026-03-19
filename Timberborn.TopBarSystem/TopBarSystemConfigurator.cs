using System;
using Bindito.Core;

namespace Timberborn.TopBarSystem
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	public class TopBarSystemConfigurator : Configurator
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002996 File Offset: 0x00000B96
		public override void Configure()
		{
			base.Bind<TopBarCounterRowFactory>().AsSingleton();
			base.Bind<TopBarCounterFactory>().AsSingleton();
			base.Bind<TopBarPanel>().AsSingleton();
		}
	}
}
