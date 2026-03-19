using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000034 RID: 52
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal sealed class AspRequiredAttributeAttribute : Attribute
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002531 File Offset: 0x00000731
		[NotNull]
		public string Attribute { get; }

		// Token: 0x06000077 RID: 119 RVA: 0x00002539 File Offset: 0x00000739
		public AspRequiredAttributeAttribute([NotNull] string attribute)
		{
			this.Attribute = attribute;
		}
	}
}
