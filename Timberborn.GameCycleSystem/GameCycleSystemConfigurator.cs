using System;
using Bindito.Core;

namespace Timberborn.GameCycleSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	[Context("MapEditor")]
	public class GameCycleSystemConfigurator : Configurator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000236A File Offset: 0x0000056A
		public override void Configure()
		{
			base.Bind<GameCycleService>().AsSingleton();
		}
	}
}
