using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x02000043 RID: 67
	public interface IPathTransformer
	{
		// Token: 0x0600015D RID: 349
		bool Transform(ref int index, ReadOnlyList<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners);
	}
}
