using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000009 RID: 9
	public class DemolishableEffectsSpec : ComponentSpec, INeedEffectsSpec, IEquatable<DemolishableEffectsSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025A1 File Offset: 0x000007A1
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DemolishableEffectsSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025AD File Offset: 0x000007AD
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000025B5 File Offset: 0x000007B5
		[Serialize]
		public ImmutableArray<NeedApplierEffectSpec> Effects { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000025C0 File Offset: 0x000007C0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolishableEffectsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000260C File Offset: 0x0000080C
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

		// Token: 0x0600002A RID: 42 RVA: 0x00002656 File Offset: 0x00000856
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolishableEffectsSpec left, DemolishableEffectsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002662 File Offset: 0x00000862
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolishableEffectsSpec left, DemolishableEffectsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002676 File Offset: 0x00000876
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<NeedApplierEffectSpec>>.Default.GetHashCode(this.<Effects>k__BackingField);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002695 File Offset: 0x00000895
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolishableEffectsSpec);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002519 File Offset: 0x00000719
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026A3 File Offset: 0x000008A3
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolishableEffectsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<NeedApplierEffectSpec>>.Default.Equals(this.<Effects>k__BackingField, other.<Effects>k__BackingField));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026D4 File Offset: 0x000008D4
		[CompilerGenerated]
		protected DemolishableEffectsSpec([Nullable(1)] DemolishableEffectsSpec original) : base(original)
		{
			this.Effects = original.<Effects>k__BackingField;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002599 File Offset: 0x00000799
		public DemolishableEffectsSpec()
		{
		}
	}
}
