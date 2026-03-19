using System;
using Bindito.Core;

namespace Timberborn.TerrainUndoSystem
{
	// Token: 0x02000007 RID: 7
	[Context("MapEditor")]
	public class TerrainUndoSystemConfigurator : Configurator
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021A3 File Offset: 0x000003A3
		public override void Configure()
		{
			base.Bind<TerrainUndoableRegistrar>().AsSingleton();
		}
	}
}
