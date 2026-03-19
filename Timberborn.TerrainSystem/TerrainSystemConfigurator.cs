using System;
using Bindito.Core;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000016 RID: 22
	[Context("Game")]
	[Context("MapEditor")]
	public class TerrainSystemConfigurator : Configurator
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00003F94 File Offset: 0x00002194
		public override void Configure()
		{
			base.Bind<ITerrainService>().To<TerrainService>().AsSingleton();
			base.Bind<TerrainMap>().AsSingleton();
			base.Bind<ColumnTerrainMap>().AsSingleton();
			base.Bind<IThreadSafeColumnTerrainMap>().To<ThreadSafeColumnTerrainMap>().AsSingleton();
			base.Bind<TerrainCutout>().AsSingleton();
			base.Bind<CeilingRetriever>().AsSingleton();
		}
	}
}
