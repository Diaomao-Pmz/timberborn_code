using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000023EC File Offset: 0x000005EC
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000023FB File Offset: 0x000005FB
		public bool ReturnValue { get; }
	}
}
