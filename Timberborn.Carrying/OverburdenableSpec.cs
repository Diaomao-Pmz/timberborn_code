using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.BonusSystem;

namespace Timberborn.Carrying
{
	// Token: 0x02000017 RID: 23
	public class OverburdenableSpec : ComponentSpec, IEquatable<OverburdenableSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003873 File Offset: 0x00001A73
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(OverburdenableSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000387F File Offset: 0x00001A7F
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003887 File Offset: 0x00001A87
		[Serialize]
		public ImmutableArray<BonusSpec> OverburdenedBonuses { get; set; }

		// Token: 0x0600009B RID: 155 RVA: 0x00003890 File Offset: 0x00001A90
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OverburdenableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000038DC File Offset: 0x00001ADC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("OverburdenedBonuses = ");
			builder.Append(this.OverburdenedBonuses.ToString());
			return true;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003926 File Offset: 0x00001B26
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(OverburdenableSpec left, OverburdenableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003932 File Offset: 0x00001B32
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(OverburdenableSpec left, OverburdenableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003946 File Offset: 0x00001B46
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<BonusSpec>>.Default.GetHashCode(this.<OverburdenedBonuses>k__BackingField);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003965 File Offset: 0x00001B65
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OverburdenableSpec);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003561 File Offset: 0x00001761
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003973 File Offset: 0x00001B73
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(OverburdenableSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<BonusSpec>>.Default.Equals(this.<OverburdenedBonuses>k__BackingField, other.<OverburdenedBonuses>k__BackingField));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000039A4 File Offset: 0x00001BA4
		[CompilerGenerated]
		protected OverburdenableSpec([Nullable(1)] OverburdenableSpec original) : base(original)
		{
			this.OverburdenedBonuses = original.<OverburdenedBonuses>k__BackingField;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000035E1 File Offset: 0x000017E1
		public OverburdenableSpec()
		{
		}
	}
}
