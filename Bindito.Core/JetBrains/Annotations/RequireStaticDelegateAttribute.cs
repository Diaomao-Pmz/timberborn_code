using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200001C RID: 28
	[AttributeUsage(AttributeTargets.Parameter)]
	internal sealed class RequireStaticDelegateAttribute : Attribute
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002314 File Offset: 0x00000514
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000231C File Offset: 0x0000051C
		public bool IsError { get; set; }
	}
}
