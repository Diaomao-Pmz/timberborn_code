using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200005C RID: 92
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class RazorImportNamespaceAttribute : Attribute
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002826 File Offset: 0x00000A26
		[NotNull]
		public string Name { get; }

		// Token: 0x060000C5 RID: 197 RVA: 0x0000282E File Offset: 0x00000A2E
		public RazorImportNamespaceAttribute([NotNull] string name)
		{
			this.Name = name;
		}
	}
}
