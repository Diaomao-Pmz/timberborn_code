using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	public class MechanicalNodeIlluminatorSpec : ComponentSpec, IEquatable<MechanicalNodeIlluminatorSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003994 File Offset: 0x00001B94
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalNodeIlluminatorSpec);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000039A0 File Offset: 0x00001BA0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalNodeIlluminatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000039EC File Offset: 0x00001BEC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000039F5 File Offset: 0x00001BF5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalNodeIlluminatorSpec left, MechanicalNodeIlluminatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003A01 File Offset: 0x00001C01
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalNodeIlluminatorSpec left, MechanicalNodeIlluminatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003A15 File Offset: 0x00001C15
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003A1D File Offset: 0x00001C1D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalNodeIlluminatorSpec);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003027 File Offset: 0x00001227
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003A2B File Offset: 0x00001C2B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalNodeIlluminatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003A42 File Offset: 0x00001C42
		[CompilerGenerated]
		protected MechanicalNodeIlluminatorSpec(MechanicalNodeIlluminatorSpec original) : base(original)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003076 File Offset: 0x00001276
		public MechanicalNodeIlluminatorSpec()
		{
		}
	}
}
