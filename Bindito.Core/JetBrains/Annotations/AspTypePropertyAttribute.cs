using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000035 RID: 53
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class AspTypePropertyAttribute : Attribute
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002548 File Offset: 0x00000748
		public bool CreateConstructorReferences { get; }

		// Token: 0x06000079 RID: 121 RVA: 0x00002550 File Offset: 0x00000750
		public AspTypePropertyAttribute(bool createConstructorReferences)
		{
			this.CreateConstructorReferences = createConstructorReferences;
		}
	}
}
