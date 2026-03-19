using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Bots
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class BotSpec : ComponentSpec, IEquatable<BotSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000254E File Offset: 0x0000074E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BotSpec);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000255C File Offset: 0x0000075C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BotSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025A8 File Offset: 0x000007A8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025B1 File Offset: 0x000007B1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BotSpec left, BotSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025BD File Offset: 0x000007BD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BotSpec left, BotSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025D1 File Offset: 0x000007D1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025D9 File Offset: 0x000007D9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BotSpec);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000224A File Offset: 0x0000044A
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025E7 File Offset: 0x000007E7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BotSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025FE File Offset: 0x000007FE
		[CompilerGenerated]
		protected BotSpec(BotSpec original) : base(original)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002299 File Offset: 0x00000499
		public BotSpec()
		{
		}
	}
}
