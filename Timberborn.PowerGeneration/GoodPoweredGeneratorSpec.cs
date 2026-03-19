using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class GoodPoweredGeneratorSpec : ComponentSpec, IEquatable<GoodPoweredGeneratorSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000236C File Offset: 0x0000056C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GoodPoweredGeneratorSpec);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002378 File Offset: 0x00000578
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodPoweredGeneratorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000021F4 File Offset: 0x000003F4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023C4 File Offset: 0x000005C4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodPoweredGeneratorSpec left, GoodPoweredGeneratorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023D0 File Offset: 0x000005D0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodPoweredGeneratorSpec left, GoodPoweredGeneratorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000221D File Offset: 0x0000041D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023E4 File Offset: 0x000005E4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodPoweredGeneratorSpec);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002233 File Offset: 0x00000433
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000223C File Offset: 0x0000043C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodPoweredGeneratorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002253 File Offset: 0x00000453
		[CompilerGenerated]
		protected GoodPoweredGeneratorSpec(GoodPoweredGeneratorSpec original) : base(original)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000225C File Offset: 0x0000045C
		public GoodPoweredGeneratorSpec()
		{
		}
	}
}
