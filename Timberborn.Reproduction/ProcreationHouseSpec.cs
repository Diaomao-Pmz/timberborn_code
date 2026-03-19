using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Reproduction
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class ProcreationHouseSpec : ComponentSpec, IEquatable<ProcreationHouseSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002FB8 File Offset: 0x000011B8
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ProcreationHouseSpec);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FC4 File Offset: 0x000011C4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ProcreationHouseSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003010 File Offset: 0x00001210
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003019 File Offset: 0x00001219
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ProcreationHouseSpec left, ProcreationHouseSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003025 File Offset: 0x00001225
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ProcreationHouseSpec left, ProcreationHouseSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003039 File Offset: 0x00001239
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003041 File Offset: 0x00001241
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ProcreationHouseSpec);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000029BF File Offset: 0x00000BBF
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000304F File Offset: 0x0000124F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ProcreationHouseSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003066 File Offset: 0x00001266
		[CompilerGenerated]
		protected ProcreationHouseSpec(ProcreationHouseSpec original) : base(original)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public ProcreationHouseSpec()
		{
		}
	}
}
