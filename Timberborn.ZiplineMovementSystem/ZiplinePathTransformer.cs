using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using Timberborn.ZiplineSystem;
using UnityEngine;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x0200000C RID: 12
	public class ZiplinePathTransformer : IPathTransformer, ILoadableSingleton
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public ZiplinePathTransformer(IBlockService blockService, ISpecService specService, ZiplineGroupService ziplineGroupService)
		{
			this._blockService = blockService;
			this._specService = specService;
			this._ziplineGroupService = ziplineGroupService;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public void Load()
		{
			this._pathSpeed = 1f / this._specService.GetSingleSpec<ZiplineCableNavMeshSpec>().CableUnitCost;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public bool Transform(ref int index, ReadOnlyList<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners)
		{
			if (index < flowFieldPath.Count - 1)
			{
				FlowFieldPathNode from = flowFieldPath[index];
				FlowFieldPathNode flowFieldPathNode = flowFieldPath[index + 1];
				Vector3 start;
				Vector3 end;
				if (this.IsValidEdge(from, flowFieldPathNode) && this.TryGetAnchors(from.Position, flowFieldPathNode.Position, out start, out end))
				{
					ValueTuple<Vector3, Vector3> valueTuple = ZiplineCalculator.CalculateWorldConnections(start, end);
					Vector3 item = valueTuple.Item1;
					Vector3 item2 = valueTuple.Item2;
					int regularGroupId = this._ziplineGroupService.RegularGroupId;
					bool flag = flowFieldPathNode.Cost <= 0f;
					float speed = flag ? float.MaxValue : this._pathSpeed;
					pathCorners.Add(new PathCorner(item + ZiplinePathTransformer.PathOffset, this._pathSpeed, regularGroupId));
					pathCorners.Add(new PathCorner(item2 + ZiplinePathTransformer.PathOffset, speed, regularGroupId));
					if (!flag && index < flowFieldPath.Count - 2)
					{
						FlowFieldPathNode to = flowFieldPath[index + 2];
						this.AddTurn(pathCorners, from, flowFieldPathNode, to, item2);
					}
					index++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D04 File Offset: 0x00000F04
		public bool TryGetAnchors(Vector3 from, Vector3 to, out Vector3 fromAnchor, out Vector3 toAnchor)
		{
			Vector3Int coordinates = CoordinateSystem.WorldToGridInt(from);
			Vector3Int coordinates2 = CoordinateSystem.WorldToGridInt(to);
			ZiplineTower bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<ZiplineTower>(coordinates);
			ZiplineTower bottomObjectComponentAt2 = this._blockService.GetBottomObjectComponentAt<ZiplineTower>(coordinates2);
			if (bottomObjectComponentAt == null || bottomObjectComponentAt2 == null)
			{
				fromAnchor = Vector3.zero;
				toAnchor = Vector3.zero;
				return false;
			}
			fromAnchor = CoordinateSystem.GridToWorld(bottomObjectComponentAt.CableAnchorPoint);
			toAnchor = CoordinateSystem.GridToWorld(bottomObjectComponentAt2.CableAnchorPoint);
			return true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D7C File Offset: 0x00000F7C
		public bool IsValidEdge(FlowFieldPathNode from, FlowFieldPathNode to)
		{
			return from.Cost > 0f && this._ziplineGroupService.IsRegularEdge(from.GroupId, to.GroupId);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public void AddTurn(List<PathCorner> pathCorners, FlowFieldPathNode from, FlowFieldPathNode through, FlowFieldPathNode to, Vector3 throughConnection)
		{
			Vector3 turnStart;
			Vector3 turnEnd;
			if (this.CanAddTurn(from, through, to) && this.TryGetAnchors(through.Position, to.Position, out turnStart, out turnEnd))
			{
				Vector3 vector = ZiplineCalculator.CalculateTurn(turnStart, turnEnd, throughConnection);
				pathCorners.Add(new PathCorner(vector + ZiplinePathTransformer.PathOffset, this._pathSpeed, this._ziplineGroupService.TurnGroupId));
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E0C File Offset: 0x0000100C
		public bool CanAddTurn(FlowFieldPathNode from, FlowFieldPathNode through, FlowFieldPathNode to)
		{
			if (this.IsValidEdge(through, to))
			{
				Vector3 normalized = (through.Position - from.Position).normalized;
				Vector3 normalized2 = (to.Position - through.Position).normalized;
				return Vector3.SignedAngle(normalized, normalized2, Vector3.up) < ZiplinePathTransformer.TurnAngleThreshold;
			}
			return false;
		}

		// Token: 0x04000023 RID: 35
		public static readonly float TurnAngleThreshold = 45f;

		// Token: 0x04000024 RID: 36
		public static readonly Vector3 PathOffset = new Vector3(0f, -0.44f, 0f);

		// Token: 0x04000025 RID: 37
		public readonly IBlockService _blockService;

		// Token: 0x04000026 RID: 38
		public readonly ISpecService _specService;

		// Token: 0x04000027 RID: 39
		public readonly ZiplineGroupService _ziplineGroupService;

		// Token: 0x04000028 RID: 40
		public float _pathSpeed;
	}
}
