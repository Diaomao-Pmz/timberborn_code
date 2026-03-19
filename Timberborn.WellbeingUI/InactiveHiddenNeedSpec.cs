using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class InactiveHiddenNeedSpec : ComponentSpec, IEquatable<InactiveHiddenNeedSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002747 File Offset: 0x00000947
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(InactiveHiddenNeedSpec);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002754 File Offset: 0x00000954
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("InactiveHiddenNeedSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027A0 File Offset: 0x000009A0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027A9 File Offset: 0x000009A9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(InactiveHiddenNeedSpec left, InactiveHiddenNeedSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027B5 File Offset: 0x000009B5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(InactiveHiddenNeedSpec left, InactiveHiddenNeedSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027C9 File Offset: 0x000009C9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027D1 File Offset: 0x000009D1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InactiveHiddenNeedSpec);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027DF File Offset: 0x000009DF
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027E8 File Offset: 0x000009E8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(InactiveHiddenNeedSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027FF File Offset: 0x000009FF
		[CompilerGenerated]
		protected InactiveHiddenNeedSpec(InactiveHiddenNeedSpec original) : base(original)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002808 File Offset: 0x00000A08
		public InactiveHiddenNeedSpec()
		{
		}
	}
}
