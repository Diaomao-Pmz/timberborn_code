using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002439 File Offset: 0x00000639
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002451 File Offset: 0x00000651
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002460 File Offset: 0x00000660
		public string[] Members { get; }
	}
}
