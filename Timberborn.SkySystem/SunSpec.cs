using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.SkySystem
{
	// Token: 0x02000014 RID: 20
	public class SunSpec : ComponentSpec, IEquatable<SunSpec>
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003703 File Offset: 0x00001903
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SunSpec);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000370F File Offset: 0x0000190F
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003717 File Offset: 0x00001917
		[Serialize]
		public AssetRef<Light> SunPrefab { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003720 File Offset: 0x00001920
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003728 File Offset: 0x00001928
		[Serialize]
		public DayStageColorsSpec SunriseColors { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003731 File Offset: 0x00001931
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003739 File Offset: 0x00001939
		[Serialize]
		public DayStageColorsSpec DayColors { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003742 File Offset: 0x00001942
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000374A File Offset: 0x0000194A
		[Serialize]
		public DayStageColorsSpec SunsetColors { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003753 File Offset: 0x00001953
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000375B File Offset: 0x0000195B
		[Serialize]
		public DayStageColorsSpec NightColors { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003764 File Offset: 0x00001964
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000376C File Offset: 0x0000196C
		[Serialize]
		public float RotateWithCameraOffset { get; set; }

		// Token: 0x060000A3 RID: 163 RVA: 0x00003778 File Offset: 0x00001978
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SunSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000037C4 File Offset: 0x000019C4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SunPrefab = ");
			builder.Append(this.SunPrefab);
			builder.Append(", SunriseColors = ");
			builder.Append(this.SunriseColors);
			builder.Append(", DayColors = ");
			builder.Append(this.DayColors);
			builder.Append(", SunsetColors = ");
			builder.Append(this.SunsetColors);
			builder.Append(", NightColors = ");
			builder.Append(this.NightColors);
			builder.Append(", RotateWithCameraOffset = ");
			builder.Append(this.RotateWithCameraOffset.ToString());
			return true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000388B File Offset: 0x00001A8B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SunSpec left, SunSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003897 File Offset: 0x00001A97
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SunSpec left, SunSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000038AC File Offset: 0x00001AAC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<Light>>.Default.GetHashCode(this.<SunPrefab>k__BackingField)) * -1521134295 + EqualityComparer<DayStageColorsSpec>.Default.GetHashCode(this.<SunriseColors>k__BackingField)) * -1521134295 + EqualityComparer<DayStageColorsSpec>.Default.GetHashCode(this.<DayColors>k__BackingField)) * -1521134295 + EqualityComparer<DayStageColorsSpec>.Default.GetHashCode(this.<SunsetColors>k__BackingField)) * -1521134295 + EqualityComparer<DayStageColorsSpec>.Default.GetHashCode(this.<NightColors>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RotateWithCameraOffset>k__BackingField);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003949 File Offset: 0x00001B49
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SunSpec);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000292D File Offset: 0x00000B2D
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003958 File Offset: 0x00001B58
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SunSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<Light>>.Default.Equals(this.<SunPrefab>k__BackingField, other.<SunPrefab>k__BackingField) && EqualityComparer<DayStageColorsSpec>.Default.Equals(this.<SunriseColors>k__BackingField, other.<SunriseColors>k__BackingField) && EqualityComparer<DayStageColorsSpec>.Default.Equals(this.<DayColors>k__BackingField, other.<DayColors>k__BackingField) && EqualityComparer<DayStageColorsSpec>.Default.Equals(this.<SunsetColors>k__BackingField, other.<SunsetColors>k__BackingField) && EqualityComparer<DayStageColorsSpec>.Default.Equals(this.<NightColors>k__BackingField, other.<NightColors>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RotateWithCameraOffset>k__BackingField, other.<RotateWithCameraOffset>k__BackingField));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A14 File Offset: 0x00001C14
		[CompilerGenerated]
		protected SunSpec([Nullable(1)] SunSpec original) : base(original)
		{
			this.SunPrefab = original.<SunPrefab>k__BackingField;
			this.SunriseColors = original.<SunriseColors>k__BackingField;
			this.DayColors = original.<DayColors>k__BackingField;
			this.SunsetColors = original.<SunsetColors>k__BackingField;
			this.NightColors = original.<NightColors>k__BackingField;
			this.RotateWithCameraOffset = original.<RotateWithCameraOffset>k__BackingField;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000029AD File Offset: 0x00000BAD
		public SunSpec()
		{
		}
	}
}
