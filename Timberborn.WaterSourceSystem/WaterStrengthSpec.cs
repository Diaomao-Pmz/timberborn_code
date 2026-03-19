using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterStrengthSpec : ComponentSpec, IEquatable<WaterStrengthSpec>
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000040D9 File Offset: 0x000022D9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterStrengthSpec);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000040E5 File Offset: 0x000022E5
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000040ED File Offset: 0x000022ED
		[Serialize]
		public float MaxWaterSourceStrength { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000040F6 File Offset: 0x000022F6
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000040FE File Offset: 0x000022FE
		[Serialize]
		public float MaxWaterSourceChangePerSecond { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004107 File Offset: 0x00002307
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000410F File Offset: 0x0000230F
		[Serialize]
		public float MinWaterSourceChangeScaler { get; set; }

		// Token: 0x0600014D RID: 333 RVA: 0x00004118 File Offset: 0x00002318
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterStrengthSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004164 File Offset: 0x00002364
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxWaterSourceStrength = ");
			builder.Append(this.MaxWaterSourceStrength.ToString());
			builder.Append(", MaxWaterSourceChangePerSecond = ");
			builder.Append(this.MaxWaterSourceChangePerSecond.ToString());
			builder.Append(", MinWaterSourceChangeScaler = ");
			builder.Append(this.MinWaterSourceChangeScaler.ToString());
			return true;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000041FC File Offset: 0x000023FC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterStrengthSpec left, WaterStrengthSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004208 File Offset: 0x00002408
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterStrengthSpec left, WaterStrengthSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000421C File Offset: 0x0000241C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxWaterSourceStrength>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxWaterSourceChangePerSecond>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinWaterSourceChangeScaler>k__BackingField);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004274 File Offset: 0x00002474
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterStrengthSpec);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004284 File Offset: 0x00002484
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterStrengthSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MaxWaterSourceStrength>k__BackingField, other.<MaxWaterSourceStrength>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxWaterSourceChangePerSecond>k__BackingField, other.<MaxWaterSourceChangePerSecond>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinWaterSourceChangeScaler>k__BackingField, other.<MinWaterSourceChangeScaler>k__BackingField));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000042F0 File Offset: 0x000024F0
		[CompilerGenerated]
		protected WaterStrengthSpec(WaterStrengthSpec original) : base(original)
		{
			this.MaxWaterSourceStrength = original.<MaxWaterSourceStrength>k__BackingField;
			this.MaxWaterSourceChangePerSecond = original.<MaxWaterSourceChangePerSecond>k__BackingField;
			this.MinWaterSourceChangeScaler = original.<MinWaterSourceChangeScaler>k__BackingField;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00002320 File Offset: 0x00000520
		public WaterStrengthSpec()
		{
		}
	}
}
