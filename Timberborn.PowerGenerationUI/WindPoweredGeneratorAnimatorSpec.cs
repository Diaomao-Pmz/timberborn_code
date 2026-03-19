using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	public class WindPoweredGeneratorAnimatorSpec : ComponentSpec, IEquatable<WindPoweredGeneratorAnimatorSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002C40 File Offset: 0x00000E40
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WindPoweredGeneratorAnimatorSpec);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C4C File Offset: 0x00000E4C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WindPoweredGeneratorAnimatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000239C File Offset: 0x0000059C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C98 File Offset: 0x00000E98
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WindPoweredGeneratorAnimatorSpec left, WindPoweredGeneratorAnimatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CA4 File Offset: 0x00000EA4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WindPoweredGeneratorAnimatorSpec left, WindPoweredGeneratorAnimatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000023C5 File Offset: 0x000005C5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002CB8 File Offset: 0x00000EB8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WindPoweredGeneratorAnimatorSpec);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000023DB File Offset: 0x000005DB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000023E4 File Offset: 0x000005E4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WindPoweredGeneratorAnimatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000023FB File Offset: 0x000005FB
		[CompilerGenerated]
		protected WindPoweredGeneratorAnimatorSpec(WindPoweredGeneratorAnimatorSpec original) : base(original)
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002404 File Offset: 0x00000604
		public WindPoweredGeneratorAnimatorSpec()
		{
		}
	}
}
