using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class TubeSpec : ComponentSpec, IEquatable<TubeSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002C8C File Offset: 0x00000E8C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TubeSpec);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C98 File Offset: 0x00000E98
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TubeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002CE4 File Offset: 0x00000EE4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CED File Offset: 0x00000EED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TubeSpec left, TubeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CF9 File Offset: 0x00000EF9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TubeSpec left, TubeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D0D File Offset: 0x00000F0D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D15 File Offset: 0x00000F15
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TubeSpec);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002A3E File Offset: 0x00000C3E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D23 File Offset: 0x00000F23
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TubeSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002D3A File Offset: 0x00000F3A
		[CompilerGenerated]
		protected TubeSpec(TubeSpec original) : base(original)
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002A8D File Offset: 0x00000C8D
		public TubeSpec()
		{
		}
	}
}
