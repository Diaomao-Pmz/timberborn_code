using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MapEditorPlacementRandomizing
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockObjectRandomizablePlacementSpec : ComponentSpec, IEquatable<BlockObjectRandomizablePlacementSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021BD File Offset: 0x000003BD
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectRandomizablePlacementSpec);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021CC File Offset: 0x000003CC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectRandomizablePlacementSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002218 File Offset: 0x00000418
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002221 File Offset: 0x00000421
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectRandomizablePlacementSpec left, BlockObjectRandomizablePlacementSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000222D File Offset: 0x0000042D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectRandomizablePlacementSpec left, BlockObjectRandomizablePlacementSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002241 File Offset: 0x00000441
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002249 File Offset: 0x00000449
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectRandomizablePlacementSpec);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002257 File Offset: 0x00000457
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002260 File Offset: 0x00000460
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectRandomizablePlacementSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002277 File Offset: 0x00000477
		[CompilerGenerated]
		protected BlockObjectRandomizablePlacementSpec(BlockObjectRandomizablePlacementSpec original) : base(original)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002280 File Offset: 0x00000480
		public BlockObjectRandomizablePlacementSpec()
		{
		}
	}
}
