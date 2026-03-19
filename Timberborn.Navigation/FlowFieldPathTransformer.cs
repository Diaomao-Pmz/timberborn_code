using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x02000024 RID: 36
	public class FlowFieldPathTransformer
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x0000480E File Offset: 0x00002A0E
		public FlowFieldPathTransformer(IEnumerable<IPathTransformer> pathTransformers)
		{
			this._pathTransformers = pathTransformers.ToImmutableArray<IPathTransformer>();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004824 File Offset: 0x00002A24
		public void TransformPath(List<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners)
		{
			pathCorners.Clear();
			if (flowFieldPath.Count == 1)
			{
				FlowFieldPathNode flowFieldPathNode = flowFieldPath[0];
				pathCorners.Add(new PathCorner(flowFieldPathNode.Position, FlowFieldPathTransformer.DefaultSpeed, flowFieldPathNode.GroupId));
				return;
			}
			ReadOnlyList<FlowFieldPathNode> flowFieldPath2 = flowFieldPath.AsReadOnlyList<FlowFieldPathNode>();
			int i = 0;
			while (i < flowFieldPath2.Count)
			{
				if (!this.TransformNode(ref i, flowFieldPath2, pathCorners))
				{
					this.AddPathCorner(flowFieldPath2[i], pathCorners);
					i++;
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000489B File Offset: 0x00002A9B
		public void TransformReversedPath(List<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners)
		{
			FlowFieldPathTransformer.ReversePath(flowFieldPath);
			this.TransformPath(flowFieldPath, pathCorners);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000048AC File Offset: 0x00002AAC
		public bool TransformNode(ref int index, ReadOnlyList<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners)
		{
			ImmutableArray<IPathTransformer>.Enumerator enumerator = this._pathTransformers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Transform(ref index, flowFieldPath, pathCorners))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000048E4 File Offset: 0x00002AE4
		public void AddPathCorner(FlowFieldPathNode flowFieldPathNode, List<PathCorner> pathCorners)
		{
			pathCorners.Add(new PathCorner(flowFieldPathNode.Position, flowFieldPathNode.NormalizedSpeed, flowFieldPathNode.GroupId));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004908 File Offset: 0x00002B08
		public static void ReversePath(List<FlowFieldPathNode> rawPath)
		{
			rawPath.Reverse();
			for (int i = 0; i < rawPath.Count - 1; i++)
			{
				FlowFieldPathNode flowFieldPathNode = rawPath[i + 1];
				rawPath[i] = new FlowFieldPathNode(rawPath[i].Position, flowFieldPathNode.Cost, flowFieldPathNode.DistanceToNext, flowFieldPathNode.GroupId);
			}
		}

		// Token: 0x04000070 RID: 112
		public static readonly float DefaultSpeed = 1f;

		// Token: 0x04000071 RID: 113
		public readonly ImmutableArray<IPathTransformer> _pathTransformers;
	}
}
