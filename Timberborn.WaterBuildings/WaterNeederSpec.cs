using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterNeederSpec : ComponentSpec, IEquatable<WaterNeederSpec>
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00008219 File Offset: 0x00006419
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterNeederSpec);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008228 File Offset: 0x00006428
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterNeederSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00002F68 File Offset: 0x00001168
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008274 File Offset: 0x00006474
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterNeederSpec left, WaterNeederSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008280 File Offset: 0x00006480
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterNeederSpec left, WaterNeederSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00002F91 File Offset: 0x00001191
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008294 File Offset: 0x00006494
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterNeederSpec);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00002FA7 File Offset: 0x000011A7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterNeederSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00002FBE File Offset: 0x000011BE
		[CompilerGenerated]
		protected WaterNeederSpec(WaterNeederSpec original) : base(original)
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00002CBC File Offset: 0x00000EBC
		public WaterNeederSpec()
		{
		}
	}
}
