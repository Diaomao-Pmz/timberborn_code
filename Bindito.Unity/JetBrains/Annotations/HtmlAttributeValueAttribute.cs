using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200005A RID: 90
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class HtmlAttributeValueAttribute : Attribute
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002807 File Offset: 0x00000A07
		[NotNull]
		public string Name { get; }

		// Token: 0x060000C2 RID: 194 RVA: 0x0000280F File Offset: 0x00000A0F
		public HtmlAttributeValueAttribute([NotNull] string name)
		{
			this.Name = name;
		}
	}
}
