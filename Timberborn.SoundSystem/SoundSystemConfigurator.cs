using System;
using Bindito.Core;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000019 RID: 25
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class SoundSystemConfigurator : Configurator
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00003540 File Offset: 0x00001740
		public override void Configure()
		{
			base.Bind<AudioClipService>().AsSingleton();
			base.Bind<ISoundSystem>().To<SoundSystem>().AsSingleton();
			base.Bind<AudioMixerGroupRetriever>().AsSingleton();
			base.Bind<VolumeController>().AsSingleton();
			base.Bind<AudioSourceFactory>().AsSingleton();
			base.Bind<AudioSourceFader>().AsSingleton();
			base.Bind<SoundEmitterRetriever>().AsSingleton();
		}
	}
}
