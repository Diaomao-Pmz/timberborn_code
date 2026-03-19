using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class SoilContaminationMapSpec : ComponentSpec, IEquatable<SoilContaminationMapSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000029FE File Offset: 0x00000BFE
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SoilContaminationMapSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002A0A File Offset: 0x00000C0A
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002A12 File Offset: 0x00000C12
		[Serialize]
		public float MaxMapContamination { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002A1B File Offset: 0x00000C1B
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002A23 File Offset: 0x00000C23
		[Serialize]
		public float ContaminationThreshold { get; set; }

		// Token: 0x06000027 RID: 39 RVA: 0x00002A2C File Offset: 0x00000C2C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SoilContaminationMapSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A78 File Offset: 0x00000C78
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxMapContamination = ");
			builder.Append(this.MaxMapContamination.ToString());
			builder.Append(", ContaminationThreshold = ");
			builder.Append(this.ContaminationThreshold.ToString());
			return true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002AE9 File Offset: 0x00000CE9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SoilContaminationMapSpec left, SoilContaminationMapSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AF5 File Offset: 0x00000CF5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SoilContaminationMapSpec left, SoilContaminationMapSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B09 File Offset: 0x00000D09
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxMapContamination>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ContaminationThreshold>k__BackingField);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B3F File Offset: 0x00000D3F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SoilContaminationMapSpec);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B4D File Offset: 0x00000D4D
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B58 File Offset: 0x00000D58
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SoilContaminationMapSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MaxMapContamination>k__BackingField, other.<MaxMapContamination>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ContaminationThreshold>k__BackingField, other.<ContaminationThreshold>k__BackingField));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002BAC File Offset: 0x00000DAC
		[CompilerGenerated]
		protected SoilContaminationMapSpec(SoilContaminationMapSpec original) : base(original)
		{
			this.MaxMapContamination = original.<MaxMapContamination>k__BackingField;
			this.ContaminationThreshold = original.<ContaminationThreshold>k__BackingField;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BCD File Offset: 0x00000DCD
		public SoilContaminationMapSpec()
		{
		}
	}
}
