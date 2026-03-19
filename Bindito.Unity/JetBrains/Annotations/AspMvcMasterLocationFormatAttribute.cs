using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcMasterLocationFormatAttribute : Attribute
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000025BB File Offset: 0x000007BB
		[NotNull]
		public string Format { get; }

		// Token: 0x06000083 RID: 131 RVA: 0x000025C3 File Offset: 0x000007C3
		public AspMvcMasterLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}
	}
}
