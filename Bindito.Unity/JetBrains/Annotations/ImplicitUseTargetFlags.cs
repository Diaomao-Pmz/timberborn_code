using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000015 RID: 21
	[Flags]
	internal enum ImplicitUseTargetFlags
	{
		// Token: 0x04000016 RID: 22
		Default = 1,
		// Token: 0x04000017 RID: 23
		Itself = 1,
		// Token: 0x04000018 RID: 24
		Members = 2,
		// Token: 0x04000019 RID: 25
		WithInheritors = 4,
		// Token: 0x0400001A RID: 26
		WithMembers = 3
	}
}
