using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.ReturnValue)]
	internal sealed class CollectionAccessAttribute : Attribute
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000023A8 File Offset: 0x000005A8
		public CollectionAccessType CollectionAccessType { get; }

		// Token: 0x0600004E RID: 78 RVA: 0x000023B0 File Offset: 0x000005B0
		public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
		{
			this.CollectionAccessType = collectionAccessType;
		}
	}
}
