using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Parameter)]
	internal sealed class MustDisposeResourceAttribute : Attribute
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000022E6 File Offset: 0x000004E6
		public bool Value { get; }

		// Token: 0x06000039 RID: 57 RVA: 0x000022EE File Offset: 0x000004EE
		public MustDisposeResourceAttribute()
		{
			this.Value = 1;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000022FD File Offset: 0x000004FD
		public MustDisposeResourceAttribute(bool value)
		{
			this.Value = value;
		}
	}
}
