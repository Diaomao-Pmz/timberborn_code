using System;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x02000007 RID: 7
	public class ConnectionService : IConnectionService
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public ConnectionService(IBlockService blockService, PreviewBlockService previewBlockService, INavMeshService navMeshService, IPathService pathService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
			this._navMeshService = navMeshService;
			this._pathService = pathService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		public bool CanConnectInDirection(Vector3Int origin, Direction2D direction2D)
		{
			Vector3Int target = origin + direction2D.ToOffset();
			return this.CanConnect(origin, target);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000214A File Offset: 0x0000034A
		public bool CanConnect(Vector3Int origin, Vector3Int target)
		{
			return this.IsPath(origin, target) || this.IsEntrance(origin, target) || this.IsEnforced(origin, target);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216A File Offset: 0x0000036A
		public bool IsPath(Vector3Int origin, Vector3Int target)
		{
			return this._navMeshService.AreConnectedRoadPreview(origin, target) && this._pathService.IsPath(origin) && this._pathService.IsPath(target);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002197 File Offset: 0x00000397
		public bool IsEntrance(Vector3Int origin, Vector3Int target)
		{
			return this.IsEntranceInDirectionAt(origin, target) || this.IsEntranceInDirectionAt(target, origin);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B0 File Offset: 0x000003B0
		public bool IsEntranceInDirectionAt(Vector3Int entranceCoordinates, Vector3Int doorstepCoordinates)
		{
			if (this._navMeshService.AreConnectedPreview(entranceCoordinates, doorstepCoordinates) && this._pathService.IsPath(entranceCoordinates))
			{
				Direction2D direction2D = ConnectionService.ToEntranceDirection(entranceCoordinates - doorstepCoordinates);
				return ((this._blockService.GetEntrancesAt(entranceCoordinates) | this._previewBlockService.GetEntrancesAt(entranceCoordinates)) & direction2D.ToDirections()) > Directions2D.None;
			}
			return false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000220C File Offset: 0x0000040C
		public static Direction2D ToEntranceDirection(Vector3Int direction)
		{
			if (direction == Direction2D.Down.ToOffset())
			{
				return Direction2D.Down;
			}
			if (direction == Direction2D.Left.ToOffset())
			{
				return Direction2D.Left;
			}
			if (direction == Direction2D.Right.ToOffset())
			{
				return Direction2D.Right;
			}
			if (direction == Direction2D.Up.ToOffset())
			{
				return Direction2D.Up;
			}
			throw new ArgumentOutOfRangeException("direction", direction, null);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000226C File Offset: 0x0000046C
		public bool IsEnforced(Vector3Int origin, Vector3Int target)
		{
			object obj = this._blockService.GetFirstObjectWithComponentAt<IPathConnectionEnforcer>(origin) ?? this._previewBlockService.GetFirstObjectWithComponentAt<IPathConnectionEnforcer>(origin);
			IPathConnectionEnforcer pathConnectionEnforcer = this._blockService.GetFirstObjectWithComponentAt<IPathConnectionEnforcer>(target) ?? this._previewBlockService.GetFirstObjectWithComponentAt<IPathConnectionEnforcer>(target);
			object obj2 = obj;
			if (obj2 == null)
			{
				return pathConnectionEnforcer != null && pathConnectionEnforcer.CanConnectPath(origin, target);
			}
			return ((IPathConnectionEnforcer)obj2).CanConnectPath(origin, target);
		}

		// Token: 0x04000008 RID: 8
		public readonly IBlockService _blockService;

		// Token: 0x04000009 RID: 9
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400000A RID: 10
		public readonly INavMeshService _navMeshService;

		// Token: 0x0400000B RID: 11
		public readonly IPathService _pathService;
	}
}
