using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockObjectWithPathRangeSpec : ComponentSpec, IEquatable<BlockObjectWithPathRangeSpec>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000031D9 File Offset: 0x000013D9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectWithPathRangeSpec);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000031E8 File Offset: 0x000013E8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectWithPathRangeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002234 File Offset: 0x00000434
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003234 File Offset: 0x00001434
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectWithPathRangeSpec left, BlockObjectWithPathRangeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003240 File Offset: 0x00001440
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectWithPathRangeSpec left, BlockObjectWithPathRangeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000225D File Offset: 0x0000045D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003254 File Offset: 0x00001454
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectWithPathRangeSpec);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002273 File Offset: 0x00000473
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000227C File Offset: 0x0000047C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectWithPathRangeSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002293 File Offset: 0x00000493
		[CompilerGenerated]
		protected BlockObjectWithPathRangeSpec(BlockObjectWithPathRangeSpec original) : base(original)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000229C File Offset: 0x0000049C
		public BlockObjectWithPathRangeSpec()
		{
		}
	}
}
