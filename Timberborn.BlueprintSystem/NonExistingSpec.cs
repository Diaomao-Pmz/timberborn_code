using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000021 RID: 33
	public class NonExistingSpec : ComponentSpec, IEquatable<NonExistingSpec>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003EFB File Offset: 0x000020FB
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(NonExistingSpec);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003F07 File Offset: 0x00002107
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003F0F File Offset: 0x0000210F
		[Serialize]
		public string SpecName { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003F18 File Offset: 0x00002118
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003F20 File Offset: 0x00002120
		[Serialize]
		public string Content { get; set; }

		// Token: 0x060000DA RID: 218 RVA: 0x00003F2C File Offset: 0x0000212C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NonExistingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003F78 File Offset: 0x00002178
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SpecName = ");
			builder.Append(this.SpecName);
			builder.Append(", Content = ");
			builder.Append(this.Content);
			return true;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003FCD File Offset: 0x000021CD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NonExistingSpec left, NonExistingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003FD9 File Offset: 0x000021D9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NonExistingSpec left, NonExistingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003FED File Offset: 0x000021ED
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SpecName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Content>k__BackingField);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004023 File Offset: 0x00002223
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NonExistingSpec);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003CE0 File Offset: 0x00001EE0
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004034 File Offset: 0x00002234
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NonExistingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<SpecName>k__BackingField, other.<SpecName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Content>k__BackingField, other.<Content>k__BackingField));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004088 File Offset: 0x00002288
		[CompilerGenerated]
		protected NonExistingSpec([Nullable(1)] NonExistingSpec original) : base(original)
		{
			this.SpecName = original.<SpecName>k__BackingField;
			this.Content = original.<Content>k__BackingField;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003D2F File Offset: 0x00001F2F
		public NonExistingSpec()
		{
		}
	}
}
