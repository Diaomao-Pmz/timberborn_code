using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021A7 File Offset: 0x000003A7
		public bool Required { get; }

		// Token: 0x0600001A RID: 26 RVA: 0x000021AF File Offset: 0x000003AF
		public LocalizationRequiredAttribute() : this(true)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021B8 File Offset: 0x000003B8
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}
	}
}
