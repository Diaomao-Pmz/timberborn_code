using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000055 RID: 85
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class AspMinimalApiDeclarationAttribute : Attribute
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002773 File Offset: 0x00000973
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000277B File Offset: 0x0000097B
		public string HttpVerb { get; set; }
	}
}
