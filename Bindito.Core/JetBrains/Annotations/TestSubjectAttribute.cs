using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200006A RID: 106
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, AllowMultiple = true)]
	internal sealed class TestSubjectAttribute : Attribute
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000028FF File Offset: 0x00000AFF
		[NotNull]
		public Type Subject { get; }

		// Token: 0x060000DA RID: 218 RVA: 0x00002907 File Offset: 0x00000B07
		public TestSubjectAttribute([NotNull] Type subject)
		{
			this.Subject = subject;
		}
	}
}
