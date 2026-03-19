using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000054 RID: 84
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class AspAttributeRoutingAttribute : Attribute
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000275A File Offset: 0x0000095A
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00002762 File Offset: 0x00000962
		public string HttpVerb { get; set; }
	}
}
