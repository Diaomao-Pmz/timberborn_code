using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200000D RID: 13
	public class BlockObjectAtopDeletionBlocker : BaseComponent, IAwakableComponent, IBlockObjectDeletionBlocker
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000030B3 File Offset: 0x000012B3
		public BlockObjectAtopDeletionBlocker(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000030C2 File Offset: 0x000012C2
		public bool NoForcedDelete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000030C2 File Offset: 0x000012C2
		public bool IsStackedDeletionBlocked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000030C5 File Offset: 0x000012C5
		public bool IsDeletionBlocked
		{
			get
			{
				return this.HasBlockingObjectAtop();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000030CD File Offset: 0x000012CD
		public string ReasonLocKey
		{
			get
			{
				return "DeletionBlocker.ObjectAtop";
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000030D4 File Offset: 0x000012D4
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000030E4 File Offset: 0x000012E4
		public bool HasBlockingObjectAtop()
		{
			foreach (Block block in this._blockObject.PositionedBlocks.GetOccupiedStackableBlocks())
			{
				Vector3Int coordinates = block.Coordinates.Above();
				foreach (BlockObject blockObject in this._blockService.GetStackedObjectsWithUndergroundAt(coordinates))
				{
					if (blockObject != this._blockObject && !blockObject.Overridable)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000046 RID: 70
		public readonly IBlockService _blockService;

		// Token: 0x04000047 RID: 71
		public BlockObject _blockObject;
	}
}
