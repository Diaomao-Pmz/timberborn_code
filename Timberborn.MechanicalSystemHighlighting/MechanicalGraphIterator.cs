using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.MechanicalSystem;
using UnityEngine;

namespace Timberborn.MechanicalSystemHighlighting
{
	// Token: 0x02000008 RID: 8
	public class MechanicalGraphIterator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000232C File Offset: 0x0000052C
		public MechanicalGraphIterator(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002348 File Offset: 0x00000548
		public void Iterate(IEnumerable<MechanicalNode> rootNodes, ICollection<MechanicalNode> graphNodes, bool includeUnfinished)
		{
			this.InitializeStackFromRootNodes(rootNodes, graphNodes);
			while (this._graphStack.Count > 0)
			{
				MechanicalGraphIterator.GraphNode graphNode = this._graphStack.Pop();
				foreach (Direction3D direction in graphNode.Transput.Directions.GetEnumerator())
				{
					MechanicalNode neighborNode = this.GetNeighborNode(graphNode, direction);
					if (neighborNode && !graphNodes.Contains(neighborNode))
					{
						BlockObject component = neighborNode.GetComponent<BlockObject>();
						if (includeUnfinished || component.IsFinished)
						{
							graphNodes.Add(neighborNode);
							foreach (TransputSpec transput in neighborNode.GetComponent<TransputProviderSpec>().Transputs)
							{
								this._graphStack.Push(new MechanicalGraphIterator.GraphNode(transput, component));
							}
						}
					}
				}
			}
			this._graphStack.Clear();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002438 File Offset: 0x00000638
		public void InitializeStackFromRootNodes(IEnumerable<MechanicalNode> rootNodes, ICollection<MechanicalNode> graphNodes)
		{
			foreach (MechanicalNode mechanicalNode in rootNodes)
			{
				if (mechanicalNode && !mechanicalNode.IsDetached)
				{
					graphNodes.Add(mechanicalNode);
					TransputProviderSpec component = mechanicalNode.GetComponent<TransputProviderSpec>();
					BlockObject component2 = mechanicalNode.GetComponent<BlockObject>();
					foreach (TransputSpec transput in component.Transputs)
					{
						this._graphStack.Push(new MechanicalGraphIterator.GraphNode(transput, component2));
					}
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024D4 File Offset: 0x000006D4
		public MechanicalNode GetNeighborNode(MechanicalGraphIterator.GraphNode graphNode, Direction3D direction)
		{
			Vector3Int vector3Int = graphNode.Parent.TransformCoordinates(graphNode.Transput.Coordinates);
			Direction3D direction3D = graphNode.Parent.TransformDirection(direction);
			Vector3Int coordinates = vector3Int + direction3D.ToOffset();
			MechanicalNode firstObjectWithComponentAt = this._blockService.GetFirstObjectWithComponentAt<MechanicalNode>(coordinates);
			if (!MechanicalGraphIterator.IsValidNeighborNode(firstObjectWithComponentAt, vector3Int, direction3D))
			{
				return null;
			}
			return firstObjectWithComponentAt;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002530 File Offset: 0x00000730
		public static bool IsValidNeighborNode(MechanicalNode node, Vector3Int startCoordinates, Direction3D startDirection)
		{
			if (node && !node.IsDetached)
			{
				TransputProviderSpec component = node.GetComponent<TransputProviderSpec>();
				BlockObject component2 = node.GetComponent<BlockObject>();
				foreach (TransputSpec transputSpec in component.Transputs)
				{
					foreach (Direction3D direction3D in transputSpec.Directions.GetEnumerator())
					{
						Direction3D direction3D2 = component2.TransformDirection(direction3D);
						if (component2.TransformCoordinates(transputSpec.Coordinates) + direction3D2.ToOffset() == startCoordinates && direction3D2 == startDirection.Across())
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04000011 RID: 17
		public readonly IBlockService _blockService;

		// Token: 0x04000012 RID: 18
		public readonly Stack<MechanicalGraphIterator.GraphNode> _graphStack = new Stack<MechanicalGraphIterator.GraphNode>();

		// Token: 0x02000009 RID: 9
		public readonly struct GraphNode
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600001B RID: 27 RVA: 0x000025E2 File Offset: 0x000007E2
			public TransputSpec Transput { get; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600001C RID: 28 RVA: 0x000025EA File Offset: 0x000007EA
			public BlockObject Parent { get; }

			// Token: 0x0600001D RID: 29 RVA: 0x000025F2 File Offset: 0x000007F2
			public GraphNode(TransputSpec transput, BlockObject parent)
			{
				this.Transput = transput;
				this.Parent = parent;
			}
		}
	}
}
