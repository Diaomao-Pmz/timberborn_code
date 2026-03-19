using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CharacterMovementSystem;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.Navigation;
using Timberborn.Persistence;
using Timberborn.WalkingSystem;
using Timberborn.WorldPersistence;
using Timberborn.ZiplineSystem;
using UnityEngine;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x0200000B RID: 11
	public class ZiplinePathTracker : BaseComponent, IAwakableComponent, IInitializableEntity, IPostLoadableEntity, IPersistentEntity, INavMeshProximityValidator, IPathStartProvider, ICitizenPositionOverrider
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002625 File Offset: 0x00000825
		public ZiplinePathTracker(IBlockService blockService, ZiplineGroupService ziplineGroupService)
		{
			this._blockService = blockService;
			this._ziplineGroupService = ziplineGroupService;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000263B File Offset: 0x0000083B
		public void Awake()
		{
			this._walker = base.GetComponent<Walker>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002649 File Offset: 0x00000849
		public void InitializeEntity()
		{
			this._walker.PathFollower.MovedAlongPath += this.OnMovedAlongPath;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002668 File Offset: 0x00000868
		public void Save(IEntitySaver entitySaver)
		{
			if (this._fromPoint != null && this._toPoint != null)
			{
				IObjectSaver component = entitySaver.GetComponent(ZiplinePathTracker.ZiplinePathTrackerKey);
				component.Set(ZiplinePathTracker.FromPointKey, this._fromPoint.Value);
				component.Set(ZiplinePathTracker.ToPointKey, this._toPoint.Value);
				component.Set(ZiplinePathTracker.LastMovementSpeedKey, this._lastMovementSpeed);
				if (this._nextTurnPoint != null)
				{
					component.Set(ZiplinePathTracker.NextTurnPointKey, this._nextTurnPoint.Value);
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026FC File Offset: 0x000008FC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(ZiplinePathTracker.ZiplinePathTrackerKey, out objectLoader))
			{
				this._fromPoint = new Vector3?(objectLoader.Get(ZiplinePathTracker.FromPointKey));
				this._toPoint = new Vector3?(objectLoader.Get(ZiplinePathTracker.ToPointKey));
				this._lastMovementSpeed = objectLoader.Get(ZiplinePathTracker.LastMovementSpeedKey);
				if (objectLoader.Has<Vector3>(ZiplinePathTracker.NextTurnPointKey))
				{
					this._nextTurnPoint = new Vector3?(objectLoader.Get(ZiplinePathTracker.NextTurnPointKey));
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002778 File Offset: 0x00000978
		public void PostLoadEntity()
		{
			this.ValidateCurrentEdge();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002780 File Offset: 0x00000980
		public bool IsOnNavMesh()
		{
			this.ValidateCurrentEdge();
			Vector3 vector;
			return this.IsCurrentlyOnZiplineEdge(out vector);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000279C File Offset: 0x0000099C
		public bool TryGetPathStart(IDestination destination, List<PathCorner> pathCorners, out Vector3 start)
		{
			Vector3 vector;
			if (this.IsCurrentlyOnZiplineEdge(out vector))
			{
				start = vector;
				float lastMovementSpeed = this._lastMovementSpeed;
				pathCorners.Add(new PathCorner(base.Transform.position, lastMovementSpeed, this._ziplineGroupService.PathStartGroupId));
				pathCorners.Add(new PathCorner(vector, lastMovementSpeed, this._ziplineGroupService.RegularGroupId));
				if (this._nextTurnPoint != null)
				{
					pathCorners.Add(new PathCorner(this._nextTurnPoint.Value, lastMovementSpeed, this._ziplineGroupService.TurnGroupId));
				}
				return true;
			}
			start = default(Vector3);
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002834 File Offset: 0x00000A34
		public bool TryGetOverridenPosition(out Vector3 position)
		{
			return this.IsCurrentlyOnZiplineEdge(out position);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000283D File Offset: 0x00000A3D
		public void ValidateCurrentEdge()
		{
			if (!this.ZiplineEdgeHasValidConnection())
			{
				this.ClearCurrentEdge();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002850 File Offset: 0x00000A50
		public void OnMovedAlongPath(object sender, MovementEventArgs movementEventArgs)
		{
			if (this.IsMovingOnZiplineEdge(movementEventArgs))
			{
				PathCorner from = movementEventArgs.From;
				PathCorner to = movementEventArgs.To;
				if (this.EnteredNewZiplineEdge(from, to))
				{
					this._fromPoint = new Vector3?(from.Position);
					this._toPoint = new Vector3?(to.Position);
					this._lastMovementSpeed = from.Speed;
					this.PeekNextTurningCorner(movementEventArgs);
					return;
				}
			}
			else
			{
				this.ClearCurrentEdge();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028C0 File Offset: 0x00000AC0
		public bool IsMovingOnZiplineEdge(MovementEventArgs movementEventArgs)
		{
			return this._ziplineGroupService.IsAnyZiplineGroup(movementEventArgs.From.GroupId) && this._ziplineGroupService.IsAnyZiplineGroup(movementEventArgs.To.GroupId);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002908 File Offset: 0x00000B08
		public bool EnteredNewZiplineEdge(PathCorner fromCorner, PathCorner toCorner)
		{
			if (!this._ziplineGroupService.IsRegularEdge(fromCorner.GroupId, toCorner.GroupId) && !this._ziplineGroupService.IsTurnEdge(toCorner.GroupId, fromCorner.GroupId))
			{
				return false;
			}
			if (this._fromPoint != null)
			{
				Vector3? vector = this._fromPoint;
				Vector3 position = fromCorner.Position;
				if (vector != null && (vector == null || !(vector.GetValueOrDefault() != position)))
				{
					return false;
				}
			}
			if (this._toPoint != null)
			{
				Vector3? vector = this._toPoint;
				Vector3 position = toCorner.Position;
				return vector == null || (vector != null && vector.GetValueOrDefault() != position);
			}
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029DC File Offset: 0x00000BDC
		public bool ZiplineEdgeHasValidConnection()
		{
			if (this._fromPoint != null && this._toPoint != null)
			{
				Vector3Int vector3Int = CoordinateSystem.WorldToGridInt(this._fromPoint.Value);
				Vector3Int vector3Int2 = CoordinateSystem.WorldToGridInt(this._toPoint.Value);
				ZiplineTower bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<ZiplineTower>(vector3Int);
				ZiplineTower bottomObjectComponentAt2 = this._blockService.GetBottomObjectComponentAt<ZiplineTower>(vector3Int2);
				return bottomObjectComponentAt && bottomObjectComponentAt2 && vector3Int == bottomObjectComponentAt.CableAnchorPointInt && vector3Int2 == bottomObjectComponentAt2.CableAnchorPointInt && (bottomObjectComponentAt == bottomObjectComponentAt2 || bottomObjectComponentAt.IsConnectedTo(bottomObjectComponentAt2));
			}
			return false;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A7F File Offset: 0x00000C7F
		public bool IsCurrentlyOnZiplineEdge(out Vector3 destination)
		{
			destination = this._toPoint.GetValueOrDefault();
			return this._fromPoint != null && this._toPoint != null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002AAC File Offset: 0x00000CAC
		public void PeekNextTurningCorner(MovementEventArgs movementEventArgs)
		{
			if (movementEventArgs.Next != null && this._ziplineGroupService.IsTurnEdge(movementEventArgs.To.GroupId, movementEventArgs.Next.Value.GroupId))
			{
				this._nextTurnPoint = new Vector3?(movementEventArgs.Next.Value.Position);
				return;
			}
			this._nextTurnPoint = null;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B2C File Offset: 0x00000D2C
		public void ClearCurrentEdge()
		{
			this._fromPoint = null;
			this._toPoint = null;
			this._nextTurnPoint = null;
			this._lastMovementSpeed = 0f;
		}

		// Token: 0x04000017 RID: 23
		public static readonly ComponentKey ZiplinePathTrackerKey = new ComponentKey("ZiplinePathTracker");

		// Token: 0x04000018 RID: 24
		public static readonly PropertyKey<Vector3> FromPointKey = new PropertyKey<Vector3>("FromPoint");

		// Token: 0x04000019 RID: 25
		public static readonly PropertyKey<Vector3> ToPointKey = new PropertyKey<Vector3>("ToPoint");

		// Token: 0x0400001A RID: 26
		public static readonly PropertyKey<Vector3> NextTurnPointKey = new PropertyKey<Vector3>("NextTurnPoint");

		// Token: 0x0400001B RID: 27
		public static readonly PropertyKey<float> LastMovementSpeedKey = new PropertyKey<float>("LastMovementSpeed");

		// Token: 0x0400001C RID: 28
		public readonly IBlockService _blockService;

		// Token: 0x0400001D RID: 29
		public readonly ZiplineGroupService _ziplineGroupService;

		// Token: 0x0400001E RID: 30
		public Walker _walker;

		// Token: 0x0400001F RID: 31
		public Vector3? _fromPoint;

		// Token: 0x04000020 RID: 32
		public Vector3? _toPoint;

		// Token: 0x04000021 RID: 33
		public Vector3? _nextTurnPoint;

		// Token: 0x04000022 RID: 34
		public float _lastMovementSpeed;
	}
}
