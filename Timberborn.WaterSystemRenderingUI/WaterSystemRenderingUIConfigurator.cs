using System;
using Bindito.Core;

namespace Timberborn.WaterSystemRenderingUI
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class WaterSystemRenderingUIConfigurator : Configurator
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public override void Configure()
		{
			base.Bind<WaterRenderingDebuggingPanel>().AsSingleton();
			base.Bind<WaterRenderingTimeDebuggingPanel>().AsSingleton();
		}
	}
}
