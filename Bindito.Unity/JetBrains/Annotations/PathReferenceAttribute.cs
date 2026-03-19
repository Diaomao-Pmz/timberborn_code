using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class PathReferenceAttribute : Attribute
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002335 File Offset: 0x00000535
		[CanBeNull]
		public string BasePath { get; }

		// Token: 0x06000041 RID: 65 RVA: 0x0000233D File Offset: 0x0000053D
		public PathReferenceAttribute()
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002345 File Offset: 0x00000545
		public PathReferenceAttribute([NotNull] [PathReference] string basePath)
		{
			this.BasePath = basePath;
		}
	}
}
