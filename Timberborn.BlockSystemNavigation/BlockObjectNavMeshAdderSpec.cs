using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockObjectNavMeshAdderSpec : ComponentSpec, IEquatable<BlockObjectNavMeshAdderSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021DA File Offset: 0x000003DA
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectNavMeshAdderSpec);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021E8 File Offset: 0x000003E8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectNavMeshAdderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002234 File Offset: 0x00000434
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000223D File Offset: 0x0000043D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectNavMeshAdderSpec left, BlockObjectNavMeshAdderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002249 File Offset: 0x00000449
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectNavMeshAdderSpec left, BlockObjectNavMeshAdderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000225D File Offset: 0x0000045D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002265 File Offset: 0x00000465
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectNavMeshAdderSpec);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002273 File Offset: 0x00000473
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000227C File Offset: 0x0000047C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectNavMeshAdderSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002293 File Offset: 0x00000493
		[CompilerGenerated]
		protected BlockObjectNavMeshAdderSpec(BlockObjectNavMeshAdderSpec original) : base(original)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000229C File Offset: 0x0000049C
		public BlockObjectNavMeshAdderSpec()
		{
		}
	}
}
