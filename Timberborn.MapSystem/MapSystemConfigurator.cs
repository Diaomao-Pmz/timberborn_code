using System;
using Bindito.Core;

namespace Timberborn.MapSystem
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class MapSystemConfigurator : Configurator
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022BF File Offset: 0x000004BF
		public override void Configure()
		{
			base.Bind<MapLoader>().AsSingleton();
			base.Bind<MapSaver>().AsSingleton();
		}
	}
}
