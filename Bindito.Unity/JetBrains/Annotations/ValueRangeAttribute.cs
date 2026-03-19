using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true)]
	internal sealed class ValueRangeAttribute : Attribute
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020A6 File Offset: 0x000002A6
		public object From { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020AE File Offset: 0x000002AE
		public object To { get; }

		// Token: 0x0600000C RID: 12 RVA: 0x000020B6 File Offset: 0x000002B6
		public ValueRangeAttribute(long from, long to)
		{
			this.From = from;
			this.To = to;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020D6 File Offset: 0x000002D6
		public ValueRangeAttribute(ulong from, ulong to)
		{
			this.From = from;
			this.To = to;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020F8 File Offset: 0x000002F8
		public ValueRangeAttribute(long value)
		{
			this.From = (this.To = value);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002120 File Offset: 0x00000320
		public ValueRangeAttribute(ulong value)
		{
			this.From = (this.To = value);
		}
	}
}
