using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	public class WalkerPoweredGeneratorSpec : ComponentSpec, IEquatable<WalkerPoweredGeneratorSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000028ED File Offset: 0x00000AED
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WalkerPoweredGeneratorSpec);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000028FC File Offset: 0x00000AFC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WalkerPoweredGeneratorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000021F4 File Offset: 0x000003F4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002948 File Offset: 0x00000B48
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WalkerPoweredGeneratorSpec left, WalkerPoweredGeneratorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002954 File Offset: 0x00000B54
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WalkerPoweredGeneratorSpec left, WalkerPoweredGeneratorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000221D File Offset: 0x0000041D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002968 File Offset: 0x00000B68
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WalkerPoweredGeneratorSpec);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002233 File Offset: 0x00000433
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000223C File Offset: 0x0000043C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WalkerPoweredGeneratorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002253 File Offset: 0x00000453
		[CompilerGenerated]
		protected WalkerPoweredGeneratorSpec(WalkerPoweredGeneratorSpec original) : base(original)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000225C File Offset: 0x0000045C
		public WalkerPoweredGeneratorSpec()
		{
		}
	}
}
