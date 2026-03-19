using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021EE File Offset: 0x000003EE
		public ImplicitUseKindFlags UseKindFlags { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000021F6 File Offset: 0x000003F6
		public ImplicitUseTargetFlags TargetFlags { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021FE File Offset: 0x000003FE
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002206 File Offset: 0x00000406
		public string Reason { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x0000220F File Offset: 0x0000040F
		public UsedImplicitlyAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002219 File Offset: 0x00000419
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002223 File Offset: 0x00000423
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000222D File Offset: 0x0000042D
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}
	}
}
