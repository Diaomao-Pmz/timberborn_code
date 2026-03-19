using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000016 RID: 22
	[MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	internal sealed class PublicAPIAttribute : Attribute
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002287 File Offset: 0x00000487
		[CanBeNull]
		public string Comment { get; }

		// Token: 0x0600002F RID: 47 RVA: 0x0000228F File Offset: 0x0000048F
		public PublicAPIAttribute()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002297 File Offset: 0x00000497
		public PublicAPIAttribute([NotNull] string comment)
		{
			this.Comment = comment;
		}
	}
}
