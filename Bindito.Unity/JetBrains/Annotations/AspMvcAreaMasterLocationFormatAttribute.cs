using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000036 RID: 54
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000255F File Offset: 0x0000075F
		[NotNull]
		public string Format { get; }

		// Token: 0x0600007B RID: 123 RVA: 0x00002567 File Offset: 0x00000767
		public AspMvcAreaMasterLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}
	}
}
