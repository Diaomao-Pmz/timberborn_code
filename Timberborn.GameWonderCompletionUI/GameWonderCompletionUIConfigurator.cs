using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.GameWonderCompletionUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class GameWonderCompletionUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<StatisticItemFactory>().AsSingleton();
			base.Bind<WonderCompletionPanel>().AsSingleton();
			base.MultiBind<IDevModule>().To<WonderCompletionDevModule>().AsSingleton();
		}
	}
}
