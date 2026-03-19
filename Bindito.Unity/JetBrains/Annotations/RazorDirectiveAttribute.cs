using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200005E RID: 94
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class RazorDirectiveAttribute : Attribute
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002863 File Offset: 0x00000A63
		[NotNull]
		public string Directive { get; }

		// Token: 0x060000CA RID: 202 RVA: 0x0000286B File Offset: 0x00000A6B
		public RazorDirectiveAttribute([NotNull] string directive)
		{
			this.Directive = directive;
		}
	}
}
