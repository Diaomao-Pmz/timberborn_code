using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200005D RID: 93
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class RazorInjectionAttribute : Attribute
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000283D File Offset: 0x00000A3D
		[NotNull]
		public string Type { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002845 File Offset: 0x00000A45
		[NotNull]
		public string FieldName { get; }

		// Token: 0x060000C8 RID: 200 RVA: 0x0000284D File Offset: 0x00000A4D
		public RazorInjectionAttribute([NotNull] string type, [NotNull] string fieldName)
		{
			this.Type = type;
			this.FieldName = fieldName;
		}
	}
}
