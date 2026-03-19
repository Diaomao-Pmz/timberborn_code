using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class FloodableBuildingSpec : ComponentSpec, IEquatable<FloodableBuildingSpec>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002F0E File Offset: 0x0000110E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FloodableBuildingSpec);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F1C File Offset: 0x0000111C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FloodableBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F68 File Offset: 0x00001168
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002F71 File Offset: 0x00001171
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FloodableBuildingSpec left, FloodableBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F7D File Offset: 0x0000117D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FloodableBuildingSpec left, FloodableBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002F91 File Offset: 0x00001191
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F99 File Offset: 0x00001199
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FloodableBuildingSpec);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002FA7 File Offset: 0x000011A7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FloodableBuildingSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002FBE File Offset: 0x000011BE
		[CompilerGenerated]
		protected FloodableBuildingSpec(FloodableBuildingSpec original) : base(original)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002CBC File Offset: 0x00000EBC
		public FloodableBuildingSpec()
		{
		}
	}
}
