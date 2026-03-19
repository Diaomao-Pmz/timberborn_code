using System;
using Bindito.Core;
using Bindito.Unity;

namespace Timberborn.SingletonSystem
{
	// Token: 0x02000018 RID: 24
	[Context("Bootstrapper")]
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class SingletonSystemConfigurator : Configurator
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002940 File Offset: 0x00000B40
		public override void Configure()
		{
			base.Bind<EventBus>().AsSingleton();
			base.Bind<ISingletonRepository>().To<SingletonRepository>().AsSingleton();
			base.Bind<SingletonLifecycleService>().AsSingleton();
			SingletonListener singletonListener = new SingletonListener();
			base.Bind<SingletonListener>().ToInstance(singletonListener);
			base.AddInjectionListener(singletonListener);
			base.AddProvisionListener(singletonListener);
			base.MultiBind<ISceneInitializer>().To<InstantiatingSceneInitializer<SingletonLifecycleUnityAdapter>>().AsSingleton();
		}
	}
}
