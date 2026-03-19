using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200003B RID: 59
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcPartialViewLocationFormatAttribute : Attribute
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000025D2 File Offset: 0x000007D2
		[NotNull]
		public string Format { get; }

		// Token: 0x06000085 RID: 133 RVA: 0x000025DA File Offset: 0x000007DA
		public AspMvcPartialViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}
	}
}
