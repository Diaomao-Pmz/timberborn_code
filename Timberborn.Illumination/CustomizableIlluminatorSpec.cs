using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Illumination
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class CustomizableIlluminatorSpec : ComponentSpec, IEquatable<CustomizableIlluminatorSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000268D File Offset: 0x0000088D
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CustomizableIlluminatorSpec);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000269C File Offset: 0x0000089C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CustomizableIlluminatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002228 File Offset: 0x00000428
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026E8 File Offset: 0x000008E8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CustomizableIlluminatorSpec left, CustomizableIlluminatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026F4 File Offset: 0x000008F4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CustomizableIlluminatorSpec left, CustomizableIlluminatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002251 File Offset: 0x00000451
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002708 File Offset: 0x00000908
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CustomizableIlluminatorSpec);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002270 File Offset: 0x00000470
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CustomizableIlluminatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002287 File Offset: 0x00000487
		[CompilerGenerated]
		protected CustomizableIlluminatorSpec(CustomizableIlluminatorSpec original) : base(original)
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002290 File Offset: 0x00000490
		public CustomizableIlluminatorSpec()
		{
		}
	}
}
