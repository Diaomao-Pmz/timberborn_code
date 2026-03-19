using System;
using Bindito.Core;

namespace Timberborn.MapThumbnail
{
	// Token: 0x02000007 RID: 7
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class MapThumbnailConfigurator : Configurator
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002140 File Offset: 0x00000340
		public override void Configure()
		{
			base.Bind<MapThumbnailConfiguration>().AsSingleton();
			base.Bind<MapThumbnailSaveEntryReader>().AsSingleton();
			base.Bind<MapThumbnailCache>().AsSingleton();
		}
	}
}
