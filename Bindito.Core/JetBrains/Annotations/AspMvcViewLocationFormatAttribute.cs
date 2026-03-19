using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200003D RID: 61
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcViewLocationFormatAttribute : Attribute
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002600 File Offset: 0x00000800
		[NotNull]
		public string Format { get; }

		// Token: 0x06000089 RID: 137 RVA: 0x00002608 File Offset: 0x00000808
		public AspMvcViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}
	}
}
