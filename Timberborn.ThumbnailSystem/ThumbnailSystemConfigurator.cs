using System;
using Bindito.Core;

namespace Timberborn.ThumbnailSystem
{
	// Token: 0x02000007 RID: 7
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class ThumbnailSystemConfigurator : Configurator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002258 File Offset: 0x00000458
		public override void Configure()
		{
			base.Bind<ThumbnailSerializer>().AsSingleton();
		}
	}
}
