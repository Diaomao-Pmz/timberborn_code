using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Goods;

namespace Timberborn.Wonders
{
	// Token: 0x0200001C RID: 28
	public class WonderInventorySpec : ComponentSpec, IEquatable<WonderInventorySpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003288 File Offset: 0x00001488
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WonderInventorySpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003294 File Offset: 0x00001494
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000329C File Offset: 0x0000149C
		[Serialize]
		public ImmutableArray<GoodAmountSpec> RequiredGoods { get; set; }

		// Token: 0x0600009C RID: 156 RVA: 0x000032A8 File Offset: 0x000014A8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WonderInventorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000032F4 File Offset: 0x000014F4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("RequiredGoods = ");
			builder.Append(this.RequiredGoods.ToString());
			return true;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000333E File Offset: 0x0000153E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WonderInventorySpec left, WonderInventorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000334A File Offset: 0x0000154A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WonderInventorySpec left, WonderInventorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000335E File Offset: 0x0000155E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.GetHashCode(this.<RequiredGoods>k__BackingField);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000337D File Offset: 0x0000157D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WonderInventorySpec);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000029FB File Offset: 0x00000BFB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000338B File Offset: 0x0000158B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WonderInventorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.Equals(this.<RequiredGoods>k__BackingField, other.<RequiredGoods>k__BackingField));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000033BC File Offset: 0x000015BC
		[CompilerGenerated]
		protected WonderInventorySpec([Nullable(1)] WonderInventorySpec original) : base(original)
		{
			this.RequiredGoods = original.<RequiredGoods>k__BackingField;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002A4A File Offset: 0x00000C4A
		public WonderInventorySpec()
		{
		}
	}
}
