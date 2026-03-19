using System;
using Bindito.Core;

namespace Timberborn.Modding
{
	// Token: 0x0200000F RID: 15
	[Context("Bootstrapper")]
	public class ModdingConfigurator : Configurator
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public override void Configure()
		{
			base.Bind<ModRepository>().AsSingleton().AsExported();
			base.Bind<ModSorter>().AsSingleton().AsExported();
			base.Bind<ModLoader>().AsSingleton();
			base.Bind<ManifestLoader>().AsSingleton();
			base.MultiBind<IModsProvider>().To<UserFolderModsProvider>().AsSingleton();
		}
	}
}
