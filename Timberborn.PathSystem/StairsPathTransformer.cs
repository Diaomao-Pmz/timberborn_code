using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x0200001D RID: 29
	public class StairsPathTransformer : IPathTransformer, ILoadableSingleton
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003A87 File Offset: 0x00001C87
		public StairsPathTransformer(IBlockService blockService, NavMeshGroupService navMeshGroupService)
		{
			this._blockService = blockService;
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003A9D File Offset: 0x00001C9D
		public void Load()
		{
			this._validGroupId = this._navMeshGroupService.GetDefaultGroupId();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public bool Transform(ref int index, ReadOnlyList<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners)
		{
			if (flowFieldPath[index].GroupId != this._validGroupId)
			{
				return false;
			}
			if (index <= 0 || index >= flowFieldPath.Count - 1)
			{
				return this.AdjustPathEnd(ref index, flowFieldPath, pathCorners);
			}
			return this.ApplyStairsPath(ref index, flowFieldPath, pathCorners);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003B04 File Offset: 0x00001D04
		public bool ApplyStairsPath(ref int index, ReadOnlyList<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners)
		{
			FlowFieldPathNode from = flowFieldPath[index - 1];
			FlowFieldPathNode flowFieldPathNode = flowFieldPath[index];
			FlowFieldPathNode flowFieldPathNode2 = flowFieldPath[index + 1];
			if (flowFieldPathNode2.GroupId == this._validGroupId && StairsPathTransformer.IsVerticalEdge(flowFieldPathNode, flowFieldPathNode2))
			{
				Vector3 offset = StairsPathTransformer.GetOffset(from, flowFieldPathNode);
				Vector3 vector = flowFieldPathNode.Position - offset;
				if (pathCorners.Count == 0 || pathCorners[pathCorners.Count - 1].Position != vector)
				{
					pathCorners.Add(new PathCorner(vector, 1f, this._validGroupId));
				}
				bool flag = index < flowFieldPath.Count - 2;
				if (flag)
				{
					Vector3 vector2 = 0.5f * (flowFieldPathNode.Position + flowFieldPathNode2.Position);
					Vector3Int coordinates = CoordinateSystem.WorldToGridInt(vector2);
					if (this._blockService.GetPathObjectComponentAt<SpiralStairsSpec>(coordinates) != null)
					{
						this.ApplySpiralStairsPath(vector2, vector, flowFieldPathNode2, flowFieldPath[index + 2], pathCorners);
					}
					else
					{
						pathCorners.Add(new PathCorner(flowFieldPathNode2.Position + offset, 1f, this._validGroupId));
					}
				}
				index += (flag ? 2 : 1);
				return true;
			}
			return false;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003C44 File Offset: 0x00001E44
		public static bool IsVerticalEdge(FlowFieldPathNode from, FlowFieldPathNode to)
		{
			return Math.Abs(from.Position.y - to.Position.y) > 0.5f;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003C6C File Offset: 0x00001E6C
		public static Vector3 GetOffset(FlowFieldPathNode from, FlowFieldPathNode to)
		{
			Vector3 vector = to.Position - from.Position;
			vector.y = 0f;
			return vector.normalized * 0.5f;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003CAC File Offset: 0x00001EAC
		public void ApplySpiralStairsPath(Vector3 center, Vector3 startPosition, FlowFieldPathNode outboundEdgeStart, FlowFieldPathNode outboundEdgeEnd, List<PathCorner> pathCorners)
		{
			Vector3 vector = outboundEdgeStart.Position + StairsPathTransformer.GetOffset(outboundEdgeStart, outboundEdgeEnd);
			Vector3 normalized = (startPosition - center).normalized;
			Vector3 normalized2 = (vector - center).normalized;
			Vector3 position = center + normalized * 0.275f + normalized2 * 0.125f;
			Vector3 position2 = center + normalized2 * 0.275f + normalized * 0.125f;
			pathCorners.Add(new PathCorner(position, 1f, this._validGroupId));
			pathCorners.Add(new PathCorner(position2, 1f, this._validGroupId));
			pathCorners.Add(new PathCorner(vector, 1f, this._validGroupId));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003D80 File Offset: 0x00001F80
		public bool AdjustPathEnd(ref int index, ReadOnlyList<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners)
		{
			FlowFieldPathNode flowFieldPathNode = flowFieldPath[index];
			Vector3 position = flowFieldPathNode.Position;
			Vector3Int coordinates = CoordinateSystem.WorldToGridInt(position);
			IPathHeightProvider bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<IPathHeightProvider>(coordinates);
			float y;
			if (bottomObjectComponentAt != null && bottomObjectComponentAt.TryGetHeight(position, out y))
			{
				position.y = y;
				pathCorners.Add(new PathCorner(position, flowFieldPathNode.NormalizedSpeed, this._validGroupId));
				index++;
				return true;
			}
			return false;
		}

		// Token: 0x04000052 RID: 82
		public readonly IBlockService _blockService;

		// Token: 0x04000053 RID: 83
		public readonly NavMeshGroupService _navMeshGroupService;

		// Token: 0x04000054 RID: 84
		public int _validGroupId;
	}
}
