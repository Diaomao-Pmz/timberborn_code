using System;
using UnityEngine;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x02000016 RID: 22
	public readonly struct SelectionStart
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000327B File Offset: 0x0000147B
		public Vector3Int Coordinates { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003283 File Offset: 0x00001483
		public int ReferenceTerrainLevel { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000328B File Offset: 0x0000148B
		public int VerticalOffset { get; }

		// Token: 0x06000060 RID: 96 RVA: 0x00003293 File Offset: 0x00001493
		public SelectionStart(Vector3Int coordinates, BlockObjectHit? originalBlockObjectHit, int verticalOffset)
		{
			this.Coordinates = coordinates;
			this._originalBlockObjectHit = originalBlockObjectHit;
			this.ReferenceTerrainLevel = coordinates.z;
			this.VerticalOffset = verticalOffset;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000032B8 File Offset: 0x000014B8
		public SelectionStart(Vector3Int coordinates)
		{
			this = new SelectionStart(coordinates, null, 0);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032D8 File Offset: 0x000014D8
		public SelectionStart(BlockObjectHit blockObjectHit)
		{
			this = new SelectionStart(blockObjectHit.HitProjectedOnGround, new BlockObjectHit?(blockObjectHit), 0);
			if (blockObjectHit.BlockObject.PositionedBlocks.GetBlock(this.Coordinates).Underground)
			{
				this.ReferenceTerrainLevel = this.Coordinates.z + 1;
				this.VerticalOffset = -1;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003338 File Offset: 0x00001538
		public BlockObjectHit? GetBlockObjectHit()
		{
			if (this._originalBlockObjectHit != null && this._originalBlockObjectHit.Value.BlockObject && this._originalBlockObjectHit.Value.BlockObject.PositionedBlocks.HasBlockAt(this.Coordinates))
			{
				return this._originalBlockObjectHit;
			}
			return null;
		}

		// Token: 0x04000045 RID: 69
		public readonly BlockObjectHit? _originalBlockObjectHit;
	}
}
