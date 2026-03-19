using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WindSystem
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class WeathervaneSpec : ComponentSpec, IEquatable<WeathervaneSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002294 File Offset: 0x00000494
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WeathervaneSpec);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022A0 File Offset: 0x000004A0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WeathervaneSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002224 File Offset: 0x00000424
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022EC File Offset: 0x000004EC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WeathervaneSpec left, WeathervaneSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022F8 File Offset: 0x000004F8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WeathervaneSpec left, WeathervaneSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000224D File Offset: 0x0000044D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000230C File Offset: 0x0000050C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WeathervaneSpec);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002263 File Offset: 0x00000463
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000226C File Offset: 0x0000046C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WeathervaneSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002283 File Offset: 0x00000483
		[CompilerGenerated]
		protected WeathervaneSpec(WeathervaneSpec original) : base(original)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000228C File Offset: 0x0000048C
		public WeathervaneSpec()
		{
		}
	}
}
