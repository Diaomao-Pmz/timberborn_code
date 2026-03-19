using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedApplication
{
	// Token: 0x0200001B RID: 27
	public class YieldRemoverWorkplaceEffectsSpec : ComponentSpec, INeedEffectsSpec, IEquatable<YieldRemoverWorkplaceEffectsSpec>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003A92 File Offset: 0x00001C92
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(YieldRemoverWorkplaceEffectsSpec);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003A9E File Offset: 0x00001C9E
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003AA6 File Offset: 0x00001CA6
		[Serialize]
		public string YieldGoodId { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003AAF File Offset: 0x00001CAF
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003AB7 File Offset: 0x00001CB7
		[Serialize]
		public int MinimumAttemptsThreshold { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003AC0 File Offset: 0x00001CC0
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003AC8 File Offset: 0x00001CC8
		[Serialize]
		public ImmutableArray<NeedApplierEffectSpec> Effects { get; set; }

		// Token: 0x060000D7 RID: 215 RVA: 0x00003AD4 File Offset: 0x00001CD4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("YieldRemoverWorkplaceEffectsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003B20 File Offset: 0x00001D20
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("YieldGoodId = ");
			builder.Append(this.YieldGoodId);
			builder.Append(", MinimumAttemptsThreshold = ");
			builder.Append(this.MinimumAttemptsThreshold.ToString());
			builder.Append(", Effects = ");
			builder.Append(this.Effects.ToString());
			return true;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003BAA File Offset: 0x00001DAA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(YieldRemoverWorkplaceEffectsSpec left, YieldRemoverWorkplaceEffectsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003BB6 File Offset: 0x00001DB6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(YieldRemoverWorkplaceEffectsSpec left, YieldRemoverWorkplaceEffectsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003BCC File Offset: 0x00001DCC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<YieldGoodId>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MinimumAttemptsThreshold>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<NeedApplierEffectSpec>>.Default.GetHashCode(this.<Effects>k__BackingField);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003C24 File Offset: 0x00001E24
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as YieldRemoverWorkplaceEffectsSpec);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002519 File Offset: 0x00000719
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003C34 File Offset: 0x00001E34
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(YieldRemoverWorkplaceEffectsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<YieldGoodId>k__BackingField, other.<YieldGoodId>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MinimumAttemptsThreshold>k__BackingField, other.<MinimumAttemptsThreshold>k__BackingField) && EqualityComparer<ImmutableArray<NeedApplierEffectSpec>>.Default.Equals(this.<Effects>k__BackingField, other.<Effects>k__BackingField));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003CA0 File Offset: 0x00001EA0
		[CompilerGenerated]
		protected YieldRemoverWorkplaceEffectsSpec([Nullable(1)] YieldRemoverWorkplaceEffectsSpec original) : base(original)
		{
			this.YieldGoodId = original.<YieldGoodId>k__BackingField;
			this.MinimumAttemptsThreshold = original.<MinimumAttemptsThreshold>k__BackingField;
			this.Effects = original.<Effects>k__BackingField;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002599 File Offset: 0x00000799
		public YieldRemoverWorkplaceEffectsSpec()
		{
		}
	}
}
