using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200003E RID: 62
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class AspMvcActionAttribute : Attribute
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002617 File Offset: 0x00000817
		[CanBeNull]
		public string AnonymousProperty { get; }

		// Token: 0x0600008B RID: 139 RVA: 0x0000261F File Offset: 0x0000081F
		public AspMvcActionAttribute()
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002627 File Offset: 0x00000827
		public AspMvcActionAttribute([NotNull] string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}
	}
}
