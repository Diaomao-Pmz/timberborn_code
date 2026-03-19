using System;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using Timberborn.BlueprintSystem;

namespace Timberborn.ForestryUI
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	[UsedImplicitly]
	public class TreeCuttingToolGroupSpec : ComponentSpec, IEquatable<TreeCuttingToolGroupSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000031B1 File Offset: 0x000013B1
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TreeCuttingToolGroupSpec);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000031C0 File Offset: 0x000013C0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TreeCuttingToolGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000320C File Offset: 0x0000140C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003215 File Offset: 0x00001415
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TreeCuttingToolGroupSpec left, TreeCuttingToolGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003221 File Offset: 0x00001421
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TreeCuttingToolGroupSpec left, TreeCuttingToolGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003235 File Offset: 0x00001435
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000323D File Offset: 0x0000143D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TreeCuttingToolGroupSpec);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030E1 File Offset: 0x000012E1
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000324B File Offset: 0x0000144B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TreeCuttingToolGroupSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003262 File Offset: 0x00001462
		[CompilerGenerated]
		protected TreeCuttingToolGroupSpec(TreeCuttingToolGroupSpec original) : base(original)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031A9 File Offset: 0x000013A9
		public TreeCuttingToolGroupSpec()
		{
		}
	}
}
