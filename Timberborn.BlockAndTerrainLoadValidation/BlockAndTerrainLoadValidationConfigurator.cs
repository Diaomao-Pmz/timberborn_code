using System;
using Bindito.Core;
using Timberborn.WorldPersistence;

namespace Timberborn.BlockAndTerrainLoadValidation
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class BlockAndTerrainLoadValidationConfigurator : Configurator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020ED File Offset: 0x000002ED
		public override void Configure()
		{
			base.MultiBind<IEntityBatchLoader>().To<BlockAndTerrainBatchLoader>().AsSingleton();
		}
	}
}
