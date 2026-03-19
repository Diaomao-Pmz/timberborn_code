using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.LevelVisibilitySystemUI
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class LevelVisibilitySystemUIConfigurator : Configurator
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000289F File Offset: 0x00000A9F
		public override void Configure()
		{
			base.Bind<ILevelVisibilityPanel>().To<LevelVisibilityPanel>().AsSingleton();
			base.Bind<LevelVisibilitySelector>().AsSingleton();
			base.Bind<LevelVisibilityPicker>().AsSingleton();
			base.MultiBind<IDevModule>().To<LevelVisibilityDevModule>().AsSingleton();
		}
	}
}
