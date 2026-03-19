using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NaturalResourcesMoisture
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	public class WateredNaturalResourceSpec : ComponentSpec, IEquatable<WateredNaturalResourceSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002D46 File Offset: 0x00000F46
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WateredNaturalResourceSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002D52 File Offset: 0x00000F52
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002D5A File Offset: 0x00000F5A
		[Serialize]
		public float DaysToDieDry { get; set; }

		// Token: 0x06000057 RID: 87 RVA: 0x00002D64 File Offset: 0x00000F64
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WateredNaturalResourceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DB0 File Offset: 0x00000FB0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DaysToDieDry = ");
			builder.Append(this.DaysToDieDry.ToString());
			return true;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002DFA File Offset: 0x00000FFA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WateredNaturalResourceSpec left, WateredNaturalResourceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E06 File Offset: 0x00001006
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WateredNaturalResourceSpec left, WateredNaturalResourceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E1A File Offset: 0x0000101A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DaysToDieDry>k__BackingField);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E39 File Offset: 0x00001039
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WateredNaturalResourceSpec);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000023A2 File Offset: 0x000005A2
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E47 File Offset: 0x00001047
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WateredNaturalResourceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<DaysToDieDry>k__BackingField, other.<DaysToDieDry>k__BackingField));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E78 File Offset: 0x00001078
		[CompilerGenerated]
		protected WateredNaturalResourceSpec(WateredNaturalResourceSpec original) : base(original)
		{
			this.DaysToDieDry = original.<DaysToDieDry>k__BackingField;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002445 File Offset: 0x00000645
		public WateredNaturalResourceSpec()
		{
		}
	}
}
