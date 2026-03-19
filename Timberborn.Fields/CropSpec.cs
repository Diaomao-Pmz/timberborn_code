using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Fields
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class CropSpec : ComponentSpec, IEquatable<CropSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021FB File Offset: 0x000003FB
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CropSpec);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002208 File Offset: 0x00000408
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CropSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002254 File Offset: 0x00000454
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000225D File Offset: 0x0000045D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CropSpec left, CropSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002269 File Offset: 0x00000469
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CropSpec left, CropSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000227D File Offset: 0x0000047D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002285 File Offset: 0x00000485
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CropSpec);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002293 File Offset: 0x00000493
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000229C File Offset: 0x0000049C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CropSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022B3 File Offset: 0x000004B3
		[CompilerGenerated]
		protected CropSpec(CropSpec original) : base(original)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022BC File Offset: 0x000004BC
		public CropSpec()
		{
		}
	}
}
