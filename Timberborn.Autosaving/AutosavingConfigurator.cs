using System;
using Bindito.Core;
using Bindito.Unity;

namespace Timberborn.Autosaving
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class AutosavingConfigurator : Configurator
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000026BB File Offset: 0x000008BB
		public override void Configure()
		{
			base.Bind<Autosaver>().AsSingleton();
			base.Bind<AutosaveNameService>().AsSingleton();
			base.MultiBind<ISceneInitializer>().To<InstantiatingSceneInitializer<AutosaverUnityAdapter>>().AsSingleton();
		}
	}
}
