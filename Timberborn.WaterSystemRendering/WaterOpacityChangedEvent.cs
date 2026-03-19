using System;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000016 RID: 22
	public class WaterOpacityChangedEvent
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004CA1 File Offset: 0x00002EA1
		public bool IsWaterTransparent { get; }

		// Token: 0x0600008E RID: 142 RVA: 0x00004CA9 File Offset: 0x00002EA9
		public WaterOpacityChangedEvent(bool isWaterTransparent)
		{
			this.IsWaterTransparent = isWaterTransparent;
		}
	}
}
