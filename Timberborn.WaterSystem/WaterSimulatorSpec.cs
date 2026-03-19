using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterSimulatorSpec : ComponentSpec, IEquatable<WaterSimulatorSpec>
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000074A6 File Offset: 0x000056A6
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterSimulatorSpec);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000074B2 File Offset: 0x000056B2
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000074BA File Offset: 0x000056BA
		[Serialize]
		public float WaterFlowFactor { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000074C3 File Offset: 0x000056C3
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000074CB File Offset: 0x000056CB
		[Serialize]
		public float WaterSpillThreshold { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000074D4 File Offset: 0x000056D4
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000074DC File Offset: 0x000056DC
		[Serialize]
		public float NormalEvaporationSpeed { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000074E5 File Offset: 0x000056E5
		// (set) Token: 0x0600015F RID: 351 RVA: 0x000074ED File Offset: 0x000056ED
		[Serialize]
		public float FastEvaporationDepthThreshold { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000074F6 File Offset: 0x000056F6
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000074FE File Offset: 0x000056FE
		[Serialize]
		public float FastEvaporationSpeed { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007507 File Offset: 0x00005707
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000750F File Offset: 0x0000570F
		[Serialize]
		public float OutflowBalancingScaler { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007518 File Offset: 0x00005718
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00007520 File Offset: 0x00005720
		[Serialize]
		public float SoftDamOffset { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00007529 File Offset: 0x00005729
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007531 File Offset: 0x00005731
		[Serialize]
		public float HardDamOffset { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000753A File Offset: 0x0000573A
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00007542 File Offset: 0x00005742
		[Serialize]
		public float MaxHardDamDecrease { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000754B File Offset: 0x0000574B
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00007553 File Offset: 0x00005753
		[Serialize]
		public float FlowChangeLimit { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000755C File Offset: 0x0000575C
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00007564 File Offset: 0x00005764
		[Serialize]
		public float OverflowPressureFactor { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000756D File Offset: 0x0000576D
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00007575 File Offset: 0x00005775
		[Serialize]
		public float DiffusionOutflowLimit { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000757E File Offset: 0x0000577E
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00007586 File Offset: 0x00005786
		[Serialize]
		public float DiffusionDepthLimit { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000758F File Offset: 0x0000578F
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00007597 File Offset: 0x00005797
		[Serialize]
		public float DiffusionRate { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000075A0 File Offset: 0x000057A0
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000075A8 File Offset: 0x000057A8
		[Serialize]
		public float MaxWaterContamination { get; set; }

		// Token: 0x06000176 RID: 374 RVA: 0x000075B4 File Offset: 0x000057B4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterSimulatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007600 File Offset: 0x00005800
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WaterFlowFactor = ");
			builder.Append(this.WaterFlowFactor.ToString());
			builder.Append(", WaterSpillThreshold = ");
			builder.Append(this.WaterSpillThreshold.ToString());
			builder.Append(", NormalEvaporationSpeed = ");
			builder.Append(this.NormalEvaporationSpeed.ToString());
			builder.Append(", FastEvaporationDepthThreshold = ");
			builder.Append(this.FastEvaporationDepthThreshold.ToString());
			builder.Append(", FastEvaporationSpeed = ");
			builder.Append(this.FastEvaporationSpeed.ToString());
			builder.Append(", OutflowBalancingScaler = ");
			builder.Append(this.OutflowBalancingScaler.ToString());
			builder.Append(", SoftDamOffset = ");
			builder.Append(this.SoftDamOffset.ToString());
			builder.Append(", HardDamOffset = ");
			builder.Append(this.HardDamOffset.ToString());
			builder.Append(", MaxHardDamDecrease = ");
			builder.Append(this.MaxHardDamDecrease.ToString());
			builder.Append(", FlowChangeLimit = ");
			builder.Append(this.FlowChangeLimit.ToString());
			builder.Append(", OverflowPressureFactor = ");
			builder.Append(this.OverflowPressureFactor.ToString());
			builder.Append(", DiffusionOutflowLimit = ");
			builder.Append(this.DiffusionOutflowLimit.ToString());
			builder.Append(", DiffusionDepthLimit = ");
			builder.Append(this.DiffusionDepthLimit.ToString());
			builder.Append(", DiffusionRate = ");
			builder.Append(this.DiffusionRate.ToString());
			builder.Append(", MaxWaterContamination = ");
			builder.Append(this.MaxWaterContamination.ToString());
			return true;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000786C File Offset: 0x00005A6C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterSimulatorSpec left, WaterSimulatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007878 File Offset: 0x00005A78
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterSimulatorSpec left, WaterSimulatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000788C File Offset: 0x00005A8C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<WaterFlowFactor>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<WaterSpillThreshold>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<NormalEvaporationSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<FastEvaporationDepthThreshold>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<FastEvaporationSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<OutflowBalancingScaler>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<SoftDamOffset>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<HardDamOffset>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxHardDamDecrease>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<FlowChangeLimit>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<OverflowPressureFactor>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DiffusionOutflowLimit>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DiffusionDepthLimit>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DiffusionRate>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxWaterContamination>k__BackingField);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000079F8 File Offset: 0x00005BF8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterSimulatorSpec);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007A06 File Offset: 0x00005C06
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007A10 File Offset: 0x00005C10
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterSimulatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<WaterFlowFactor>k__BackingField, other.<WaterFlowFactor>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<WaterSpillThreshold>k__BackingField, other.<WaterSpillThreshold>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<NormalEvaporationSpeed>k__BackingField, other.<NormalEvaporationSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<FastEvaporationDepthThreshold>k__BackingField, other.<FastEvaporationDepthThreshold>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<FastEvaporationSpeed>k__BackingField, other.<FastEvaporationSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<OutflowBalancingScaler>k__BackingField, other.<OutflowBalancingScaler>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<SoftDamOffset>k__BackingField, other.<SoftDamOffset>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<HardDamOffset>k__BackingField, other.<HardDamOffset>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxHardDamDecrease>k__BackingField, other.<MaxHardDamDecrease>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<FlowChangeLimit>k__BackingField, other.<FlowChangeLimit>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<OverflowPressureFactor>k__BackingField, other.<OverflowPressureFactor>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DiffusionOutflowLimit>k__BackingField, other.<DiffusionOutflowLimit>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DiffusionDepthLimit>k__BackingField, other.<DiffusionDepthLimit>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DiffusionRate>k__BackingField, other.<DiffusionRate>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxWaterContamination>k__BackingField, other.<MaxWaterContamination>k__BackingField));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007BC0 File Offset: 0x00005DC0
		[CompilerGenerated]
		protected WaterSimulatorSpec(WaterSimulatorSpec original) : base(original)
		{
			this.WaterFlowFactor = original.<WaterFlowFactor>k__BackingField;
			this.WaterSpillThreshold = original.<WaterSpillThreshold>k__BackingField;
			this.NormalEvaporationSpeed = original.<NormalEvaporationSpeed>k__BackingField;
			this.FastEvaporationDepthThreshold = original.<FastEvaporationDepthThreshold>k__BackingField;
			this.FastEvaporationSpeed = original.<FastEvaporationSpeed>k__BackingField;
			this.OutflowBalancingScaler = original.<OutflowBalancingScaler>k__BackingField;
			this.SoftDamOffset = original.<SoftDamOffset>k__BackingField;
			this.HardDamOffset = original.<HardDamOffset>k__BackingField;
			this.MaxHardDamDecrease = original.<MaxHardDamDecrease>k__BackingField;
			this.FlowChangeLimit = original.<FlowChangeLimit>k__BackingField;
			this.OverflowPressureFactor = original.<OverflowPressureFactor>k__BackingField;
			this.DiffusionOutflowLimit = original.<DiffusionOutflowLimit>k__BackingField;
			this.DiffusionDepthLimit = original.<DiffusionDepthLimit>k__BackingField;
			this.DiffusionRate = original.<DiffusionRate>k__BackingField;
			this.MaxWaterContamination = original.<MaxWaterContamination>k__BackingField;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007C88 File Offset: 0x00005E88
		public WaterSimulatorSpec()
		{
		}
	}
}
