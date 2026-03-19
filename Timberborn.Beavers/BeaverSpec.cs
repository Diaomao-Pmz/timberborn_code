using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Beavers
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class BeaverSpec : ComponentSpec, IEquatable<BeaverSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028E8 File Offset: 0x00000AE8
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BeaverSpec);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000028F4 File Offset: 0x00000AF4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BeaverSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002158 File Offset: 0x00000358
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002940 File Offset: 0x00000B40
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BeaverSpec left, BeaverSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000294C File Offset: 0x00000B4C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BeaverSpec left, BeaverSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002181 File Offset: 0x00000381
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002960 File Offset: 0x00000B60
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BeaverSpec);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002197 File Offset: 0x00000397
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000021A0 File Offset: 0x000003A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BeaverSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000021B7 File Offset: 0x000003B7
		[CompilerGenerated]
		protected BeaverSpec(BeaverSpec original) : base(original)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000021C0 File Offset: 0x000003C0
		public BeaverSpec()
		{
		}
	}
}
