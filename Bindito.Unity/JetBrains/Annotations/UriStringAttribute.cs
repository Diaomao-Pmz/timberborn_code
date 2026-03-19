using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200004E RID: 78
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class UriStringAttribute : Attribute
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000026FC File Offset: 0x000008FC
		[CanBeNull]
		public string HttpVerb { get; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00002704 File Offset: 0x00000904
		public UriStringAttribute()
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000270C File Offset: 0x0000090C
		public UriStringAttribute(string httpVerb)
		{
			this.HttpVerb = httpVerb;
		}
	}
}
