using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Yielding
{
	// Token: 0x0200001A RID: 26
	public class YieldRemovalChanceBonusSpec : ComponentSpec, IEquatable<YieldRemovalChanceBonusSpec>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003A58 File Offset: 0x00001C58
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(YieldRemovalChanceBonusSpec);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003A64 File Offset: 0x00001C64
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003A6C File Offset: 0x00001C6C
		[Serialize]
		public string BonusId { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003A75 File Offset: 0x00001C75
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003A7D File Offset: 0x00001C7D
		[Serialize]
		public string GoodId { get; set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00003A88 File Offset: 0x00001C88
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("YieldRemovalChanceBonusSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003AD4 File Offset: 0x00001CD4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BonusId = ");
			builder.Append(this.BonusId);
			builder.Append(", GoodId = ");
			builder.Append(this.GoodId);
			return true;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003B29 File Offset: 0x00001D29
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(YieldRemovalChanceBonusSpec left, YieldRemovalChanceBonusSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003B35 File Offset: 0x00001D35
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(YieldRemovalChanceBonusSpec left, YieldRemovalChanceBonusSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003B49 File Offset: 0x00001D49
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BonusId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodId>k__BackingField);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003B7F File Offset: 0x00001D7F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as YieldRemovalChanceBonusSpec);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002AFA File Offset: 0x00000CFA
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003B90 File Offset: 0x00001D90
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(YieldRemovalChanceBonusSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<BonusId>k__BackingField, other.<BonusId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoodId>k__BackingField, other.<GoodId>k__BackingField));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003BE4 File Offset: 0x00001DE4
		[CompilerGenerated]
		protected YieldRemovalChanceBonusSpec([Nullable(1)] YieldRemovalChanceBonusSpec original) : base(original)
		{
			this.BonusId = original.<BonusId>k__BackingField;
			this.GoodId = original.<GoodId>k__BackingField;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002B9D File Offset: 0x00000D9D
		public YieldRemovalChanceBonusSpec()
		{
		}
	}
}
