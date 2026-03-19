using System;
using System.Collections.Generic;
using Microsoft.Extensions.ObjectPool;
using Timberborn.EntitySystem;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000052 RID: 82
	public class OverridenBlockObjectService
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x000066C8 File Offset: 0x000048C8
		public OverridenBlockObjectService(IBlockService blockService, EntityService entityService)
		{
			this._blockService = blockService;
			this._entityService = entityService;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000066FC File Offset: 0x000048FC
		public void OverrideBlockObjects(BlockObject blockObject)
		{
			List<BlockObject> list = this._intersectionPool.Get();
			foreach (Block block in blockObject.PositionedBlocks.GetAllBlocks())
			{
				this._blockService.GetIntersectingObjectsAt(block.Coordinates, block.Occupation, this._blockObjectCache);
				foreach (BlockObject blockObject2 in this._blockObjectCache)
				{
					if (blockObject2.Overridable)
					{
						list.Add(blockObject2);
					}
				}
				this._blockObjectCache.Clear();
			}
			foreach (BlockObject intersection in list)
			{
				this.ProcessIntersectingBlockObject(blockObject, intersection);
			}
			this._intersectionPool.Return(list);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006808 File Offset: 0x00004A08
		public void ProcessIntersectingBlockObject(BlockObject blockObject, BlockObject intersection)
		{
			IBlockObjectCustomOverriding component = intersection.GetComponent<IBlockObjectCustomOverriding>();
			if (component != null)
			{
				component.OverrideBy(blockObject);
				return;
			}
			this._entityService.Delete(intersection);
		}

		// Token: 0x040000EF RID: 239
		public readonly IBlockService _blockService;

		// Token: 0x040000F0 RID: 240
		public readonly EntityService _entityService;

		// Token: 0x040000F1 RID: 241
		public readonly List<BlockObject> _blockObjectCache = new List<BlockObject>();

		// Token: 0x040000F2 RID: 242
		public readonly ObjectPool<List<BlockObject>> _intersectionPool = new DefaultObjectPool<List<BlockObject>>(new OverridenBlockObjectService.ListPolicy<BlockObject>());

		// Token: 0x02000053 RID: 83
		public class ListPolicy<T> : PooledObjectPolicy<List<T>>
		{
			// Token: 0x060001F8 RID: 504 RVA: 0x00006833 File Offset: 0x00004A33
			public override List<T> Create()
			{
				return new List<T>();
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x0000683A File Offset: 0x00004A3A
			public override bool Return(List<T> target)
			{
				target.Clear();
				return true;
			}
		}
	}
}
