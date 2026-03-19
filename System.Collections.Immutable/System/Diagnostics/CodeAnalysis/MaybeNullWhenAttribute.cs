using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000023D5 File Offset: 0x000005D5
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023E4 File Offset: 0x000005E4
		public bool ReturnValue { get; }
	}
}
