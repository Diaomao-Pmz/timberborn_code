using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	public class FloodableFireSpec : ComponentSpec, IEquatable<FloodableFireSpec>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000308C File Offset: 0x0000128C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FloodableFireSpec);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003098 File Offset: 0x00001298
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FloodableFireSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F68 File Offset: 0x00001168
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030E4 File Offset: 0x000012E4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FloodableFireSpec left, FloodableFireSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030F0 File Offset: 0x000012F0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FloodableFireSpec left, FloodableFireSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002F91 File Offset: 0x00001191
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003104 File Offset: 0x00001304
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FloodableFireSpec);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002FA7 File Offset: 0x000011A7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FloodableFireSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002FBE File Offset: 0x000011BE
		[CompilerGenerated]
		protected FloodableFireSpec(FloodableFireSpec original) : base(original)
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002CBC File Offset: 0x00000EBC
		public FloodableFireSpec()
		{
		}
	}
}
