using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000025 RID: 37
	[NullableContext(1)]
	[Nullable(0)]
	public class PrimaryInputBindingSpec : InputBindingSpec, IEquatable<PrimaryInputBindingSpec>
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004618 File Offset: 0x00002818
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PrimaryInputBindingSpec);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004624 File Offset: 0x00002824
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PrimaryInputBindingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004670 File Offset: 0x00002870
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004679 File Offset: 0x00002879
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PrimaryInputBindingSpec left, PrimaryInputBindingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004685 File Offset: 0x00002885
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PrimaryInputBindingSpec left, PrimaryInputBindingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004699 File Offset: 0x00002899
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000046A1 File Offset: 0x000028A1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PrimaryInputBindingSpec);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000024D7 File Offset: 0x000006D7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(InputBindingSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000046AF File Offset: 0x000028AF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PrimaryInputBindingSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000046C6 File Offset: 0x000028C6
		[CompilerGenerated]
		protected PrimaryInputBindingSpec(PrimaryInputBindingSpec original) : base(original)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000046CF File Offset: 0x000028CF
		public PrimaryInputBindingSpec()
		{
		}
	}
}
