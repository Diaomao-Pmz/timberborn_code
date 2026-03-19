using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockObstacles
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockOccupierSpec : ComponentSpec, IEquatable<BlockOccupierSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002600 File Offset: 0x00000800
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockOccupierSpec);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000260C File Offset: 0x0000080C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockOccupierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002658 File Offset: 0x00000858
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002661 File Offset: 0x00000861
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockOccupierSpec left, BlockOccupierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000266D File Offset: 0x0000086D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockOccupierSpec left, BlockOccupierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002681 File Offset: 0x00000881
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002689 File Offset: 0x00000889
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockOccupierSpec);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002697 File Offset: 0x00000897
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026A0 File Offset: 0x000008A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockOccupierSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026B7 File Offset: 0x000008B7
		[CompilerGenerated]
		protected BlockOccupierSpec(BlockOccupierSpec original) : base(original)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026C0 File Offset: 0x000008C0
		public BlockOccupierSpec()
		{
		}
	}
}
