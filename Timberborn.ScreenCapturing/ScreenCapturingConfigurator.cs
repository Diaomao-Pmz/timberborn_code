using System;
using Bindito.Core;

namespace Timberborn.ScreenCapturing
{
	// Token: 0x02000004 RID: 4
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class ScreenCapturingConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public override void Configure()
		{
			base.Bind<ScreenshotService>().AsSingleton();
		}
	}
}
