using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Wandering
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class RestPlaceSpec : ComponentSpec, IEquatable<RestPlaceSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022C5 File Offset: 0x000004C5
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RestPlaceSpec);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022D4 File Offset: 0x000004D4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RestPlaceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002320 File Offset: 0x00000520
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002329 File Offset: 0x00000529
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RestPlaceSpec left, RestPlaceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002335 File Offset: 0x00000535
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RestPlaceSpec left, RestPlaceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002349 File Offset: 0x00000549
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002351 File Offset: 0x00000551
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RestPlaceSpec);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000235F File Offset: 0x0000055F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002368 File Offset: 0x00000568
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RestPlaceSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000237F File Offset: 0x0000057F
		[CompilerGenerated]
		protected RestPlaceSpec(RestPlaceSpec original) : base(original)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002388 File Offset: 0x00000588
		public RestPlaceSpec()
		{
		}
	}
}
