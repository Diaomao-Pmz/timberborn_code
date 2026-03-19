using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.BonusSystem;

namespace Timberborn.NeedSpecs
{
	// Token: 0x02000015 RID: 21
	public class PunitiveNeedSpec : ComponentSpec, IEquatable<PunitiveNeedSpec>
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003A0F File Offset: 0x00001C0F
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PunitiveNeedSpec);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003A1B File Offset: 0x00001C1B
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003A23 File Offset: 0x00001C23
		[Serialize]
		public ImmutableArray<BonusSpec> Penalties { get; set; }

		// Token: 0x060000C0 RID: 192 RVA: 0x00003A2C File Offset: 0x00001C2C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PunitiveNeedSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003A78 File Offset: 0x00001C78
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Penalties = ");
			builder.Append(this.Penalties.ToString());
			return true;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003AC2 File Offset: 0x00001CC2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PunitiveNeedSpec left, PunitiveNeedSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003ACE File Offset: 0x00001CCE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PunitiveNeedSpec left, PunitiveNeedSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003AE2 File Offset: 0x00001CE2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<BonusSpec>>.Default.GetHashCode(this.<Penalties>k__BackingField);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003B01 File Offset: 0x00001D01
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PunitiveNeedSpec);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000271B File Offset: 0x0000091B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003B0F File Offset: 0x00001D0F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PunitiveNeedSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<BonusSpec>>.Default.Equals(this.<Penalties>k__BackingField, other.<Penalties>k__BackingField));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003B40 File Offset: 0x00001D40
		[CompilerGenerated]
		protected PunitiveNeedSpec([Nullable(1)] PunitiveNeedSpec original) : base(original)
		{
			this.Penalties = original.<Penalties>k__BackingField;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000283C File Offset: 0x00000A3C
		public PunitiveNeedSpec()
		{
		}
	}
}
