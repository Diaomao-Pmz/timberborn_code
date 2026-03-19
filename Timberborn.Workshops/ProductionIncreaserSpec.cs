using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	public class ProductionIncreaserSpec : ComponentSpec, IEquatable<ProductionIncreaserSpec>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003F99 File Offset: 0x00002199
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ProductionIncreaserSpec);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003FA8 File Offset: 0x000021A8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ProductionIncreaserSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003FF4 File Offset: 0x000021F4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003FFD File Offset: 0x000021FD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ProductionIncreaserSpec left, ProductionIncreaserSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004009 File Offset: 0x00002209
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ProductionIncreaserSpec left, ProductionIncreaserSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000401D File Offset: 0x0000221D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004025 File Offset: 0x00002225
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ProductionIncreaserSpec);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003857 File Offset: 0x00001A57
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004033 File Offset: 0x00002233
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ProductionIncreaserSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000404A File Offset: 0x0000224A
		[CompilerGenerated]
		protected ProductionIncreaserSpec(ProductionIncreaserSpec original) : base(original)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000038A6 File Offset: 0x00001AA6
		public ProductionIncreaserSpec()
		{
		}
	}
}
