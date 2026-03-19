using System;
using Bindito.Core;

namespace Timberborn.SceneLoading
{
	// Token: 0x0200000B RID: 11
	[Context("Bootstrapper")]
	public class SceneLoadingConfigurator : Configurator
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000025BA File Offset: 0x000007BA
		public override void Configure()
		{
			base.Bind<LoadingScreen>().AsSingleton().AsExported();
			base.Bind<CoroutineStarter>().AsSingleton().AsExported();
			base.Bind<ISceneLoader>().To<SceneLoader>().AsSingleton().AsExported();
		}
	}
}
