using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Fields
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class FarmHouseSpec : ComponentSpec, IEquatable<FarmHouseSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002407 File Offset: 0x00000607
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FarmHouseSpec);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002414 File Offset: 0x00000614
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FarmHouseSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002254 File Offset: 0x00000454
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002460 File Offset: 0x00000660
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FarmHouseSpec left, FarmHouseSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000246C File Offset: 0x0000066C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FarmHouseSpec left, FarmHouseSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000227D File Offset: 0x0000047D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002480 File Offset: 0x00000680
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FarmHouseSpec);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002293 File Offset: 0x00000493
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000229C File Offset: 0x0000049C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FarmHouseSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000022B3 File Offset: 0x000004B3
		[CompilerGenerated]
		protected FarmHouseSpec(FarmHouseSpec original) : base(original)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000022BC File Offset: 0x000004BC
		public FarmHouseSpec()
		{
		}
	}
}
