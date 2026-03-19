using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Beavers
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	public class ChildSpec : ComponentSpec, IEquatable<ChildSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002CCE File Offset: 0x00000ECE
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ChildSpec);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002CDC File Offset: 0x00000EDC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ChildSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002158 File Offset: 0x00000358
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002D28 File Offset: 0x00000F28
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ChildSpec left, ChildSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002D34 File Offset: 0x00000F34
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ChildSpec left, ChildSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002181 File Offset: 0x00000381
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D48 File Offset: 0x00000F48
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ChildSpec);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002197 File Offset: 0x00000397
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000021A0 File Offset: 0x000003A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ChildSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000021B7 File Offset: 0x000003B7
		[CompilerGenerated]
		protected ChildSpec(ChildSpec original) : base(original)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000021C0 File Offset: 0x000003C0
		public ChildSpec()
		{
		}
	}
}
