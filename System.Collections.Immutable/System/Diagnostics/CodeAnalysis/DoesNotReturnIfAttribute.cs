using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002422 File Offset: 0x00000622
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002431 File Offset: 0x00000631
		public bool ParameterValue { get; }
	}
}
