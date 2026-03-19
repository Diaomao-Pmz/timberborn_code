using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class DemolishableFromTopSpec : ComponentSpec, IEquatable<DemolishableFromTopSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000250E File Offset: 0x0000070E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DemolishableFromTopSpec);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000251C File Offset: 0x0000071C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolishableFromTopSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002568 File Offset: 0x00000768
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002571 File Offset: 0x00000771
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolishableFromTopSpec left, DemolishableFromTopSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000257D File Offset: 0x0000077D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolishableFromTopSpec left, DemolishableFromTopSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002591 File Offset: 0x00000791
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002599 File Offset: 0x00000799
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolishableFromTopSpec);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025A7 File Offset: 0x000007A7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025B0 File Offset: 0x000007B0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolishableFromTopSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025C7 File Offset: 0x000007C7
		[CompilerGenerated]
		protected DemolishableFromTopSpec(DemolishableFromTopSpec original) : base(original)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025D0 File Offset: 0x000007D0
		public DemolishableFromTopSpec()
		{
		}
	}
}
