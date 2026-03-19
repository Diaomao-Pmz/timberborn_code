using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Parameter)]
	internal sealed class AssertionConditionAttribute : Attribute
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000023C7 File Offset: 0x000005C7
		public AssertionConditionType ConditionType { get; }

		// Token: 0x06000051 RID: 81 RVA: 0x000023CF File Offset: 0x000005CF
		public AssertionConditionAttribute(AssertionConditionType conditionType)
		{
			this.ConditionType = conditionType;
		}
	}
}
