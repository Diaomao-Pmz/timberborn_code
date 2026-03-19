using System;
using Bindito.Core;
using Timberborn.AchievementSystem;

namespace Timberborn.SteamAchievementSystem
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class SteamAchievementSystemConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021AA File Offset: 0x000003AA
		public override void Configure()
		{
			base.Bind<IStoreAchievements>().To<SteamAchievements>().AsSingleton();
		}
	}
}
