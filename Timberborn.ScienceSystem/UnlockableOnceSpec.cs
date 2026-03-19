using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ScienceSystem
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class UnlockableOnceSpec : ComponentSpec, IEquatable<UnlockableOnceSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002930 File Offset: 0x00000B30
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(UnlockableOnceSpec);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000293C File Offset: 0x00000B3C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnlockableOnceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002988 File Offset: 0x00000B88
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002991 File Offset: 0x00000B91
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnlockableOnceSpec left, UnlockableOnceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000299D File Offset: 0x00000B9D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnlockableOnceSpec left, UnlockableOnceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029B1 File Offset: 0x00000BB1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029B9 File Offset: 0x00000BB9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnlockableOnceSpec);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002763 File Offset: 0x00000963
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029C7 File Offset: 0x00000BC7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnlockableOnceSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029DE File Offset: 0x00000BDE
		[CompilerGenerated]
		protected UnlockableOnceSpec(UnlockableOnceSpec original) : base(original)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000027B2 File Offset: 0x000009B2
		public UnlockableOnceSpec()
		{
		}
	}
}
