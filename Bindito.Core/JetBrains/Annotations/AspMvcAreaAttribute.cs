using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class AspMvcAreaAttribute : Attribute
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002636 File Offset: 0x00000836
		[CanBeNull]
		public string AnonymousProperty { get; }

		// Token: 0x0600008E RID: 142 RVA: 0x0000263E File Offset: 0x0000083E
		public AspMvcAreaAttribute()
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002646 File Offset: 0x00000846
		public AspMvcAreaAttribute([NotNull] string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}
	}
}
