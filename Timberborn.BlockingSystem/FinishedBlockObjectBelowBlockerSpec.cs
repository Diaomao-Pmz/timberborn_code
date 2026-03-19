using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockingSystem
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class FinishedBlockObjectBelowBlockerSpec : ComponentSpec, IEquatable<FinishedBlockObjectBelowBlockerSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002AF4 File Offset: 0x00000CF4
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FinishedBlockObjectBelowBlockerSpec);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B00 File Offset: 0x00000D00
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FinishedBlockObjectBelowBlockerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000023B8 File Offset: 0x000005B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B4C File Offset: 0x00000D4C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FinishedBlockObjectBelowBlockerSpec left, FinishedBlockObjectBelowBlockerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B58 File Offset: 0x00000D58
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FinishedBlockObjectBelowBlockerSpec left, FinishedBlockObjectBelowBlockerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000023E1 File Offset: 0x000005E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002B6C File Offset: 0x00000D6C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FinishedBlockObjectBelowBlockerSpec);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000023F7 File Offset: 0x000005F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002400 File Offset: 0x00000600
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FinishedBlockObjectBelowBlockerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002417 File Offset: 0x00000617
		[CompilerGenerated]
		protected FinishedBlockObjectBelowBlockerSpec(FinishedBlockObjectBelowBlockerSpec original) : base(original)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002420 File Offset: 0x00000620
		public FinishedBlockObjectBelowBlockerSpec()
		{
		}
	}
}
