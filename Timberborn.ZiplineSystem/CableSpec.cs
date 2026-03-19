using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class CableSpec : ComponentSpec, IEquatable<CableSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002591 File Offset: 0x00000791
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CableSpec);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025A0 File Offset: 0x000007A0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002324 File Offset: 0x00000524
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025EC File Offset: 0x000007EC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CableSpec left, CableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025F8 File Offset: 0x000007F8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CableSpec left, CableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000234D File Offset: 0x0000054D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000260C File Offset: 0x0000080C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CableSpec);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000236C File Offset: 0x0000056C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CableSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002383 File Offset: 0x00000583
		[CompilerGenerated]
		protected CableSpec(CableSpec original) : base(original)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000238C File Offset: 0x0000058C
		public CableSpec()
		{
		}
	}
}
