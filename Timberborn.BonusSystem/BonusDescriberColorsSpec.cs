using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BonusSystem
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class BonusDescriberColorsSpec : ComponentSpec, IEquatable<BonusDescriberColorsSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002247 File Offset: 0x00000447
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BonusDescriberColorsSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002253 File Offset: 0x00000453
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000225B File Offset: 0x0000045B
		[Serialize]
		public Color PositiveBonusHighlight { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002264 File Offset: 0x00000464
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000226C File Offset: 0x0000046C
		[Serialize]
		public Color NegativeBonusHighlight { get; set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002278 File Offset: 0x00000478
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BonusDescriberColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C4 File Offset: 0x000004C4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PositiveBonusHighlight = ");
			builder.Append(this.PositiveBonusHighlight.ToString());
			builder.Append(", NegativeBonusHighlight = ");
			builder.Append(this.NegativeBonusHighlight.ToString());
			return true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002335 File Offset: 0x00000535
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BonusDescriberColorsSpec left, BonusDescriberColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002341 File Offset: 0x00000541
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BonusDescriberColorsSpec left, BonusDescriberColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002355 File Offset: 0x00000555
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PositiveBonusHighlight>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<NegativeBonusHighlight>k__BackingField);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000238B File Offset: 0x0000058B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BonusDescriberColorsSpec);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002399 File Offset: 0x00000599
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023A4 File Offset: 0x000005A4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BonusDescriberColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<PositiveBonusHighlight>k__BackingField, other.<PositiveBonusHighlight>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<NegativeBonusHighlight>k__BackingField, other.<NegativeBonusHighlight>k__BackingField));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023F8 File Offset: 0x000005F8
		[CompilerGenerated]
		protected BonusDescriberColorsSpec(BonusDescriberColorsSpec original) : base(original)
		{
			this.PositiveBonusHighlight = original.<PositiveBonusHighlight>k__BackingField;
			this.NegativeBonusHighlight = original.<NegativeBonusHighlight>k__BackingField;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002419 File Offset: 0x00000619
		public BonusDescriberColorsSpec()
		{
		}
	}
}
