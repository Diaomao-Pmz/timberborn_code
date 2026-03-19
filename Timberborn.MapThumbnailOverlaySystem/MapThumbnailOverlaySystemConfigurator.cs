using System;
using Bindito.Core;
using Timberborn.SaveSystem;

namespace Timberborn.MapThumbnailOverlaySystem
{
	// Token: 0x02000008 RID: 8
	[Context("MapEditor")]
	public class MapThumbnailOverlaySystemConfigurator : Configurator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000024C8 File Offset: 0x000006C8
		public override void Configure()
		{
			base.Bind<MapThumbnailOverlay>().AsSingleton();
			base.Bind<MapThumbnailOverlaySerializer>().AsSingleton();
			base.MultiBind<ISaveEntryWriter>().To<MapThumbnailOverlaySaveEntryWriter>().AsSingleton();
		}
	}
}
