using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Illumination
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockableIlluminatorSpec : ComponentSpec, IEquatable<BlockableIlluminatorSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021D0 File Offset: 0x000003D0
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockableIlluminatorSpec);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021DC File Offset: 0x000003DC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockableIlluminatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002228 File Offset: 0x00000428
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002231 File Offset: 0x00000431
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockableIlluminatorSpec left, BlockableIlluminatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000223D File Offset: 0x0000043D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockableIlluminatorSpec left, BlockableIlluminatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002251 File Offset: 0x00000451
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002259 File Offset: 0x00000459
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockableIlluminatorSpec);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002270 File Offset: 0x00000470
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockableIlluminatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002287 File Offset: 0x00000487
		[CompilerGenerated]
		protected BlockableIlluminatorSpec(BlockableIlluminatorSpec original) : base(original)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002290 File Offset: 0x00000490
		public BlockableIlluminatorSpec()
		{
		}
	}
}
