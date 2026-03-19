using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GameSound
{
	// Token: 0x02000007 RID: 7
	public class AmbientSpec : ComponentSpec, IEquatable<AmbientSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AmbientSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public string DayAmbient { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize]
		public string NightAmbient { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		[Serialize]
		public string WaterAmbient { get; set; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AmbientSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000218C File Offset: 0x0000038C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DayAmbient = ");
			builder.Append(this.DayAmbient);
			builder.Append(", NightAmbient = ");
			builder.Append(this.NightAmbient);
			builder.Append(", WaterAmbient = ");
			builder.Append(this.WaterAmbient);
			return true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FA File Offset: 0x000003FA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AmbientSpec left, AmbientSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002206 File Offset: 0x00000406
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AmbientSpec left, AmbientSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000221C File Offset: 0x0000041C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DayAmbient>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NightAmbient>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WaterAmbient>k__BackingField);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002274 File Offset: 0x00000474
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AmbientSpec);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002282 File Offset: 0x00000482
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000228C File Offset: 0x0000048C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AmbientSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<DayAmbient>k__BackingField, other.<DayAmbient>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NightAmbient>k__BackingField, other.<NightAmbient>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WaterAmbient>k__BackingField, other.<WaterAmbient>k__BackingField));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022F8 File Offset: 0x000004F8
		[CompilerGenerated]
		protected AmbientSpec([Nullable(1)] AmbientSpec original) : base(original)
		{
			this.DayAmbient = original.<DayAmbient>k__BackingField;
			this.NightAmbient = original.<NightAmbient>k__BackingField;
			this.WaterAmbient = original.<WaterAmbient>k__BackingField;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002325 File Offset: 0x00000525
		public AmbientSpec()
		{
		}
	}
}
