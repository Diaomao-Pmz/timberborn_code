using System;
using Bindito.Core;

namespace Timberborn.GameOverUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class GameOverUIConfigurator : Configurator
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002215 File Offset: 0x00000415
		public override void Configure()
		{
			base.Bind<GameOverBox>().AsSingleton();
		}
	}
}
