using System;
using Bindito.Core;
using Timberborn.SaveSystem;
using Timberborn.ThumbnailCapturing;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x02000008 RID: 8
	[Context("MapEditor")]
	public class MapThumbnailCapturingConfigurator : Configurator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002384 File Offset: 0x00000584
		public override void Configure()
		{
			base.Bind<ThumbnailCameraDefaultPositionProvider>().AsSingleton();
			base.Bind<CameraConfigurationSerializer>().AsSingleton();
			base.Bind<MapThumbnailCameraMover>().AsSingleton();
			base.Bind<IThumbnailRenderTextureProvider>().To<MapThumbnailRenderTextureProvider>().AsSingleton();
			base.MultiBind<ISaveEntryWriter>().To<MapThumbnailSaveEntryWriter>().AsSingleton();
			base.MultiBind<IThumbnailRenderingListener>().To<ShadowThumbnailRenderingListener>().AsSingleton();
			base.MultiBind<IThumbnailRenderingListener>().To<SunThumbnailRenderingListener>().AsSingleton();
			base.MultiBind<IThumbnailRenderingListener>().To<StartingLocationThumbnailRenderingListener>().AsSingleton();
			base.MultiBind<IThumbnailRenderingListener>().To<WaterThumbnailRenderingListener>().AsSingleton();
			base.MultiBind<IThumbnailRenderingListener>().To<SelectionThumbnailRenderingListener>().AsSingleton();
		}
	}
}
