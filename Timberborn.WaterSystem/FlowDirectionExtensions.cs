using System;
using Timberborn.Coordinates;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200000D RID: 13
	public static class FlowDirectionExtensions
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000024B0 File Offset: 0x000006B0
		public static FlowDirection ToFlowDirection(this Orientation orientation)
		{
			FlowDirection result;
			switch (orientation)
			{
			case Orientation.Cw0:
				result = FlowDirection.Top;
				break;
			case Orientation.Cw90:
				result = FlowDirection.Right;
				break;
			case Orientation.Cw180:
				result = FlowDirection.Bottom;
				break;
			case Orientation.Cw270:
				result = FlowDirection.Left;
				break;
			default:
				throw new ArgumentOutOfRangeException("orientation", orientation, null);
			}
			return result;
		}
	}
}
