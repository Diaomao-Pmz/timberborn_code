using System;
using Bindito.Core;
using Bindito.Unity;

namespace Timberborn.Common
{
	// Token: 0x02000011 RID: 17
	[Context("Bootstrapper")]
	public class CommonConfigurator : Configurator
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002B3C File Offset: 0x00000D3C
		public override void Configure()
		{
			base.Bind<IRandomNumberGenerator>().To<RandomNumberGenerator>().AsSingleton().AsExported();
			base.Bind<IFakeRandomNumberGeneratorFactory>().To<FakeRandomNumberGeneratorFactory>().AsSingleton().AsExported();
			base.Bind<BoundsCalculator>().AsSingleton().AsExported();
			base.MultiBind<ISceneInitializer>().To<InstantiatingSceneInitializer<ApplicationFocusLogger>>().AsSingleton();
		}
	}
}
