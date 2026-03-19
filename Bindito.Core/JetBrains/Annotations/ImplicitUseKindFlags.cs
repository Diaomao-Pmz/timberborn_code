using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000014 RID: 20
	[Flags]
	internal enum ImplicitUseKindFlags
	{
		// Token: 0x04000010 RID: 16
		Default = 7,
		// Token: 0x04000011 RID: 17
		Access = 1,
		// Token: 0x04000012 RID: 18
		Assign = 2,
		// Token: 0x04000013 RID: 19
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x04000014 RID: 20
		InstantiatedNoFixedConstructorSignature = 8
	}
}
