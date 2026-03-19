using System;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using Timberborn.BlueprintSystem;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	[UsedImplicitly]
	public class BuilderPriorityToolGroupSpec : ComponentSpec, IEquatable<BuilderPriorityToolGroupSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002ADD File Offset: 0x00000CDD
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BuilderPriorityToolGroupSpec);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AEC File Offset: 0x00000CEC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuilderPriorityToolGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B38 File Offset: 0x00000D38
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B41 File Offset: 0x00000D41
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuilderPriorityToolGroupSpec left, BuilderPriorityToolGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B4D File Offset: 0x00000D4D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuilderPriorityToolGroupSpec left, BuilderPriorityToolGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B61 File Offset: 0x00000D61
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B69 File Offset: 0x00000D69
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuilderPriorityToolGroupSpec);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B77 File Offset: 0x00000D77
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B80 File Offset: 0x00000D80
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuilderPriorityToolGroupSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B97 File Offset: 0x00000D97
		[CompilerGenerated]
		protected BuilderPriorityToolGroupSpec(BuilderPriorityToolGroupSpec original) : base(original)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public BuilderPriorityToolGroupSpec()
		{
		}
	}
}
