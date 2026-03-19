using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	public class StockpileIlluminatorSpec : ComponentSpec, IEquatable<StockpileIlluminatorSpec>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003AFB File Offset: 0x00001CFB
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(StockpileIlluminatorSpec);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B08 File Offset: 0x00001D08
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StockpileIlluminatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003B54 File Offset: 0x00001D54
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003B5D File Offset: 0x00001D5D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StockpileIlluminatorSpec left, StockpileIlluminatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003B69 File Offset: 0x00001D69
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StockpileIlluminatorSpec left, StockpileIlluminatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003B7D File Offset: 0x00001D7D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B85 File Offset: 0x00001D85
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StockpileIlluminatorSpec);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B93 File Offset: 0x00001D93
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003B9C File Offset: 0x00001D9C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StockpileIlluminatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003BB3 File Offset: 0x00001DB3
		[CompilerGenerated]
		protected StockpileIlluminatorSpec(StockpileIlluminatorSpec original) : base(original)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BBC File Offset: 0x00001DBC
		public StockpileIlluminatorSpec()
		{
		}
	}
}
