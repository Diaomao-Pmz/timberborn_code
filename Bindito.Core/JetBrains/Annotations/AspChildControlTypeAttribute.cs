using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal sealed class AspChildControlTypeAttribute : Attribute
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000024F3 File Offset: 0x000006F3
		[NotNull]
		public string TagName { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000024FB File Offset: 0x000006FB
		[NotNull]
		public Type ControlType { get; }

		// Token: 0x06000072 RID: 114 RVA: 0x00002503 File Offset: 0x00000703
		public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
		{
			this.TagName = tagName;
			this.ControlType = controlType;
		}
	}
}
