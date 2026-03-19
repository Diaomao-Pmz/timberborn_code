using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200000A RID: 10
	public class BlockObjectPlacerService
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000293D File Offset: 0x00000B3D
		public BlockObjectPlacerService(DefaultBlockObjectPlacer defaultBlockObjectPlacer, IEnumerable<IBlockObjectPlacer> blockObjectPlacers)
		{
			this._defaultBlockObjectPlacer = defaultBlockObjectPlacer;
			this._blockObjectPlacers = blockObjectPlacers.ToList<IBlockObjectPlacer>();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002958 File Offset: 0x00000B58
		public IBlockObjectPlacer GetMatchingPlacer(BlockObjectSpec spec)
		{
			IBlockObjectPlacer blockObjectPlacer = this._blockObjectPlacers.SingleOrDefault((IBlockObjectPlacer placer) => placer.CanHandle(spec));
			return blockObjectPlacer ?? this._defaultBlockObjectPlacer;
		}

		// Token: 0x04000021 RID: 33
		public readonly DefaultBlockObjectPlacer _defaultBlockObjectPlacer;

		// Token: 0x04000022 RID: 34
		public readonly List<IBlockObjectPlacer> _blockObjectPlacers;
	}
}
