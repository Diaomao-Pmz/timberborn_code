using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Stockpiles
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class FixedStockpileSpec : ComponentSpec, IEquatable<FixedStockpileSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021AE File Offset: 0x000003AE
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FixedStockpileSpec);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021BC File Offset: 0x000003BC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FixedStockpileSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002208 File Offset: 0x00000408
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002211 File Offset: 0x00000411
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FixedStockpileSpec left, FixedStockpileSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000221D File Offset: 0x0000041D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FixedStockpileSpec left, FixedStockpileSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002231 File Offset: 0x00000431
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002239 File Offset: 0x00000439
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FixedStockpileSpec);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002247 File Offset: 0x00000447
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002250 File Offset: 0x00000450
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FixedStockpileSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002267 File Offset: 0x00000467
		[CompilerGenerated]
		protected FixedStockpileSpec(FixedStockpileSpec original) : base(original)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002270 File Offset: 0x00000470
		public FixedStockpileSpec()
		{
		}
	}
}
