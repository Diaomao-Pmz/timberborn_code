using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000018 RID: 24
	public class WorkshopRandomNeedApplierSpec : ComponentSpec, INeedEffectsSpec, IEquatable<WorkshopRandomNeedApplierSpec>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003668 File Offset: 0x00001868
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkshopRandomNeedApplierSpec);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003674 File Offset: 0x00001874
		// (set) Token: 0x060000AB RID: 171 RVA: 0x0000367C File Offset: 0x0000187C
		[Serialize]
		public ImmutableArray<NeedApplierEffectSpec> Effects { get; set; }

		// Token: 0x060000AC RID: 172 RVA: 0x00003688 File Offset: 0x00001888
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkshopRandomNeedApplierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000036D4 File Offset: 0x000018D4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Effects = ");
			builder.Append(this.Effects.ToString());
			return true;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000371E File Offset: 0x0000191E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkshopRandomNeedApplierSpec left, WorkshopRandomNeedApplierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000372A File Offset: 0x0000192A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkshopRandomNeedApplierSpec left, WorkshopRandomNeedApplierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000373E File Offset: 0x0000193E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<NeedApplierEffectSpec>>.Default.GetHashCode(this.<Effects>k__BackingField);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000375D File Offset: 0x0000195D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkshopRandomNeedApplierSpec);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002519 File Offset: 0x00000719
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000376B File Offset: 0x0000196B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkshopRandomNeedApplierSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<NeedApplierEffectSpec>>.Default.Equals(this.<Effects>k__BackingField, other.<Effects>k__BackingField));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000379C File Offset: 0x0000199C
		[CompilerGenerated]
		protected WorkshopRandomNeedApplierSpec([Nullable(1)] WorkshopRandomNeedApplierSpec original) : base(original)
		{
			this.Effects = original.<Effects>k__BackingField;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002599 File Offset: 0x00000799
		public WorkshopRandomNeedApplierSpec()
		{
		}
	}
}
