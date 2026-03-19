using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200003C RID: 60
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcViewComponentViewLocationFormatAttribute : Attribute
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000025E9 File Offset: 0x000007E9
		[NotNull]
		public string Format { get; }

		// Token: 0x06000087 RID: 135 RVA: 0x000025F1 File Offset: 0x000007F1
		public AspMvcViewComponentViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}
	}
}
