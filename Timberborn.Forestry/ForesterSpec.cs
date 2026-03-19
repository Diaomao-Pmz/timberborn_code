using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Forestry
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class ForesterSpec : ComponentSpec, IEquatable<ForesterSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022B8 File Offset: 0x000004B8
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ForesterSpec);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022C4 File Offset: 0x000004C4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ForesterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002158 File Offset: 0x00000358
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002310 File Offset: 0x00000510
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ForesterSpec left, ForesterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000231C File Offset: 0x0000051C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ForesterSpec left, ForesterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002181 File Offset: 0x00000381
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002330 File Offset: 0x00000530
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ForesterSpec);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002197 File Offset: 0x00000397
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000021A0 File Offset: 0x000003A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ForesterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000021B7 File Offset: 0x000003B7
		[CompilerGenerated]
		protected ForesterSpec(ForesterSpec original) : base(original)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000021C0 File Offset: 0x000003C0
		public ForesterSpec()
		{
		}
	}
}
