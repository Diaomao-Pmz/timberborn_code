using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BuilderHubSystem
{
	// Token: 0x02000007 RID: 7
	[NullableContext(1)]
	[Nullable(0)]
	public class BuilderHubSpec : ComponentSpec, IEquatable<BuilderHubSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BuilderHubSpec);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000210C File Offset: 0x0000030C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuilderHubSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002158 File Offset: 0x00000358
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002161 File Offset: 0x00000361
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuilderHubSpec left, BuilderHubSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000216D File Offset: 0x0000036D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuilderHubSpec left, BuilderHubSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002181 File Offset: 0x00000381
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002189 File Offset: 0x00000389
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuilderHubSpec);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002197 File Offset: 0x00000397
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021A0 File Offset: 0x000003A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuilderHubSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021B7 File Offset: 0x000003B7
		[CompilerGenerated]
		protected BuilderHubSpec(BuilderHubSpec original) : base(original)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021C0 File Offset: 0x000003C0
		public BuilderHubSpec()
		{
		}
	}
}
