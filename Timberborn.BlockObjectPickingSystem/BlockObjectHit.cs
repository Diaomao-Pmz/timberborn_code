using System;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x02000004 RID: 4
	public readonly struct BlockObjectHit
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public BlockObject BlockObject { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public Block HitBlock { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public Vector3Int HitProjectedOnGround { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		public BlockObjectHit(BlockObject blockObject, Block hitBlock, Vector3Int hitProjectedOnGround)
		{
			this.BlockObject = blockObject;
			this.HitBlock = hitBlock;
			this.HitProjectedOnGround = hitProjectedOnGround;
		}
	}
}
