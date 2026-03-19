using System;
using Bindito.Core;

namespace Timberborn.FactionSystem
{
	// Token: 0x0200000B RID: 11
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class FactionSystemConfigurator : Configurator
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002E7C File Offset: 0x0000107C
		public override void Configure()
		{
			base.Bind<FactionSpecService>().AsSingleton();
			base.Bind<FactionUnlockingService>().AsSingleton();
			base.Bind<FactionUnlockConditionDescriber>().AsSingleton();
		}
	}
}
