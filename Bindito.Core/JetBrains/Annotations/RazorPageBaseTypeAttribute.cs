using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class RazorPageBaseTypeAttribute : Attribute
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000287A File Offset: 0x00000A7A
		[NotNull]
		public string BaseType { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00002882 File Offset: 0x00000A82
		[CanBeNull]
		public string PageName { get; }

		// Token: 0x060000CD RID: 205 RVA: 0x0000288A File Offset: 0x00000A8A
		public RazorPageBaseTypeAttribute([NotNull] string baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002899 File Offset: 0x00000A99
		public RazorPageBaseTypeAttribute([NotNull] string baseType, string pageName)
		{
			this.BaseType = baseType;
			this.PageName = pageName;
		}
	}
}
