using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public class DemolishableStatusIconOffsetterSpec : ComponentSpec, IEquatable<DemolishableStatusIconOffsetterSpec>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000305B File Offset: 0x0000125B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DemolishableStatusIconOffsetterSpec);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003068 File Offset: 0x00001268
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolishableStatusIconOffsetterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002568 File Offset: 0x00000768
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000030B4 File Offset: 0x000012B4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolishableStatusIconOffsetterSpec left, DemolishableStatusIconOffsetterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000030C0 File Offset: 0x000012C0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolishableStatusIconOffsetterSpec left, DemolishableStatusIconOffsetterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002591 File Offset: 0x00000791
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000030D4 File Offset: 0x000012D4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolishableStatusIconOffsetterSpec);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000025A7 File Offset: 0x000007A7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000025B0 File Offset: 0x000007B0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolishableStatusIconOffsetterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000025C7 File Offset: 0x000007C7
		[CompilerGenerated]
		protected DemolishableStatusIconOffsetterSpec(DemolishableStatusIconOffsetterSpec original) : base(original)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000025D0 File Offset: 0x000007D0
		public DemolishableStatusIconOffsetterSpec()
		{
		}
	}
}
