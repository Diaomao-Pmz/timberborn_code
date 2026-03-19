using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000018 RID: 24
	public class GatePathTransformer : IPathTransformer, ILoadableSingleton
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00003E32 File Offset: 0x00002032
		public GatePathTransformer(IBlockService blockService, NavMeshGroupService navMeshGroupService)
		{
			this._blockService = blockService;
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003E48 File Offset: 0x00002048
		public void Load()
		{
			this._validGroupId = this._navMeshGroupService.GetDefaultGroupId();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003E5C File Offset: 0x0000205C
		public bool Transform(ref int index, ReadOnlyList<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners)
		{
			FlowFieldPathNode flowFieldPathNode = flowFieldPath[index];
			if (flowFieldPathNode.GroupId == this._validGroupId)
			{
				if (this.TryAdjustSpeedOnGate(ref index, pathCorners, flowFieldPathNode, flowFieldPathNode))
				{
					return true;
				}
				if (index < flowFieldPath.Count - 1)
				{
					FlowFieldPathNode nodePosition = flowFieldPath[index + 1];
					if (this.TryAdjustSpeedOnGate(ref index, pathCorners, flowFieldPathNode, nodePosition))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003EB8 File Offset: 0x000020B8
		public bool TryAdjustSpeedOnGate(ref int index, List<PathCorner> pathCorners, FlowFieldPathNode referenceNode, FlowFieldPathNode nodePosition)
		{
			Vector3Int coordinates = CoordinateSystem.WorldToGridInt(nodePosition.Position);
			if (this._blockService.GetPathObjectComponentAt<Gate>(coordinates))
			{
				pathCorners.Add(new PathCorner(referenceNode.Position, 1f, this._validGroupId));
				index++;
				return true;
			}
			return false;
		}

		// Token: 0x04000066 RID: 102
		public readonly IBlockService _blockService;

		// Token: 0x04000067 RID: 103
		public readonly NavMeshGroupService _navMeshGroupService;

		// Token: 0x04000068 RID: 104
		public int _validGroupId;
	}
}
