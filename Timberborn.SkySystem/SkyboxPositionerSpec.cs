using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.SkySystem
{
	// Token: 0x02000010 RID: 16
	public class SkyboxPositionerSpec : ComponentSpec, IEquatable<SkyboxPositionerSpec>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002F19 File Offset: 0x00001119
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SkyboxPositionerSpec);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002F25 File Offset: 0x00001125
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002F2D File Offset: 0x0000112D
		[Serialize]
		public AssetRef<Material> Skybox { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002F36 File Offset: 0x00001136
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002F3E File Offset: 0x0000113E
		[Serialize]
		public float DayProgressSunrise { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002F47 File Offset: 0x00001147
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002F4F File Offset: 0x0000114F
		[Serialize]
		public float DayProgressDay { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002F58 File Offset: 0x00001158
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002F60 File Offset: 0x00001160
		[Serialize]
		public float DayProgressSunset { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002F69 File Offset: 0x00001169
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002F71 File Offset: 0x00001171
		[Serialize]
		public float DayProgressNight { get; set; }

		// Token: 0x06000077 RID: 119 RVA: 0x00002F7C File Offset: 0x0000117C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SkyboxPositionerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002FC8 File Offset: 0x000011C8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Skybox = ");
			builder.Append(this.Skybox);
			builder.Append(", DayProgressSunrise = ");
			builder.Append(this.DayProgressSunrise.ToString());
			builder.Append(", DayProgressDay = ");
			builder.Append(this.DayProgressDay.ToString());
			builder.Append(", DayProgressSunset = ");
			builder.Append(this.DayProgressSunset.ToString());
			builder.Append(", DayProgressNight = ");
			builder.Append(this.DayProgressNight.ToString());
			return true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000030A0 File Offset: 0x000012A0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SkyboxPositionerSpec left, SkyboxPositionerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030AC File Offset: 0x000012AC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SkyboxPositionerSpec left, SkyboxPositionerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000030C0 File Offset: 0x000012C0
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<Skybox>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DayProgressSunrise>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DayProgressDay>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DayProgressSunset>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DayProgressNight>k__BackingField);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003146 File Offset: 0x00001346
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SkyboxPositionerSpec);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000292D File Offset: 0x00000B2D
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003154 File Offset: 0x00001354
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SkyboxPositionerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<Skybox>k__BackingField, other.<Skybox>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DayProgressSunrise>k__BackingField, other.<DayProgressSunrise>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DayProgressDay>k__BackingField, other.<DayProgressDay>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DayProgressSunset>k__BackingField, other.<DayProgressSunset>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DayProgressNight>k__BackingField, other.<DayProgressNight>k__BackingField));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000031F4 File Offset: 0x000013F4
		[CompilerGenerated]
		protected SkyboxPositionerSpec([Nullable(1)] SkyboxPositionerSpec original) : base(original)
		{
			this.Skybox = original.<Skybox>k__BackingField;
			this.DayProgressSunrise = original.<DayProgressSunrise>k__BackingField;
			this.DayProgressDay = original.<DayProgressDay>k__BackingField;
			this.DayProgressSunset = original.<DayProgressSunset>k__BackingField;
			this.DayProgressNight = original.<DayProgressNight>k__BackingField;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000029AD File Offset: 0x00000BAD
		public SkyboxPositionerSpec()
		{
		}
	}
}
