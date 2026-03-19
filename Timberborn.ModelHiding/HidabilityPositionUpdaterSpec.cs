using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ModelHiding
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class HidabilityPositionUpdaterSpec : ComponentSpec, IEquatable<HidabilityPositionUpdaterSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002432 File Offset: 0x00000632
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(HidabilityPositionUpdaterSpec);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002440 File Offset: 0x00000640
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("HidabilityPositionUpdaterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000248C File Offset: 0x0000068C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002495 File Offset: 0x00000695
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(HidabilityPositionUpdaterSpec left, HidabilityPositionUpdaterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024A1 File Offset: 0x000006A1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(HidabilityPositionUpdaterSpec left, HidabilityPositionUpdaterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024B5 File Offset: 0x000006B5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024BD File Offset: 0x000006BD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HidabilityPositionUpdaterSpec);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024CB File Offset: 0x000006CB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024D4 File Offset: 0x000006D4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(HidabilityPositionUpdaterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024EB File Offset: 0x000006EB
		[CompilerGenerated]
		protected HidabilityPositionUpdaterSpec(HidabilityPositionUpdaterSpec original) : base(original)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024F4 File Offset: 0x000006F4
		public HidabilityPositionUpdaterSpec()
		{
		}
	}
}
