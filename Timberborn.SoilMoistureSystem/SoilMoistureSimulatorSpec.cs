using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public class SoilMoistureSimulatorSpec : ComponentSpec, IEquatable<SoilMoistureSimulatorSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003F73 File Offset: 0x00002173
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SoilMoistureSimulatorSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003F7F File Offset: 0x0000217F
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003F87 File Offset: 0x00002187
		[Serialize]
		public float MinimumWaterContamination { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003F90 File Offset: 0x00002190
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003F98 File Offset: 0x00002198
		[Serialize]
		public float MaximumWaterContamination { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003FA1 File Offset: 0x000021A1
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003FA9 File Offset: 0x000021A9
		[Serialize]
		public float MoistureDecayRate { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003FB2 File Offset: 0x000021B2
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003FBA File Offset: 0x000021BA
		[Serialize]
		public float MoistureSpreadingRate { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003FC3 File Offset: 0x000021C3
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003FCB File Offset: 0x000021CB
		[Serialize]
		public int VerticalSpreadCostMultiplier { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003FD4 File Offset: 0x000021D4
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003FDC File Offset: 0x000021DC
		[Serialize]
		public int MaxClusterSaturation { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003FE5 File Offset: 0x000021E5
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003FED File Offset: 0x000021ED
		[Serialize]
		public float QuadraticEvaporationCoefficient { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003FF6 File Offset: 0x000021F6
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003FFE File Offset: 0x000021FE
		[Serialize]
		public float LinearQuadraticCoefficient { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004007 File Offset: 0x00002207
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000400F File Offset: 0x0000220F
		[Serialize]
		public float ConstantQuadraticCoefficient { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004018 File Offset: 0x00002218
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004020 File Offset: 0x00002220
		[Serialize]
		public int MaxEvaporationSaturation { get; set; }

		// Token: 0x06000099 RID: 153 RVA: 0x0000402C File Offset: 0x0000222C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SoilMoistureSimulatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004078 File Offset: 0x00002278
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinimumWaterContamination = ");
			builder.Append(this.MinimumWaterContamination.ToString());
			builder.Append(", MaximumWaterContamination = ");
			builder.Append(this.MaximumWaterContamination.ToString());
			builder.Append(", MoistureDecayRate = ");
			builder.Append(this.MoistureDecayRate.ToString());
			builder.Append(", MoistureSpreadingRate = ");
			builder.Append(this.MoistureSpreadingRate.ToString());
			builder.Append(", VerticalSpreadCostMultiplier = ");
			builder.Append(this.VerticalSpreadCostMultiplier.ToString());
			builder.Append(", MaxClusterSaturation = ");
			builder.Append(this.MaxClusterSaturation.ToString());
			builder.Append(", QuadraticEvaporationCoefficient = ");
			builder.Append(this.QuadraticEvaporationCoefficient.ToString());
			builder.Append(", LinearQuadraticCoefficient = ");
			builder.Append(this.LinearQuadraticCoefficient.ToString());
			builder.Append(", ConstantQuadraticCoefficient = ");
			builder.Append(this.ConstantQuadraticCoefficient.ToString());
			builder.Append(", MaxEvaporationSaturation = ");
			builder.Append(this.MaxEvaporationSaturation.ToString());
			return true;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004221 File Offset: 0x00002421
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SoilMoistureSimulatorSpec left, SoilMoistureSimulatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000422D File Offset: 0x0000242D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SoilMoistureSimulatorSpec left, SoilMoistureSimulatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004244 File Offset: 0x00002444
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinimumWaterContamination>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaximumWaterContamination>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MoistureDecayRate>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MoistureSpreadingRate>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<VerticalSpreadCostMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxClusterSaturation>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<QuadraticEvaporationCoefficient>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<LinearQuadraticCoefficient>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ConstantQuadraticCoefficient>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxEvaporationSaturation>k__BackingField);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000433D File Offset: 0x0000253D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SoilMoistureSimulatorSpec);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000026C9 File Offset: 0x000008C9
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000434C File Offset: 0x0000254C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SoilMoistureSimulatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MinimumWaterContamination>k__BackingField, other.<MinimumWaterContamination>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaximumWaterContamination>k__BackingField, other.<MaximumWaterContamination>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MoistureDecayRate>k__BackingField, other.<MoistureDecayRate>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MoistureSpreadingRate>k__BackingField, other.<MoistureSpreadingRate>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<VerticalSpreadCostMultiplier>k__BackingField, other.<VerticalSpreadCostMultiplier>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxClusterSaturation>k__BackingField, other.<MaxClusterSaturation>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<QuadraticEvaporationCoefficient>k__BackingField, other.<QuadraticEvaporationCoefficient>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<LinearQuadraticCoefficient>k__BackingField, other.<LinearQuadraticCoefficient>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ConstantQuadraticCoefficient>k__BackingField, other.<ConstantQuadraticCoefficient>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxEvaporationSaturation>k__BackingField, other.<MaxEvaporationSaturation>k__BackingField));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004474 File Offset: 0x00002674
		[CompilerGenerated]
		protected SoilMoistureSimulatorSpec(SoilMoistureSimulatorSpec original) : base(original)
		{
			this.MinimumWaterContamination = original.<MinimumWaterContamination>k__BackingField;
			this.MaximumWaterContamination = original.<MaximumWaterContamination>k__BackingField;
			this.MoistureDecayRate = original.<MoistureDecayRate>k__BackingField;
			this.MoistureSpreadingRate = original.<MoistureSpreadingRate>k__BackingField;
			this.VerticalSpreadCostMultiplier = original.<VerticalSpreadCostMultiplier>k__BackingField;
			this.MaxClusterSaturation = original.<MaxClusterSaturation>k__BackingField;
			this.QuadraticEvaporationCoefficient = original.<QuadraticEvaporationCoefficient>k__BackingField;
			this.LinearQuadraticCoefficient = original.<LinearQuadraticCoefficient>k__BackingField;
			this.ConstantQuadraticCoefficient = original.<ConstantQuadraticCoefficient>k__BackingField;
			this.MaxEvaporationSaturation = original.<MaxEvaporationSaturation>k__BackingField;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002749 File Offset: 0x00000949
		public SoilMoistureSimulatorSpec()
		{
		}
	}
}
