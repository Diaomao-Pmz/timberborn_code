using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class GoodPoweredGeneratorAnimatorSpec : ComponentSpec, IEquatable<GoodPoweredGeneratorAnimatorSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002342 File Offset: 0x00000542
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GoodPoweredGeneratorAnimatorSpec);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002350 File Offset: 0x00000550
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodPoweredGeneratorAnimatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000239C File Offset: 0x0000059C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023A5 File Offset: 0x000005A5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodPoweredGeneratorAnimatorSpec left, GoodPoweredGeneratorAnimatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B1 File Offset: 0x000005B1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodPoweredGeneratorAnimatorSpec left, GoodPoweredGeneratorAnimatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023C5 File Offset: 0x000005C5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023CD File Offset: 0x000005CD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodPoweredGeneratorAnimatorSpec);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023DB File Offset: 0x000005DB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023E4 File Offset: 0x000005E4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodPoweredGeneratorAnimatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023FB File Offset: 0x000005FB
		[CompilerGenerated]
		protected GoodPoweredGeneratorAnimatorSpec(GoodPoweredGeneratorAnimatorSpec original) : base(original)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002404 File Offset: 0x00000604
		public GoodPoweredGeneratorAnimatorSpec()
		{
		}
	}
}
