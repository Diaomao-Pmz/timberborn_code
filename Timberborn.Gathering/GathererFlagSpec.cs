using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Gathering
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class GathererFlagSpec : ComponentSpec, IEquatable<GathererFlagSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002B41 File Offset: 0x00000D41
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GathererFlagSpec);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B50 File Offset: 0x00000D50
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GathererFlagSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B9C File Offset: 0x00000D9C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002BA5 File Offset: 0x00000DA5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GathererFlagSpec left, GathererFlagSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002BB1 File Offset: 0x00000DB1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GathererFlagSpec left, GathererFlagSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BC5 File Offset: 0x00000DC5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002BCD File Offset: 0x00000DCD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GathererFlagSpec);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000270B File Offset: 0x0000090B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BDB File Offset: 0x00000DDB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GathererFlagSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002BF2 File Offset: 0x00000DF2
		[CompilerGenerated]
		protected GathererFlagSpec(GathererFlagSpec original) : base(original)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002789 File Offset: 0x00000989
		public GathererFlagSpec()
		{
		}
	}
}
