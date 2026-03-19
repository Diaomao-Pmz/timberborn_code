using System;
using Bindito.Core;

namespace Timberborn.LevelVisibilitySystem
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class LevelVisibilitySystemConfigurator : Configurator
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002434 File Offset: 0x00000634
		public override void Configure()
		{
			base.Bind<ILevelVisibilityService>().To<LevelVisibilityService>().AsSingleton();
		}
	}
}
