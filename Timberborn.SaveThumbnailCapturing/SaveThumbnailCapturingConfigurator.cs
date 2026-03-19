using System;
using Bindito.Core;
using Timberborn.SaveSystem;
using Timberborn.ThumbnailCapturing;

namespace Timberborn.SaveThumbnailCapturing
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class SaveThumbnailCapturingConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<IThumbnailRenderTextureProvider>().To<SaveThumbnailRenderTextureProvider>().AsSingleton();
			base.MultiBind<ISaveEntryWriter>().To<SaveThumbnailSaveEntryWriter>().AsSingleton();
			base.MultiBind<IThumbnailRenderingListener>().To<SaveThumbnailRenderingListener>().AsSingleton();
		}
	}
}
