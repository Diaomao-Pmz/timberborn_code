using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000008 RID: 8
	public class AreaNeedApplierSpec : ComponentSpec, INeedEffectsSpec, IEquatable<AreaNeedApplierSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000023C7 File Offset: 0x000005C7
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AreaNeedApplierSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023D3 File Offset: 0x000005D3
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000023DB File Offset: 0x000005DB
		[Serialize]
		public int ApplicationRadius { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023E4 File Offset: 0x000005E4
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000023EC File Offset: 0x000005EC
		[Serialize]
		public ImmutableArray<NeedApplierEffectSpec> Effects { get; set; }

		// Token: 0x0600001A RID: 26 RVA: 0x000023F8 File Offset: 0x000005F8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AreaNeedApplierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002444 File Offset: 0x00000644
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ApplicationRadius = ");
			builder.Append(this.ApplicationRadius.ToString());
			builder.Append(", Effects = ");
			builder.Append(this.Effects.ToString());
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024B5 File Offset: 0x000006B5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AreaNeedApplierSpec left, AreaNeedApplierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024C1 File Offset: 0x000006C1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AreaNeedApplierSpec left, AreaNeedApplierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024D5 File Offset: 0x000006D5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ApplicationRadius>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<NeedApplierEffectSpec>>.Default.GetHashCode(this.<Effects>k__BackingField);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000250B File Offset: 0x0000070B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AreaNeedApplierSpec);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002519 File Offset: 0x00000719
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002524 File Offset: 0x00000724
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AreaNeedApplierSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<ApplicationRadius>k__BackingField, other.<ApplicationRadius>k__BackingField) && EqualityComparer<ImmutableArray<NeedApplierEffectSpec>>.Default.Equals(this.<Effects>k__BackingField, other.<Effects>k__BackingField));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002578 File Offset: 0x00000778
		[CompilerGenerated]
		protected AreaNeedApplierSpec([Nullable(1)] AreaNeedApplierSpec original) : base(original)
		{
			this.ApplicationRadius = original.<ApplicationRadius>k__BackingField;
			this.Effects = original.<Effects>k__BackingField;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002599 File Offset: 0x00000799
		public AreaNeedApplierSpec()
		{
		}
	}
}
