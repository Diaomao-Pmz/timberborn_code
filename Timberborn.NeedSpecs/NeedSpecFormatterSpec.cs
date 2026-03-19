using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.NeedSpecs
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class NeedSpecFormatterSpec : ComponentSpec, IEquatable<NeedSpecFormatterSpec>
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003824 File Offset: 0x00001A24
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(NeedSpecFormatterSpec);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003830 File Offset: 0x00001A30
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003838 File Offset: 0x00001A38
		[Serialize]
		public Color PositiveHighlightColor { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003841 File Offset: 0x00001A41
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003849 File Offset: 0x00001A49
		[Serialize]
		public Color NegativeHighlightColor { get; set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00003854 File Offset: 0x00001A54
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NeedSpecFormatterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000038A0 File Offset: 0x00001AA0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PositiveHighlightColor = ");
			builder.Append(this.PositiveHighlightColor.ToString());
			builder.Append(", NegativeHighlightColor = ");
			builder.Append(this.NegativeHighlightColor.ToString());
			return true;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003911 File Offset: 0x00001B11
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NeedSpecFormatterSpec left, NeedSpecFormatterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000391D File Offset: 0x00001B1D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NeedSpecFormatterSpec left, NeedSpecFormatterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003931 File Offset: 0x00001B31
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PositiveHighlightColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<NegativeHighlightColor>k__BackingField);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003967 File Offset: 0x00001B67
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NeedSpecFormatterSpec);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000271B File Offset: 0x0000091B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003978 File Offset: 0x00001B78
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NeedSpecFormatterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<PositiveHighlightColor>k__BackingField, other.<PositiveHighlightColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<NegativeHighlightColor>k__BackingField, other.<NegativeHighlightColor>k__BackingField));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000039CC File Offset: 0x00001BCC
		[CompilerGenerated]
		protected NeedSpecFormatterSpec(NeedSpecFormatterSpec original) : base(original)
		{
			this.PositiveHighlightColor = original.<PositiveHighlightColor>k__BackingField;
			this.NegativeHighlightColor = original.<NegativeHighlightColor>k__BackingField;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000283C File Offset: 0x00000A3C
		public NeedSpecFormatterSpec()
		{
		}
	}
}
