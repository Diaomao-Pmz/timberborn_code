using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Hauling
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public class HaulingCenterSpec : ComponentSpec, IEquatable<HaulingCenterSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027B9 File Offset: 0x000009B9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(HaulingCenterSpec);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027C8 File Offset: 0x000009C8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("HaulingCenterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002814 File Offset: 0x00000A14
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000281D File Offset: 0x00000A1D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(HaulingCenterSpec left, HaulingCenterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002829 File Offset: 0x00000A29
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(HaulingCenterSpec left, HaulingCenterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000283D File Offset: 0x00000A3D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002845 File Offset: 0x00000A45
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HaulingCenterSpec);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002853 File Offset: 0x00000A53
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000285C File Offset: 0x00000A5C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(HaulingCenterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002873 File Offset: 0x00000A73
		[CompilerGenerated]
		protected HaulingCenterSpec(HaulingCenterSpec original) : base(original)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000287C File Offset: 0x00000A7C
		public HaulingCenterSpec()
		{
		}
	}
}
