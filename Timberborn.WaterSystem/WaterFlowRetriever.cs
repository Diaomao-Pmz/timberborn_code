using System;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200002D RID: 45
	public class WaterFlowRetriever
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00004944 File Offset: 0x00002B44
		public float GetFlow(int target, in ColumnOutflows outflows)
		{
			if (outflows.BottomFlow.Index3D == target)
			{
				return outflows.BottomFlow.Flow;
			}
			if (outflows.LeftFlow.Index3D == target)
			{
				return outflows.LeftFlow.Flow;
			}
			if (outflows.TopFlow.Index3D == target)
			{
				return outflows.TopFlow.Flow;
			}
			if (outflows.RightFlow.Index3D == target)
			{
				return outflows.RightFlow.Flow;
			}
			if (outflows.Outflows != null)
			{
				for (int i = 0; i < outflows.Outflows.Count; i++)
				{
					TargetedFlow targetedFlow = outflows.Outflows[i];
					if (targetedFlow.Index3D == target)
					{
						return targetedFlow.Flow;
					}
				}
			}
			return 0f;
		}
	}
}
