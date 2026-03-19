using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000040 RID: 64
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class AspMvcControllerAttribute : Attribute
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002655 File Offset: 0x00000855
		[CanBeNull]
		public string AnonymousProperty { get; }

		// Token: 0x06000091 RID: 145 RVA: 0x0000265D File Offset: 0x0000085D
		public AspMvcControllerAttribute()
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002665 File Offset: 0x00000865
		public AspMvcControllerAttribute([NotNull] string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}
	}
}
