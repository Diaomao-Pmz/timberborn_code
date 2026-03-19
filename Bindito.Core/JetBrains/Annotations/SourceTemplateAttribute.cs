using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class SourceTemplateAttribute : Attribute
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002354 File Offset: 0x00000554
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000235C File Offset: 0x0000055C
		public SourceTemplateTargetExpression Target { get; set; }
	}
}
