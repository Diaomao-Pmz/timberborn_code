using System;
using Bindito.Core;

namespace Timberborn.SteamWorkshopModUploadingUI
{
	// Token: 0x02000008 RID: 8
	[Context("MainMenu")]
	public class SteamWorkshopModUploadingUIConfigurator : Configurator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000025AB File Offset: 0x000007AB
		public override void Configure()
		{
			base.Bind<SteamWorkshopModUploader>().AsSingleton();
			base.Bind<SteamWorkshopUploadableModFactory>().AsSingleton();
		}
	}
}
