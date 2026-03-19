using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000037 RID: 55
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002576 File Offset: 0x00000776
		[NotNull]
		public string Format { get; }

		// Token: 0x0600007D RID: 125 RVA: 0x0000257E File Offset: 0x0000077E
		public AspMvcAreaPartialViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}
	}
}
