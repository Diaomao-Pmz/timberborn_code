using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000023 RID: 35
	[Flags]
	internal enum CollectionAccessType
	{
		// Token: 0x0400002A RID: 42
		None = 0,
		// Token: 0x0400002B RID: 43
		Read = 1,
		// Token: 0x0400002C RID: 44
		ModifyExistingContent = 2,
		// Token: 0x0400002D RID: 45
		UpdatedContent = 6
	}
}
