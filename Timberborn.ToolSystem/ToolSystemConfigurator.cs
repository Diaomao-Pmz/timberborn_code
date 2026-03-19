using System;
using Bindito.Core;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000018 RID: 24
	[Context("Game")]
	[Context("MapEditor")]
	public class ToolSystemConfigurator : Configurator
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002832 File Offset: 0x00000A32
		public override void Configure()
		{
			base.Bind<ToolService>().AsSingleton();
			base.Bind<ToolGroupService>().AsSingleton();
			base.Bind<ToolUnlockingService>().AsSingleton();
		}
	}
}
