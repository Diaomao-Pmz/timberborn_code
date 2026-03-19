using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SoakedEffects
{
	// Token: 0x02000007 RID: 7
	[NullableContext(1)]
	[Nullable(0)]
	public class NeedAffectedBySoakednessSpec : ComponentSpec, IEquatable<NeedAffectedBySoakednessSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(NeedAffectedBySoakednessSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public float PointsPerHour { get; set; }

		// Token: 0x0600000A RID: 10 RVA: 0x0000211C File Offset: 0x0000031C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NeedAffectedBySoakednessSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002168 File Offset: 0x00000368
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PointsPerHour = ");
			builder.Append(this.PointsPerHour.ToString());
			return true;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B2 File Offset: 0x000003B2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NeedAffectedBySoakednessSpec left, NeedAffectedBySoakednessSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021BE File Offset: 0x000003BE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NeedAffectedBySoakednessSpec left, NeedAffectedBySoakednessSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D2 File Offset: 0x000003D2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<PointsPerHour>k__BackingField);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021F1 File Offset: 0x000003F1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NeedAffectedBySoakednessSpec);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FF File Offset: 0x000003FF
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002208 File Offset: 0x00000408
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NeedAffectedBySoakednessSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<PointsPerHour>k__BackingField, other.<PointsPerHour>k__BackingField));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002239 File Offset: 0x00000439
		[CompilerGenerated]
		protected NeedAffectedBySoakednessSpec(NeedAffectedBySoakednessSpec original) : base(original)
		{
			this.PointsPerHour = original.<PointsPerHour>k__BackingField;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000224E File Offset: 0x0000044E
		public NeedAffectedBySoakednessSpec()
		{
		}
	}
}
