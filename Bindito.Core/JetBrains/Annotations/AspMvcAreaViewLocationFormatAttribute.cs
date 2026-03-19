using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000039 RID: 57
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000025A4 File Offset: 0x000007A4
		[NotNull]
		public string Format { get; }

		// Token: 0x06000081 RID: 129 RVA: 0x000025AC File Offset: 0x000007AC
		public AspMvcAreaViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}
	}
}
