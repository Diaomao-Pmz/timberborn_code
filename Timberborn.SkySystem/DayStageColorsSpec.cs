using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.SkySystem
{
	// Token: 0x02000008 RID: 8
	public class DayStageColorsSpec : IEquatable<DayStageColorsSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DayStageColorsSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public Color SunColor { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize]
		public float SunIntensity { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		[Serialize]
		public float SunXAngle { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002145 File Offset: 0x00000345
		[Serialize]
		public float ShadowStrength { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002156 File Offset: 0x00000356
		[Serialize]
		public Color AmbientSkyColor { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000215F File Offset: 0x0000035F
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002167 File Offset: 0x00000367
		[Serialize]
		public Color AmbientEquatorColor { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002178 File Offset: 0x00000378
		[Serialize]
		public Color AmbientGroundColor { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002181 File Offset: 0x00000381
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002189 File Offset: 0x00000389
		[Serialize]
		public float ReflectionsIntensity { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002192 File Offset: 0x00000392
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000219A File Offset: 0x0000039A
		[Serialize]
		public FogSettingsSpec TemperateWeatherFog { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021A3 File Offset: 0x000003A3
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021AB File Offset: 0x000003AB
		[Serialize]
		public ImmutableArray<HazardousWeatherFogSettingsSpec> HazardousWeatherFogs { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x000021B4 File Offset: 0x000003B4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DayStageColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002200 File Offset: 0x00000400
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("SunColor = ");
			builder.Append(this.SunColor.ToString());
			builder.Append(", SunIntensity = ");
			builder.Append(this.SunIntensity.ToString());
			builder.Append(", SunXAngle = ");
			builder.Append(this.SunXAngle.ToString());
			builder.Append(", ShadowStrength = ");
			builder.Append(this.ShadowStrength.ToString());
			builder.Append(", AmbientSkyColor = ");
			builder.Append(this.AmbientSkyColor.ToString());
			builder.Append(", AmbientEquatorColor = ");
			builder.Append(this.AmbientEquatorColor.ToString());
			builder.Append(", AmbientGroundColor = ");
			builder.Append(this.AmbientGroundColor.ToString());
			builder.Append(", ReflectionsIntensity = ");
			builder.Append(this.ReflectionsIntensity.ToString());
			builder.Append(", TemperateWeatherFog = ");
			builder.Append(this.TemperateWeatherFog);
			builder.Append(", HazardousWeatherFogs = ");
			builder.Append(this.HazardousWeatherFogs.ToString());
			return true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000238B File Offset: 0x0000058B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DayStageColorsSpec left, DayStageColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002397 File Offset: 0x00000597
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DayStageColorsSpec left, DayStageColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023AC File Offset: 0x000005AC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SunColor>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<SunIntensity>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<SunXAngle>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ShadowStrength>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<AmbientSkyColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<AmbientEquatorColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<AmbientGroundColor>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ReflectionsIntensity>k__BackingField)) * -1521134295 + EqualityComparer<FogSettingsSpec>.Default.GetHashCode(this.<TemperateWeatherFog>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<HazardousWeatherFogSettingsSpec>>.Default.GetHashCode(this.<HazardousWeatherFogs>k__BackingField);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024AF File Offset: 0x000006AF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DayStageColorsSpec);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024C0 File Offset: 0x000006C0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DayStageColorsSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Color>.Default.Equals(this.<SunColor>k__BackingField, other.<SunColor>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<SunIntensity>k__BackingField, other.<SunIntensity>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<SunXAngle>k__BackingField, other.<SunXAngle>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ShadowStrength>k__BackingField, other.<ShadowStrength>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<AmbientSkyColor>k__BackingField, other.<AmbientSkyColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<AmbientEquatorColor>k__BackingField, other.<AmbientEquatorColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<AmbientGroundColor>k__BackingField, other.<AmbientGroundColor>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ReflectionsIntensity>k__BackingField, other.<ReflectionsIntensity>k__BackingField) && EqualityComparer<FogSettingsSpec>.Default.Equals(this.<TemperateWeatherFog>k__BackingField, other.<TemperateWeatherFog>k__BackingField) && EqualityComparer<ImmutableArray<HazardousWeatherFogSettingsSpec>>.Default.Equals(this.<HazardousWeatherFogs>k__BackingField, other.<HazardousWeatherFogs>k__BackingField));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025F8 File Offset: 0x000007F8
		[CompilerGenerated]
		protected DayStageColorsSpec([Nullable(1)] DayStageColorsSpec original)
		{
			this.SunColor = original.<SunColor>k__BackingField;
			this.SunIntensity = original.<SunIntensity>k__BackingField;
			this.SunXAngle = original.<SunXAngle>k__BackingField;
			this.ShadowStrength = original.<ShadowStrength>k__BackingField;
			this.AmbientSkyColor = original.<AmbientSkyColor>k__BackingField;
			this.AmbientEquatorColor = original.<AmbientEquatorColor>k__BackingField;
			this.AmbientGroundColor = original.<AmbientGroundColor>k__BackingField;
			this.ReflectionsIntensity = original.<ReflectionsIntensity>k__BackingField;
			this.TemperateWeatherFog = original.<TemperateWeatherFog>k__BackingField;
			this.HazardousWeatherFogs = original.<HazardousWeatherFogs>k__BackingField;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000020F6 File Offset: 0x000002F6
		public DayStageColorsSpec()
		{
		}
	}
}
