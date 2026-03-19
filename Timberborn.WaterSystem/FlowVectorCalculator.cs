using System;
using System.Collections.Generic;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000011 RID: 17
	public class FlowVectorCalculator : ILoadableSingleton
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002ADC File Offset: 0x00000CDC
		public FlowVectorCalculator(MapIndexService mapIndexService)
		{
			this._mapIndexService = mapIndexService;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002AEB File Offset: 0x00000CEB
		public void Load()
		{
			this._verticalStride = this._mapIndexService.VerticalStride;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B00 File Offset: 0x00000D00
		public Vector2 GetFlowVectorAtTop(in ColumnOutflows outflows)
		{
			if (outflows.Outflows == null)
			{
				return new Vector2(outflows.RightFlow.Flow - outflows.LeftFlow.Flow, outflows.TopFlow.Flow - outflows.BottomFlow.Flow);
			}
			float topFlow = FlowVectorCalculator.GetTopFlow(outflows.BottomFlow, this._verticalStride, outflows.Outflows);
			float topFlow2 = FlowVectorCalculator.GetTopFlow(outflows.LeftFlow, this._verticalStride, outflows.Outflows);
			float topFlow3 = FlowVectorCalculator.GetTopFlow(outflows.TopFlow, this._verticalStride, outflows.Outflows);
			return new Vector2(FlowVectorCalculator.GetTopFlow(outflows.RightFlow, this._verticalStride, outflows.Outflows) - topFlow2, topFlow3 - topFlow);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public static float GetTopFlow(in TargetedFlow inputFlow, int verticalStride, IList<TargetedFlow> outflows)
		{
			int index3D = inputFlow.Index3D;
			if (index3D == -1)
			{
				return 0f;
			}
			int num = index3D % verticalStride;
			int num2 = index3D;
			float flow = inputFlow.Flow;
			for (int i = 0; i < outflows.Count; i++)
			{
				TargetedFlow targetedFlow = outflows[i];
				int index3D2 = targetedFlow.Index3D;
				if (index3D2 % verticalStride == num && index3D2 > num2)
				{
					num2 = index3D2;
					flow = targetedFlow.Flow;
				}
			}
			return flow;
		}

		// Token: 0x0400002F RID: 47
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000030 RID: 48
		public int _verticalStride;
	}
}
