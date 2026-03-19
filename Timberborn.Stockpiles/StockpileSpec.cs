using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Stockpiles
{
	// Token: 0x0200000D RID: 13
	public class StockpileSpec : ComponentSpec, IEquatable<StockpileSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002476 File Offset: 0x00000676
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(StockpileSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002482 File Offset: 0x00000682
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000248A File Offset: 0x0000068A
		[Serialize]
		public int MaxCapacity { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002493 File Offset: 0x00000693
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000249B File Offset: 0x0000069B
		[Serialize]
		public string WhitelistedGoodType { get; set; }

		// Token: 0x06000030 RID: 48 RVA: 0x000024A4 File Offset: 0x000006A4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StockpileSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024F0 File Offset: 0x000006F0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxCapacity = ");
			builder.Append(this.MaxCapacity.ToString());
			builder.Append(", WhitelistedGoodType = ");
			builder.Append(this.WhitelistedGoodType);
			return true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002553 File Offset: 0x00000753
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StockpileSpec left, StockpileSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000255F File Offset: 0x0000075F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StockpileSpec left, StockpileSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002573 File Offset: 0x00000773
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxCapacity>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WhitelistedGoodType>k__BackingField);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025A9 File Offset: 0x000007A9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StockpileSpec);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002247 File Offset: 0x00000447
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025B8 File Offset: 0x000007B8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StockpileSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxCapacity>k__BackingField, other.<MaxCapacity>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WhitelistedGoodType>k__BackingField, other.<WhitelistedGoodType>k__BackingField));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000260C File Offset: 0x0000080C
		[CompilerGenerated]
		protected StockpileSpec([Nullable(1)] StockpileSpec original) : base(original)
		{
			this.MaxCapacity = original.<MaxCapacity>k__BackingField;
			this.WhitelistedGoodType = original.<WhitelistedGoodType>k__BackingField;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002270 File Offset: 0x00000470
		public StockpileSpec()
		{
		}
	}
}
