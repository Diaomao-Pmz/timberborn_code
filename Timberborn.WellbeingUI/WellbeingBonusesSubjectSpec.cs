using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200001D RID: 29
	public class WellbeingBonusesSubjectSpec : ComponentSpec, IEquatable<WellbeingBonusesSubjectSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003CF7 File Offset: 0x00001EF7
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WellbeingBonusesSubjectSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003D03 File Offset: 0x00001F03
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003D0B File Offset: 0x00001F0B
		[Serialize]
		public ImmutableArray<string> Bonuses { get; set; }

		// Token: 0x06000092 RID: 146 RVA: 0x00003D14 File Offset: 0x00001F14
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WellbeingBonusesSubjectSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003D60 File Offset: 0x00001F60
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Bonuses = ");
			builder.Append(this.Bonuses.ToString());
			return true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003DAA File Offset: 0x00001FAA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WellbeingBonusesSubjectSpec left, WellbeingBonusesSubjectSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003DB6 File Offset: 0x00001FB6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WellbeingBonusesSubjectSpec left, WellbeingBonusesSubjectSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003DCA File Offset: 0x00001FCA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<Bonuses>k__BackingField);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003DE9 File Offset: 0x00001FE9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WellbeingBonusesSubjectSpec);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000027DF File Offset: 0x000009DF
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003DF7 File Offset: 0x00001FF7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WellbeingBonusesSubjectSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<Bonuses>k__BackingField, other.<Bonuses>k__BackingField));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003E28 File Offset: 0x00002028
		[CompilerGenerated]
		protected WellbeingBonusesSubjectSpec([Nullable(1)] WellbeingBonusesSubjectSpec original) : base(original)
		{
			this.Bonuses = original.<Bonuses>k__BackingField;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002808 File Offset: 0x00000A08
		public WellbeingBonusesSubjectSpec()
		{
		}
	}
}
