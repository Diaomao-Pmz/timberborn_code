using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200000D RID: 13
	public class HazardousWeatherWaterSourceSpec : ComponentSpec, IEquatable<HazardousWeatherWaterSourceSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000026E4 File Offset: 0x000008E4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(HazardousWeatherWaterSourceSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000026F0 File Offset: 0x000008F0
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000026F8 File Offset: 0x000008F8
		[Serialize]
		public ImmutableArray<string> ActiveInHazardousWeather { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002704 File Offset: 0x00000904
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("HazardousWeatherWaterSourceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002750 File Offset: 0x00000950
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ActiveInHazardousWeather = ");
			builder.Append(this.ActiveInHazardousWeather.ToString());
			return true;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000279A File Offset: 0x0000099A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(HazardousWeatherWaterSourceSpec left, HazardousWeatherWaterSourceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027A6 File Offset: 0x000009A6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(HazardousWeatherWaterSourceSpec left, HazardousWeatherWaterSourceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027BA File Offset: 0x000009BA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<ActiveInHazardousWeather>k__BackingField);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027D9 File Offset: 0x000009D9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HazardousWeatherWaterSourceSpec);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027E7 File Offset: 0x000009E7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(HazardousWeatherWaterSourceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<ActiveInHazardousWeather>k__BackingField, other.<ActiveInHazardousWeather>k__BackingField));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002818 File Offset: 0x00000A18
		[CompilerGenerated]
		protected HazardousWeatherWaterSourceSpec([Nullable(1)] HazardousWeatherWaterSourceSpec original) : base(original)
		{
			this.ActiveInHazardousWeather = original.<ActiveInHazardousWeather>k__BackingField;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002320 File Offset: 0x00000520
		public HazardousWeatherWaterSourceSpec()
		{
		}
	}
}
