using System;
using Bindito.Core;

namespace Timberborn.AssetSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Bootstrapper")]
	public class AssetSystemConfigurator : Configurator
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000023FC File Offset: 0x000005FC
		public override void Configure()
		{
			base.Bind<IAssetLoader>().To<AssetLoader>().AsSingleton().AsExported();
			base.MultiBind<IAssetProvider>().To<ResourceAssetProvider>().AsSingleton();
		}
	}
}
