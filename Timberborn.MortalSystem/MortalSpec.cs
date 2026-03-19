using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MortalSystem
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class MortalSpec : ComponentSpec, IEquatable<MortalSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002D8A File Offset: 0x00000F8A
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MortalSpec);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D98 File Offset: 0x00000F98
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MortalSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002DE4 File Offset: 0x00000FE4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002DED File Offset: 0x00000FED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MortalSpec left, MortalSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002DF9 File Offset: 0x00000FF9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MortalSpec left, MortalSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E0D File Offset: 0x0000100D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E15 File Offset: 0x00001015
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MortalSpec);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002642 File Offset: 0x00000842
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002E23 File Offset: 0x00001023
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MortalSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E3A File Offset: 0x0000103A
		[CompilerGenerated]
		protected MortalSpec(MortalSpec original) : base(original)
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002691 File Offset: 0x00000891
		public MortalSpec()
		{
		}
	}
}
