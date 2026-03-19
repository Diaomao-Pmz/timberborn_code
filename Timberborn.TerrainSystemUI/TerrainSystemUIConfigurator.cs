using System;
using Bindito.Core;

namespace Timberborn.TerrainSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class TerrainSystemUIConfigurator : Configurator
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002260 File Offset: 0x00000460
		public override void Configure()
		{
			base.Bind<TerrainDebuggingPanel>().AsSingleton();
		}
	}
}
