using System;
using Bindito.Core;

namespace Timberborn.MapStateSystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class MapStateSystemConfigurator : Configurator
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002658 File Offset: 0x00000858
		public override void Configure()
		{
			base.Bind<MapSize>().AsSingleton();
		}
	}
}
