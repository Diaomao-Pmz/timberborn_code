using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Carrying
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public class GoodCarrierSpec : ComponentSpec, IEquatable<GoodCarrierSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000035E9 File Offset: 0x000017E9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GoodCarrierSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000035F5 File Offset: 0x000017F5
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000035FD File Offset: 0x000017FD
		[Serialize]
		public int BaseLiftingCapacity { get; set; }

		// Token: 0x06000085 RID: 133 RVA: 0x00003608 File Offset: 0x00001808
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodCarrierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003654 File Offset: 0x00001854
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BaseLiftingCapacity = ");
			builder.Append(this.BaseLiftingCapacity.ToString());
			return true;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000369E File Offset: 0x0000189E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodCarrierSpec left, GoodCarrierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000036AA File Offset: 0x000018AA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodCarrierSpec left, GoodCarrierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000036BE File Offset: 0x000018BE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<BaseLiftingCapacity>k__BackingField);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000036DD File Offset: 0x000018DD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodCarrierSpec);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003561 File Offset: 0x00001761
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000036EB File Offset: 0x000018EB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodCarrierSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<BaseLiftingCapacity>k__BackingField, other.<BaseLiftingCapacity>k__BackingField));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000371C File Offset: 0x0000191C
		[CompilerGenerated]
		protected GoodCarrierSpec(GoodCarrierSpec original) : base(original)
		{
			this.BaseLiftingCapacity = original.<BaseLiftingCapacity>k__BackingField;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000035E1 File Offset: 0x000017E1
		public GoodCarrierSpec()
		{
		}
	}
}
