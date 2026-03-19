using System;
using Bindito.Core;

namespace Timberborn.SaveThumbnail
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("Game")]
	public class SaveThumbnailConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020DA File Offset: 0x000002DA
		public override void Configure()
		{
			base.Bind<SaveThumbnailConfiguration>().AsSingleton();
			base.Bind<SaveThumbnailSaveEntryReader>().AsSingleton();
		}
	}
}
