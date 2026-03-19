using System;
using Bindito.Core;

namespace Timberborn.GameOver
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class GameOverConfigurator : Configurator
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000224E File Offset: 0x0000044E
		public override void Configure()
		{
			base.Bind<IGameOverChecker>().To<GameOverChecker>().AsSingleton();
			base.Bind<GameOverDisabler>().AsSingleton();
		}
	}
}
