using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WindSystem
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockableBuildingWindAnimatorSpec : ComponentSpec, IEquatable<BlockableBuildingWindAnimatorSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021C9 File Offset: 0x000003C9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockableBuildingWindAnimatorSpec);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D8 File Offset: 0x000003D8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockableBuildingWindAnimatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002224 File Offset: 0x00000424
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000222D File Offset: 0x0000042D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockableBuildingWindAnimatorSpec left, BlockableBuildingWindAnimatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002239 File Offset: 0x00000439
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockableBuildingWindAnimatorSpec left, BlockableBuildingWindAnimatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000224D File Offset: 0x0000044D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002255 File Offset: 0x00000455
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockableBuildingWindAnimatorSpec);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002263 File Offset: 0x00000463
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000226C File Offset: 0x0000046C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockableBuildingWindAnimatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002283 File Offset: 0x00000483
		[CompilerGenerated]
		protected BlockableBuildingWindAnimatorSpec(BlockableBuildingWindAnimatorSpec original) : base(original)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000228C File Offset: 0x0000048C
		public BlockableBuildingWindAnimatorSpec()
		{
		}
	}
}
