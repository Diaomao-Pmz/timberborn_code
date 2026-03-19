using System;
using Bindito.Core;

namespace Timberborn.PlayerDataSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Bootstrapper")]
	public class PlayerDataSystemConfigurator : Configurator
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000026AE File Offset: 0x000008AE
		public override void Configure()
		{
			base.Bind<IPlayerDataService>().To<PlayerDataService>().AsSingleton().AsExported();
			base.Bind<PlayerDataSerializer>().AsSingleton();
			base.Bind<PlayerDataFileService>().AsSingleton();
		}
	}
}
