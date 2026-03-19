using System;
using Timberborn.BlockSystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000014 RID: 20
	public class BlockSystemNavMeshSizeProvider : INavMeshSizeProvider
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003309 File Offset: 0x00001509
		public BlockSystemNavMeshSizeProvider(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003318 File Offset: 0x00001518
		public Vector3Int Size
		{
			get
			{
				return this._blockService.Size;
			}
		}

		// Token: 0x04000026 RID: 38
		public readonly IBlockService _blockService;
	}
}
