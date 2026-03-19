using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.GameSound
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class GameSoundConfigurator : Configurator
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000026AC File Offset: 0x000008AC
		public override void Configure()
		{
			base.Bind<WaterAmbientSound>().AsSingleton();
			base.Bind<SoundListenerDebugger>().AsSingleton();
			base.Bind<GameUISoundController>().AsSingleton();
			base.Bind<SoundSystemDebuggingPanel>().AsSingleton();
			base.Bind<GameMusicPlayer>().AsSingleton();
			base.Bind<DayNightAmbientSound>().AsSingleton();
			base.MultiBind<IDevModule>().To<SoundListenerDebuggerDevModule>().AsSingleton();
		}
	}
}
