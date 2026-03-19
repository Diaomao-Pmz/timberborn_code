using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BotUpkeep
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class BotManufactorySpec : ComponentSpec, IEquatable<BotManufactorySpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000028E8 File Offset: 0x00000AE8
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BotManufactorySpec);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028F4 File Offset: 0x00000AF4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BotManufactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002940 File Offset: 0x00000B40
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002949 File Offset: 0x00000B49
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BotManufactorySpec left, BotManufactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002955 File Offset: 0x00000B55
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BotManufactorySpec left, BotManufactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002969 File Offset: 0x00000B69
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002971 File Offset: 0x00000B71
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BotManufactorySpec);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027BF File Offset: 0x000009BF
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000297F File Offset: 0x00000B7F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BotManufactorySpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002996 File Offset: 0x00000B96
		[CompilerGenerated]
		protected BotManufactorySpec(BotManufactorySpec original) : base(original)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028E0 File Offset: 0x00000AE0
		public BotManufactorySpec()
		{
		}
	}
}
