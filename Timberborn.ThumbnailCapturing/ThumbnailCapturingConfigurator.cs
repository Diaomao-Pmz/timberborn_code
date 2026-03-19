using System;
using Bindito.Core;

namespace Timberborn.ThumbnailCapturing
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class ThumbnailCapturingConfigurator : Configurator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002170 File Offset: 0x00000370
		public override void Configure()
		{
			base.Bind<ThumbnailCamera>().AsSingleton();
			base.Bind<ThumbnailRenderer>().AsSingleton();
			base.Bind<ThumbnailSaveEntryWriter>().AsSingleton();
		}
	}
}
