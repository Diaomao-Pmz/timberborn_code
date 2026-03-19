using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Forestry
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class LumberjackFlagSpec : ComponentSpec, IEquatable<LumberjackFlagSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000024A3 File Offset: 0x000006A3
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(LumberjackFlagSpec);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024B0 File Offset: 0x000006B0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LumberjackFlagSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002158 File Offset: 0x00000358
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024FC File Offset: 0x000006FC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LumberjackFlagSpec left, LumberjackFlagSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002508 File Offset: 0x00000708
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LumberjackFlagSpec left, LumberjackFlagSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002181 File Offset: 0x00000381
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000251C File Offset: 0x0000071C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LumberjackFlagSpec);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002197 File Offset: 0x00000397
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000021A0 File Offset: 0x000003A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LumberjackFlagSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000021B7 File Offset: 0x000003B7
		[CompilerGenerated]
		protected LumberjackFlagSpec(LumberjackFlagSpec original) : base(original)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000021C0 File Offset: 0x000003C0
		public LumberjackFlagSpec()
		{
		}
	}
}
