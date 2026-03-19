using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000062 RID: 98
	public class PreviewBlockService : ILoadableSingleton
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00007A8D File Offset: 0x00005C8D
		public PreviewBlockService(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007A9C File Offset: 0x00005C9C
		public void Load()
		{
			this._blocks = new Array3D<WorldBlock>(this._blockService.Size, () => default(WorldBlock));
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007AD3 File Offset: 0x00005CD3
		public void SetPreview(BlockObject blockObject)
		{
			this.SetOrUnsetPreview(blockObject, true);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007ADD File Offset: 0x00005CDD
		public void UnsetPreview(BlockObject blockObject)
		{
			this.SetOrUnsetPreview(blockObject, false);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public BlockObject GetBottomPreviewAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Bottom;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007B0C File Offset: 0x00005D0C
		public IEnumerable<BlockObject> GetPreviewsAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).BlockObjects;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007B34 File Offset: 0x00005D34
		public BlockObject GetPathObjectAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Path;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007B58 File Offset: 0x00005D58
		public T GetBottomObjectComponentAt<T>(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Bottom.GetComponentOfNullable<T>();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007B80 File Offset: 0x00005D80
		public T GetPathObjectComponentAt<T>(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Path.GetComponentOfNullable<T>();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007BA6 File Offset: 0x00005DA6
		public IEnumerable<T> GetObjectsWithComponentAt<T>(Vector3Int coordinates) where T : BaseComponent
		{
			using (List<BlockObject>.Enumerator enumerator = this._blocks.GetCopyAtOrDefault(coordinates).BlockObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T t;
					if (enumerator.Current.TryGetComponent<T>(out t))
					{
						yield return t;
					}
				}
			}
			List<BlockObject>.Enumerator enumerator = default(List<BlockObject>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public T GetFirstObjectWithComponentAt<T>(Vector3Int coordinates)
		{
			using (List<BlockObject>.Enumerator enumerator = this._blocks.GetCopyAtOrDefault(coordinates).BlockObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T result;
					if (enumerator.Current.TryGetComponent<T>(out result))
					{
						return result;
					}
				}
			}
			return default(T);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007C38 File Offset: 0x00005E38
		public Directions2D GetEntrancesAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Entrances;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007C5C File Offset: 0x00005E5C
		public bool IsUnfinishedGroundBlockAt(Vector3Int coordinates)
		{
			ReadOnlyList<BlockObject> blockObjects = this._blocks.GetCopyAtOrDefault(coordinates).BlockObjects;
			for (int i = 0; i < blockObjects.Count; i++)
			{
				if (blockObjects[i].PositionedBlocks.GetBlock(coordinates).Stackable.IsUnfinishedGround())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007CB5 File Offset: 0x00005EB5
		public void SetOrUnsetPreview(BlockObject blockObject, bool set)
		{
			this.SetOrUnsetBlocks(blockObject, set);
			this.SetOrUnsetEntrance(blockObject, set);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007CC8 File Offset: 0x00005EC8
		public void SetOrUnsetBlocks(BlockObject blockObject, bool set)
		{
			foreach (Block block in blockObject.PositionedBlocks.GetAllBlocks())
			{
				Vector3Int coordinates = block.Coordinates;
				if (this._blocks.Contains(coordinates))
				{
					ref WorldBlock refAt = ref this._blocks.GetRefAt(coordinates);
					if (set)
					{
						refAt.SetBlockObject(blockObject, block);
					}
					else
					{
						refAt.UnsetBlockObject(blockObject, block);
					}
				}
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007D38 File Offset: 0x00005F38
		public void SetOrUnsetEntrance(BlockObject blockObject, bool set)
		{
			if (blockObject.HasEntrance)
			{
				PositionedEntrance positionedEntrance = blockObject.PositionedEntrance;
				Vector3Int coordinates = positionedEntrance.Coordinates;
				if (this._blocks.Contains(coordinates))
				{
					ref WorldBlock refAt = ref this._blocks.GetRefAt(coordinates);
					if (set)
					{
						refAt.AddEntrance(positionedEntrance.Direction2D);
						return;
					}
					refAt.DeleteEntrance(positionedEntrance.Direction2D);
				}
			}
		}

		// Token: 0x04000131 RID: 305
		public readonly IBlockService _blockService;

		// Token: 0x04000132 RID: 306
		public Array3D<WorldBlock> _blocks;
	}
}
