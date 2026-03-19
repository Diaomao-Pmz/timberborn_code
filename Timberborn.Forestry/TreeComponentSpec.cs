using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Forestry
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class TreeComponentSpec : ComponentSpec, IEquatable<TreeComponentSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000028CD File Offset: 0x00000ACD
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TreeComponentSpec);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000028DC File Offset: 0x00000ADC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TreeComponentSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002158 File Offset: 0x00000358
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002928 File Offset: 0x00000B28
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TreeComponentSpec left, TreeComponentSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002934 File Offset: 0x00000B34
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TreeComponentSpec left, TreeComponentSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002181 File Offset: 0x00000381
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002948 File Offset: 0x00000B48
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TreeComponentSpec);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002197 File Offset: 0x00000397
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000021A0 File Offset: 0x000003A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TreeComponentSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000021B7 File Offset: 0x000003B7
		[CompilerGenerated]
		protected TreeComponentSpec(TreeComponentSpec original) : base(original)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000021C0 File Offset: 0x000003C0
		public TreeComponentSpec()
		{
		}
	}
}
