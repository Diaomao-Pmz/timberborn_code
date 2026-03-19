using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.Parameter)]
	internal sealed class InstantHandleAttribute : Attribute
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000022A6 File Offset: 0x000004A6
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000022AE File Offset: 0x000004AE
		public bool RequireAwait { get; set; }
	}
}
