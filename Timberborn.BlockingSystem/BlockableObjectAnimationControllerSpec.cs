using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockingSystem
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockableObjectAnimationControllerSpec : ComponentSpec, IEquatable<BlockableObjectAnimationControllerSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000235F File Offset: 0x0000055F
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockableObjectAnimationControllerSpec);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000236C File Offset: 0x0000056C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockableObjectAnimationControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023B8 File Offset: 0x000005B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023C1 File Offset: 0x000005C1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockableObjectAnimationControllerSpec left, BlockableObjectAnimationControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023CD File Offset: 0x000005CD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockableObjectAnimationControllerSpec left, BlockableObjectAnimationControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023E1 File Offset: 0x000005E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023E9 File Offset: 0x000005E9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockableObjectAnimationControllerSpec);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023F7 File Offset: 0x000005F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002400 File Offset: 0x00000600
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockableObjectAnimationControllerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002417 File Offset: 0x00000617
		[CompilerGenerated]
		protected BlockableObjectAnimationControllerSpec(BlockableObjectAnimationControllerSpec original) : base(original)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002420 File Offset: 0x00000620
		public BlockableObjectAnimationControllerSpec()
		{
		}
	}
}
