using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000058 RID: 88
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	internal sealed class AspMinimalApiImplicitEndpointDeclarationAttribute : Attribute
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000279C File Offset: 0x0000099C
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000027A4 File Offset: 0x000009A4
		public string HttpVerb { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000027AD File Offset: 0x000009AD
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000027B5 File Offset: 0x000009B5
		public string RouteTemplate { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000027BE File Offset: 0x000009BE
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000027C6 File Offset: 0x000009C6
		public Type BodyType { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000027CF File Offset: 0x000009CF
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000027D7 File Offset: 0x000009D7
		public string QueryParameters { get; set; }
	}
}
