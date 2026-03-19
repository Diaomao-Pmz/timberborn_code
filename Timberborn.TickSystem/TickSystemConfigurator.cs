using System;
using Bindito.Core;
using Bindito.Unity;

namespace Timberborn.TickSystem
{
	// Token: 0x0200001D RID: 29
	[Context("Game")]
	[Context("MapEditor")]
	public class TickSystemConfigurator : Configurator
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00002C84 File Offset: 0x00000E84
		public override void Configure()
		{
			base.Bind<Ticker>().AsSingleton();
			base.Bind<ITickService>().To<TickService>().AsSingleton();
			base.Bind<ITickableBucketService>().To<TickableBucketService>().AsSingleton();
			base.Bind<ITickableSingletonService>().To<TickableSingletonService>().AsSingleton();
			base.Bind<TickableEntityLifecycleManager>().AsSingleton();
			base.Bind<TickOnlyArrayService>().AsSingleton();
			base.MultiBind<ISceneInitializer>().To<InstantiatingSceneInitializer<TickerUnityAdapter>>().AsSingleton();
		}
	}
}
