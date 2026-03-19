using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002468 File Offset: 0x00000668
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002487 File Offset: 0x00000687
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000249D File Offset: 0x0000069D
		public bool ReturnValue { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024A5 File Offset: 0x000006A5
		public string[] Members { get; }
	}
}
