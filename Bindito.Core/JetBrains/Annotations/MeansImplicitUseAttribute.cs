using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	internal sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002243 File Offset: 0x00000443
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000224B File Offset: 0x0000044B
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; }

		// Token: 0x0600002A RID: 42 RVA: 0x00002253 File Offset: 0x00000453
		public MeansImplicitUseAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000225D File Offset: 0x0000045D
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002267 File Offset: 0x00000467
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002271 File Offset: 0x00000471
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}
	}
}
