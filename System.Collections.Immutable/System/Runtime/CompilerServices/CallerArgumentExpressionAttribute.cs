using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	internal sealed class CallerArgumentExpressionAttribute : Attribute
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00002D5B File Offset: 0x00000F5B
		public CallerArgumentExpressionAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002D6A File Offset: 0x00000F6A
		public string ParameterName { get; }
	}
}
