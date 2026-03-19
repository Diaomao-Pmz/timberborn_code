using System;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000011 RID: 17
	public readonly struct StatusData
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000277A File Offset: 0x0000097A
		public int Count { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002782 File Offset: 0x00000982
		public float Value { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000278A File Offset: 0x0000098A
		public StatusWarningType StatusWarningType { get; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002792 File Offset: 0x00000992
		public StatusData(int count, float value, StatusWarningType statusWarningType)
		{
			this.Count = count;
			this.Value = value;
			this.StatusWarningType = statusWarningType;
		}
	}
}
