using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000059 RID: 89
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class HtmlElementAttributesAttribute : Attribute
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000027E8 File Offset: 0x000009E8
		[CanBeNull]
		public string Name { get; }

		// Token: 0x060000BF RID: 191 RVA: 0x000027F0 File Offset: 0x000009F0
		public HtmlElementAttributesAttribute()
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000027F8 File Offset: 0x000009F8
		public HtmlElementAttributesAttribute([NotNull] string name)
		{
			this.Name = name;
		}
	}
}
