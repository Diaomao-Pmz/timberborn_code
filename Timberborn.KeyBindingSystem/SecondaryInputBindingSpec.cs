using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	public class SecondaryInputBindingSpec : InputBindingSpec, IEquatable<SecondaryInputBindingSpec>
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000046D7 File Offset: 0x000028D7
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SecondaryInputBindingSpec);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000046E4 File Offset: 0x000028E4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SecondaryInputBindingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004670 File Offset: 0x00002870
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004730 File Offset: 0x00002930
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SecondaryInputBindingSpec left, SecondaryInputBindingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000473C File Offset: 0x0000293C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SecondaryInputBindingSpec left, SecondaryInputBindingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004699 File Offset: 0x00002899
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004750 File Offset: 0x00002950
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecondaryInputBindingSpec);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000024D7 File Offset: 0x000006D7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(InputBindingSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000046AF File Offset: 0x000028AF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SecondaryInputBindingSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000046C6 File Offset: 0x000028C6
		[CompilerGenerated]
		protected SecondaryInputBindingSpec(SecondaryInputBindingSpec original) : base(original)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000046CF File Offset: 0x000028CF
		public SecondaryInputBindingSpec()
		{
		}
	}
}
