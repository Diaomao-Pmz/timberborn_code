using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.NeedSpecs;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x0200000B RID: 11
	public class ContinuousEffectBuildingSpec : ComponentSpec, IEquatable<ContinuousEffectBuildingSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022FB File Offset: 0x000004FB
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ContinuousEffectBuildingSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002307 File Offset: 0x00000507
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000230F File Offset: 0x0000050F
		[Serialize]
		public ImmutableArray<ContinuousEffectSpec> Effects { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x00002318 File Offset: 0x00000518
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ContinuousEffectBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002364 File Offset: 0x00000564
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

		// Token: 0x0600001E RID: 30 RVA: 0x000023AE File Offset: 0x000005AE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ContinuousEffectBuildingSpec left, ContinuousEffectBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023BA File Offset: 0x000005BA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ContinuousEffectBuildingSpec left, ContinuousEffectBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023CE File Offset: 0x000005CE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.GetHashCode(this.<Effects>k__BackingField);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ContinuousEffectBuildingSpec);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023FB File Offset: 0x000005FB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002404 File Offset: 0x00000604
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ContinuousEffectBuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.Equals(this.<Effects>k__BackingField, other.<Effects>k__BackingField));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002435 File Offset: 0x00000635
		[CompilerGenerated]
		protected ContinuousEffectBuildingSpec([Nullable(1)] ContinuousEffectBuildingSpec original) : base(original)
		{
			this.Effects = original.<Effects>k__BackingField;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000244A File Offset: 0x0000064A
		public ContinuousEffectBuildingSpec()
		{
		}
	}
}
