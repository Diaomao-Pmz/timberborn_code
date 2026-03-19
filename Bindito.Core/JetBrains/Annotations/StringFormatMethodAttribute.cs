using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
	internal sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		[NotNull]
		public string FormatParameterName { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002078 File Offset: 0x00000278
		public StringFormatMethodAttribute([NotNull] string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}
	}
}
