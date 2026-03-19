using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000018 RID: 24
	[Context("Game")]
	[Context("MapEditor")]
	public class TerrainSystemRenderingConfigurator : Configurator
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00004F3C File Offset: 0x0000313C
		public override void Configure()
		{
			base.Bind<TerrainBlockRandomizer>().AsSingleton();
			base.Bind<TerrainTopMeshService>().AsSingleton();
			base.Bind<TerrainMeshManager>().AsSingleton();
			base.Bind<TerrainBlockRepository>().AsSingleton();
			base.Bind<TerrainMaterialMap>().AsSingleton();
			base.Bind<SurfaceBlockCollectionFactory>().AsSingleton();
			base.Bind<TerrainLayerSliceUpdater>().AsSingleton();
			base.Bind<TerrainHighlightingService>().AsSingleton();
			base.MultiBind<IDevModule>().To<TerrainSystemRenderingDevModule>().AsSingleton();
		}
	}
}
