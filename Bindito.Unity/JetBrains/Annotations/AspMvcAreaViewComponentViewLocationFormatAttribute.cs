using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000038 RID: 56
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcAreaViewComponentViewLocationFormatAttribute : Attribute
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000258D File Offset: 0x0000078D
		[NotNull]
		public string Format { get; }

		// Token: 0x0600007F RID: 127 RVA: 0x00002595 File Offset: 0x00000795
		public AspMvcAreaViewComponentViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}
	}
}
