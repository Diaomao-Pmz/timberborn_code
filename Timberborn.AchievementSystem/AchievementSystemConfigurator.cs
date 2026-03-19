using System;
using Bindito.Core;

namespace Timberborn.AchievementSystem
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class AchievementSystemConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000235F File Offset: 0x0000055F
		public override void Configure()
		{
			base.Bind<AchievementService>().AsSingleton();
		}
	}
}
