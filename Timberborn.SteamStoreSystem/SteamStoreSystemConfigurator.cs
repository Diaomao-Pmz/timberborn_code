using System;
using Bindito.Core;
using Timberborn.StoreSystem;

namespace Timberborn.SteamStoreSystem
{
	// Token: 0x02000008 RID: 8
	[Context("Bootstrapper")]
	public class SteamStoreSystemConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000025B7 File Offset: 0x000007B7
		public override void Configure()
		{
			base.Bind<IStore>().To<SteamStore>().AsSingleton().AsExported();
			base.Bind<SteamManager>().AsSingleton().AsExported();
			base.Bind<SteamLanguage>().AsSingleton();
		}
	}
}
