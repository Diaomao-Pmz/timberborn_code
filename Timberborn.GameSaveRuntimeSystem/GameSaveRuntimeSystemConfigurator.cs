using System;
using Bindito.Core;
using Bindito.Unity;

namespace Timberborn.GameSaveRuntimeSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class GameSaveRuntimeSystemConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002424 File Offset: 0x00000624
		public override void Configure()
		{
			base.Bind<GameSaver>().AsSingleton();
			base.Bind<GameLoader>().AsSingleton();
			base.MultiBind<ISceneInitializer>().To<InstantiatingSceneInitializer<GameSaverUnityAdapter>>().AsSingleton();
		}
	}
}
