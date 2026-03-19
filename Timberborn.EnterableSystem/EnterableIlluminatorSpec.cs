using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.EnterableSystem
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class EnterableIlluminatorSpec : ComponentSpec, IEquatable<EnterableIlluminatorSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002856 File Offset: 0x00000A56
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(EnterableIlluminatorSpec);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002864 File Offset: 0x00000A64
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EnterableIlluminatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028B0 File Offset: 0x00000AB0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028B9 File Offset: 0x00000AB9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EnterableIlluminatorSpec left, EnterableIlluminatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028C5 File Offset: 0x00000AC5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EnterableIlluminatorSpec left, EnterableIlluminatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028D9 File Offset: 0x00000AD9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028E1 File Offset: 0x00000AE1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EnterableIlluminatorSpec);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000028EF File Offset: 0x00000AEF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EnterableIlluminatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002906 File Offset: 0x00000B06
		[CompilerGenerated]
		protected EnterableIlluminatorSpec(EnterableIlluminatorSpec original) : base(original)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000279E File Offset: 0x0000099E
		public EnterableIlluminatorSpec()
		{
		}
	}
}
