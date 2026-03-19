using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class SoilContaminationSimulatorSpec : ComponentSpec, IEquatable<SoilContaminationSimulatorSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003B02 File Offset: 0x00001D02
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SoilContaminationSimulatorSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003B0E File Offset: 0x00001D0E
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003B16 File Offset: 0x00001D16
		[Serialize]
		public int MaxRangeFromSource { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003B1F File Offset: 0x00001D1F
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003B27 File Offset: 0x00001D27
		[Serialize]
		public float VerticalSpreadCostMultiplier { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003B30 File Offset: 0x00001D30
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003B38 File Offset: 0x00001D38
		[Serialize]
		public float ContaminationSpreadingRate { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003B41 File Offset: 0x00001D41
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003B49 File Offset: 0x00001D49
		[Serialize]
		public float ContaminationDecayRate { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003B52 File Offset: 0x00001D52
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003B5A File Offset: 0x00001D5A
		[Serialize]
		public float ContaminationPositiveEqualizationRate { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003B63 File Offset: 0x00001D63
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003B6B File Offset: 0x00001D6B
		[Serialize]
		public float ContaminationNegativeEqualizationRate { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003B74 File Offset: 0x00001D74
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003B7C File Offset: 0x00001D7C
		[Serialize]
		public float MinimumWaterContamination { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003B85 File Offset: 0x00001D85
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003B8D File Offset: 0x00001D8D
		[Serialize]
		public float ContaminationThreshold { get; set; }

		// Token: 0x0600006D RID: 109 RVA: 0x00003B98 File Offset: 0x00001D98
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SoilContaminationSimulatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003BE4 File Offset: 0x00001DE4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxRangeFromSource = ");
			builder.Append(this.MaxRangeFromSource.ToString());
			builder.Append(", VerticalSpreadCostMultiplier = ");
			builder.Append(this.VerticalSpreadCostMultiplier.ToString());
			builder.Append(", ContaminationSpreadingRate = ");
			builder.Append(this.ContaminationSpreadingRate.ToString());
			builder.Append(", ContaminationDecayRate = ");
			builder.Append(this.ContaminationDecayRate.ToString());
			builder.Append(", ContaminationPositiveEqualizationRate = ");
			builder.Append(this.ContaminationPositiveEqualizationRate.ToString());
			builder.Append(", ContaminationNegativeEqualizationRate = ");
			builder.Append(this.ContaminationNegativeEqualizationRate.ToString());
			builder.Append(", MinimumWaterContamination = ");
			builder.Append(this.MinimumWaterContamination.ToString());
			builder.Append(", ContaminationThreshold = ");
			builder.Append(this.ContaminationThreshold.ToString());
			return true;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003D3F File Offset: 0x00001F3F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SoilContaminationSimulatorSpec left, SoilContaminationSimulatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003D4B File Offset: 0x00001F4B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SoilContaminationSimulatorSpec left, SoilContaminationSimulatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003D60 File Offset: 0x00001F60
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxRangeFromSource>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<VerticalSpreadCostMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ContaminationSpreadingRate>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ContaminationDecayRate>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ContaminationPositiveEqualizationRate>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ContaminationNegativeEqualizationRate>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinimumWaterContamination>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ContaminationThreshold>k__BackingField);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003E2B File Offset: 0x0000202B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SoilContaminationSimulatorSpec);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002B4D File Offset: 0x00000D4D
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003E3C File Offset: 0x0000203C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SoilContaminationSimulatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxRangeFromSource>k__BackingField, other.<MaxRangeFromSource>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<VerticalSpreadCostMultiplier>k__BackingField, other.<VerticalSpreadCostMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ContaminationSpreadingRate>k__BackingField, other.<ContaminationSpreadingRate>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ContaminationDecayRate>k__BackingField, other.<ContaminationDecayRate>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ContaminationPositiveEqualizationRate>k__BackingField, other.<ContaminationPositiveEqualizationRate>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ContaminationNegativeEqualizationRate>k__BackingField, other.<ContaminationNegativeEqualizationRate>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinimumWaterContamination>k__BackingField, other.<MinimumWaterContamination>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ContaminationThreshold>k__BackingField, other.<ContaminationThreshold>k__BackingField));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003F2C File Offset: 0x0000212C
		[CompilerGenerated]
		protected SoilContaminationSimulatorSpec(SoilContaminationSimulatorSpec original) : base(original)
		{
			this.MaxRangeFromSource = original.<MaxRangeFromSource>k__BackingField;
			this.VerticalSpreadCostMultiplier = original.<VerticalSpreadCostMultiplier>k__BackingField;
			this.ContaminationSpreadingRate = original.<ContaminationSpreadingRate>k__BackingField;
			this.ContaminationDecayRate = original.<ContaminationDecayRate>k__BackingField;
			this.ContaminationPositiveEqualizationRate = original.<ContaminationPositiveEqualizationRate>k__BackingField;
			this.ContaminationNegativeEqualizationRate = original.<ContaminationNegativeEqualizationRate>k__BackingField;
			this.MinimumWaterContamination = original.<MinimumWaterContamination>k__BackingField;
			this.ContaminationThreshold = original.<ContaminationThreshold>k__BackingField;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002BCD File Offset: 0x00000DCD
		public SoilContaminationSimulatorSpec()
		{
		}
	}
}
