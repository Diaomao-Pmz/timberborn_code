using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200002B RID: 43
	public class BlockService : IBlockService, ILoadableSingleton
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004C77 File Offset: 0x00002E77
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00004C7F File Offset: 0x00002E7F
		public Vector3Int Size { get; private set; }

		// Token: 0x06000125 RID: 293 RVA: 0x00004C88 File Offset: 0x00002E88
		public BlockService(MapSize mapSize, EventBus eventBus)
		{
			this._mapSize = mapSize;
			this._eventBus = eventBus;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004C9E File Offset: 0x00002E9E
		public void Load()
		{
			this.InitializeArrays();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public bool AnyObjectAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).HasAnyObject;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004CCC File Offset: 0x00002ECC
		public bool AnyTopObjectAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Top;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public bool BlockNeedsGroundBelow(Vector3Int coordinates)
		{
			foreach (BlockObject blockObject in this._blocks.GetCopyAtOrDefault(coordinates).BlockObjects)
			{
				MatterBelow matterBelow = blockObject.PositionedBlocks.GetBlock(coordinates).MatterBelow;
				if (matterBelow == MatterBelow.Ground || matterBelow == MatterBelow.GroundOrStackable)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004D78 File Offset: 0x00002F78
		public bool AnyNonOverridableObjectBelow(Vector3Int coordinates)
		{
			int num = -1;
			int i = coordinates.z - 1;
			while (i >= 0)
			{
				Vector3Int coordinates2 = coordinates + new Vector3Int(0, 0, num);
				if (this.AnyNonOverridableObjectsAt(coordinates2, BlockOccupations.All))
				{
					return true;
				}
				i--;
				num--;
			}
			return false;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004DBC File Offset: 0x00002FBC
		public ReadOnlyList<BlockObject> GetObjectsAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).BlockObjects;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004DDD File Offset: 0x00002FDD
		public IEnumerable<BlockObject> GetStackedObjectsAt(Vector3Int coordinates)
		{
			return BlockService.GetStackObjectsAt(coordinates, this._blocks.GetCopyAtOrDefault(coordinates));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004DF1 File Offset: 0x00002FF1
		public IEnumerable<BlockObject> GetStackedObjectsWithUndergroundAt(Vector3Int coordinates)
		{
			WorldBlock block = this._blocks.GetCopyAtOrDefault(coordinates);
			foreach (BlockObject blockObject in BlockService.GetStackObjectsAt(coordinates, block))
			{
				yield return blockObject;
			}
			IEnumerator<BlockObject> enumerator = null;
			if (BlockService.NeedsSolidMatterBelow(coordinates, block.Underground))
			{
				yield return block.Underground;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004E08 File Offset: 0x00003008
		public IEnumerable<T> GetObjectsWithComponentAt<T>(Vector3Int coordinates)
		{
			using (List<BlockObject>.Enumerator enumerator = this.GetObjectsAt(coordinates).GetEnumerator())
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

		// Token: 0x0600012F RID: 303 RVA: 0x00004E20 File Offset: 0x00003020
		public T GetFirstObjectWithComponentAt<T>(Vector3Int coordinates)
		{
			using (List<BlockObject>.Enumerator enumerator = this.GetObjectsAt(coordinates).GetEnumerator())
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

		// Token: 0x06000130 RID: 304 RVA: 0x00004E88 File Offset: 0x00003088
		public void GetIntersectingObjectsAt(Vector3Int coordinates, BlockOccupations occupations, List<BlockObject> result)
		{
			this._blocks.GetCopyAtOrDefault(coordinates).GetIntersectingObjects(occupations, result);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004EAC File Offset: 0x000030AC
		public bool AnyNonOverridableObjectsAt(Vector3Int coordinates, BlockOccupations occupations)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).NonOverridableBlockOccupations.Intersects(occupations);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004ED4 File Offset: 0x000030D4
		public BlockObject GetBottomObjectAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Bottom;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004EF8 File Offset: 0x000030F8
		public BlockObject GetUndergroundObjectAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Underground;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004F19 File Offset: 0x00003119
		public T GetBottomObjectComponentAt<T>(Vector3Int coordinates)
		{
			return this.GetBottomObjectAt(coordinates).GetComponentOfNullable<T>();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004F27 File Offset: 0x00003127
		public T GetPathObjectComponentAt<T>(Vector3Int coordinates)
		{
			return this.GetPathObjectAt(coordinates).GetComponentOfNullable<T>();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004F35 File Offset: 0x00003135
		public T GetMiddleObjectComponentAt<T>(Vector3Int coordinates)
		{
			return this.GetMiddleObjectAt(coordinates).GetComponentOfNullable<T>();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004F43 File Offset: 0x00003143
		public T GetTopObjectComponentAt<T>(Vector3Int coordinates)
		{
			return this.GetTopObjectAt(coordinates).GetComponentOfNullable<T>();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004F54 File Offset: 0x00003154
		public BlockObject GetPathObjectAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Path;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004F78 File Offset: 0x00003178
		public Directions2D GetEntrancesAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Entrances;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004F99 File Offset: 0x00003199
		public bool Contains(Vector3Int coordinates)
		{
			return this._blocks.Contains(coordinates);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004FA7 File Offset: 0x000031A7
		public bool Contains(Vector2Int coordinates)
		{
			return this._blocks.Contains(coordinates);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004FB8 File Offset: 0x000031B8
		public void SetObject(BlockObject blockObject)
		{
			foreach (Block block in blockObject.PositionedBlocks.GetOccupiedAndUndergroundBlocks())
			{
				if (this.Contains(block.Coordinates))
				{
					this._blocks.GetRefAt(block.Coordinates).SetBlockObject(blockObject, block);
				}
			}
			if (blockObject.HasEntrance)
			{
				PositionedEntrance positionedEntrance = blockObject.PositionedEntrance;
				Vector3Int coordinates = positionedEntrance.Coordinates;
				if (this._blocks.Contains(coordinates))
				{
					this._blocks.GetRefAt(positionedEntrance.Coordinates).AddEntrance(positionedEntrance.Direction2D);
				}
			}
			this._eventBus.Post(new BlockObjectSetEvent(blockObject));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000507C File Offset: 0x0000327C
		public void UnsetObject(BlockObject blockObject)
		{
			foreach (Block block in blockObject.PositionedBlocks.GetOccupiedAndUndergroundBlocks())
			{
				if (this.Contains(block.Coordinates))
				{
					this._blocks.GetRefAt(block.Coordinates).UnsetBlockObject(blockObject, block);
				}
			}
			if (blockObject.HasEntrance)
			{
				PositionedEntrance positionedEntrance = blockObject.PositionedEntrance;
				Vector3Int coordinates = positionedEntrance.Coordinates;
				if (this._blocks.Contains(coordinates))
				{
					this._blocks.GetRefAt(positionedEntrance.Coordinates).DeleteEntrance(positionedEntrance.Direction2D);
				}
			}
			this._eventBus.Post(new BlockObjectUnsetEvent(blockObject));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005140 File Offset: 0x00003340
		public static IEnumerable<BlockObject> GetStackObjectsAt(Vector3Int coordinates, WorldBlock block)
		{
			if (BlockService.NeedsSolidMatterBelow(coordinates, block.Floor))
			{
				yield return block.Floor;
			}
			if (BlockService.NeedsSolidMatterBelow(coordinates, block.Path))
			{
				yield return block.Path;
			}
			if (BlockService.NeedsSolidMatterBelow(coordinates, block.Bottom))
			{
				yield return block.Bottom;
			}
			if (BlockService.NeedsSolidMatterBelow(coordinates, block.Corners))
			{
				yield return block.Corners;
			}
			yield break;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005158 File Offset: 0x00003358
		public BlockObject GetMiddleObjectAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Middle;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000517C File Offset: 0x0000337C
		public BlockObject GetTopObjectAt(Vector3Int coordinates)
		{
			return this._blocks.GetCopyAtOrDefault(coordinates).Top;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000051A0 File Offset: 0x000033A0
		public void InitializeArrays()
		{
			this.Size = this._mapSize.TotalSize;
			this._blocks = new Array3D<WorldBlock>(this.Size, () => default(WorldBlock));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000051F0 File Offset: 0x000033F0
		public static bool NeedsSolidMatterBelow(Vector3Int coordinates, BlockObject blockObject)
		{
			return blockObject && blockObject.PositionedBlocks.GetBlock(coordinates).MatterBelow.IsSolidMatter();
		}

		// Token: 0x040000AA RID: 170
		public readonly MapSize _mapSize;

		// Token: 0x040000AB RID: 171
		public readonly EventBus _eventBus;

		// Token: 0x040000AC RID: 172
		public Array3D<WorldBlock> _blocks;
	}
}
