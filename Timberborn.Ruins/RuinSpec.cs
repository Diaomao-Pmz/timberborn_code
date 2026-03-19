using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Yielding;

namespace Timberborn.Ruins
{
	// Token: 0x02000012 RID: 18
	public class RuinSpec : ComponentSpec, IYielderDecorable, IOrderableYielder, IEquatable<RuinSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002EE1 File Offset: 0x000010E1
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RuinSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002EED File Offset: 0x000010ED
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002EF5 File Offset: 0x000010F5
		[Serialize]
		public string ModelParentName { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002EFE File Offset: 0x000010FE
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002F06 File Offset: 0x00001106
		[Serialize]
		public int RuinHeight { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002F0F File Offset: 0x0000110F
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002F17 File Offset: 0x00001117
		[Serialize]
		public YielderSpec Yielder { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002F20 File Offset: 0x00001120
		public int Order
		{
			get
			{
				return this.RuinHeight;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F28 File Offset: 0x00001128
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RuinSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F74 File Offset: 0x00001174
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ModelParentName = ");
			builder.Append(this.ModelParentName);
			builder.Append(", RuinHeight = ");
			builder.Append(this.RuinHeight.ToString());
			builder.Append(", Yielder = ");
			builder.Append(this.Yielder);
			builder.Append(", Order = ");
			builder.Append(this.Order.ToString());
			return true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003017 File Offset: 0x00001217
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RuinSpec left, RuinSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003023 File Offset: 0x00001223
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RuinSpec left, RuinSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003038 File Offset: 0x00001238
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModelParentName>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RuinHeight>k__BackingField)) * -1521134295 + EqualityComparer<YielderSpec>.Default.GetHashCode(this.<Yielder>k__BackingField);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003090 File Offset: 0x00001290
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RuinSpec);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000268E File Offset: 0x0000088E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030A0 File Offset: 0x000012A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RuinSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ModelParentName>k__BackingField, other.<ModelParentName>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RuinHeight>k__BackingField, other.<RuinHeight>k__BackingField) && EqualityComparer<YielderSpec>.Default.Equals(this.<Yielder>k__BackingField, other.<Yielder>k__BackingField));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000310C File Offset: 0x0000130C
		[CompilerGenerated]
		protected RuinSpec([Nullable(1)] RuinSpec original) : base(original)
		{
			this.ModelParentName = original.<ModelParentName>k__BackingField;
			this.RuinHeight = original.<RuinHeight>k__BackingField;
			this.Yielder = original.<Yielder>k__BackingField;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002731 File Offset: 0x00000931
		public RuinSpec()
		{
		}
	}
}
