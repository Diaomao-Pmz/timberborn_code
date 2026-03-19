using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000022 RID: 34
	public class ProductionResetterSpec : ComponentSpec, IEquatable<ProductionResetterSpec>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000431D File Offset: 0x0000251D
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ProductionResetterSpec);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004329 File Offset: 0x00002529
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004331 File Offset: 0x00002531
		[Serialize]
		public float HoursToResetProgress { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000433A File Offset: 0x0000253A
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004342 File Offset: 0x00002542
		[Serialize]
		public string StatusLocKey { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000434B File Offset: 0x0000254B
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00004353 File Offset: 0x00002553
		[Serialize]
		public string AlertLocKey { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000435C File Offset: 0x0000255C
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004364 File Offset: 0x00002564
		[Serialize]
		public string StatusIcon { get; set; }

		// Token: 0x060000E7 RID: 231 RVA: 0x00004370 File Offset: 0x00002570
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ProductionResetterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000043BC File Offset: 0x000025BC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("HoursToResetProgress = ");
			builder.Append(this.HoursToResetProgress.ToString());
			builder.Append(", StatusLocKey = ");
			builder.Append(this.StatusLocKey);
			builder.Append(", AlertLocKey = ");
			builder.Append(this.AlertLocKey);
			builder.Append(", StatusIcon = ");
			builder.Append(this.StatusIcon);
			return true;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004451 File Offset: 0x00002651
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ProductionResetterSpec left, ProductionResetterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000445D File Offset: 0x0000265D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ProductionResetterSpec left, ProductionResetterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004474 File Offset: 0x00002674
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<HoursToResetProgress>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StatusLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AlertLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StatusIcon>k__BackingField);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000044E3 File Offset: 0x000026E3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ProductionResetterSpec);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003857 File Offset: 0x00001A57
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000044F4 File Offset: 0x000026F4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ProductionResetterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<HoursToResetProgress>k__BackingField, other.<HoursToResetProgress>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StatusLocKey>k__BackingField, other.<StatusLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AlertLocKey>k__BackingField, other.<AlertLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StatusIcon>k__BackingField, other.<StatusIcon>k__BackingField));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004578 File Offset: 0x00002778
		[CompilerGenerated]
		protected ProductionResetterSpec([Nullable(1)] ProductionResetterSpec original) : base(original)
		{
			this.HoursToResetProgress = original.<HoursToResetProgress>k__BackingField;
			this.StatusLocKey = original.<StatusLocKey>k__BackingField;
			this.AlertLocKey = original.<AlertLocKey>k__BackingField;
			this.StatusIcon = original.<StatusIcon>k__BackingField;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000038A6 File Offset: 0x00001AA6
		public ProductionResetterSpec()
		{
		}
	}
}
