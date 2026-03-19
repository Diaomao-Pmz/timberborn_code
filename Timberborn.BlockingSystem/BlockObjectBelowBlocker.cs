using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BlockingSystem
{
	// Token: 0x0200000F RID: 15
	public class BlockObjectBelowBlocker : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002924 File Offset: 0x00000B24
		public BlockObjectBelowBlocker(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000293E File Offset: 0x00000B3E
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000294C File Offset: 0x00000B4C
		public void Block()
		{
			Asserts.IsFalse<BlockObjectBelowBlocker>(this, this._isBlocked, "_isBlocked");
			this.FillBlockableObjectsBelow();
			foreach (BlockableObject blockableObject in this._blockableObjectsBelow)
			{
				blockableObject.Block(this);
			}
			this._isBlocked = true;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000029BC File Offset: 0x00000BBC
		public void Unblock()
		{
			Asserts.IsTrue<BlockObjectBelowBlocker>(this, this._isBlocked, "_isBlocked");
			foreach (BlockableObject blockableObject in this._blockableObjectsBelow)
			{
				blockableObject.Unblock(this);
			}
			this._blockableObjectsBelow.Clear();
			this._isBlocked = false;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A30 File Offset: 0x00000C30
		public void FillBlockableObjectsBelow()
		{
			foreach (Vector3Int value in this._blockObject.PositionedBlocks.GetFoundationCoordinates())
			{
				foreach (BlockableObject item in this._blockService.GetObjectsWithComponentAt<BlockableObject>(value.Below()))
				{
					this._blockableObjectsBelow.Add(item);
				}
			}
		}

		// Token: 0x04000015 RID: 21
		public readonly IBlockService _blockService;

		// Token: 0x04000016 RID: 22
		public BlockObject _blockObject;

		// Token: 0x04000017 RID: 23
		public readonly List<BlockableObject> _blockableObjectsBelow = new List<BlockableObject>();

		// Token: 0x04000018 RID: 24
		public bool _isBlocked;
	}
}
