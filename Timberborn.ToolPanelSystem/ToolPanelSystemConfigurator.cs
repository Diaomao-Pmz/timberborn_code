using System;
using Bindito.Core;

namespace Timberborn.ToolPanelSystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class ToolPanelSystemConfigurator : Configurator
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002249 File Offset: 0x00000449
		public override void Configure()
		{
			base.Bind<ToolPanel>().AsSingleton();
		}
	}
}
