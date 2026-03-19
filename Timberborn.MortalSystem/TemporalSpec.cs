using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MortalSystem
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	public class TemporalSpec : ComponentSpec, IEquatable<TemporalSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002FA3 File Offset: 0x000011A3
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TemporalSpec);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002FB0 File Offset: 0x000011B0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TemporalSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002DE4 File Offset: 0x00000FE4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002FFC File Offset: 0x000011FC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TemporalSpec left, TemporalSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003008 File Offset: 0x00001208
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TemporalSpec left, TemporalSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002E0D File Offset: 0x0000100D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000301C File Offset: 0x0000121C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TemporalSpec);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002642 File Offset: 0x00000842
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002E23 File Offset: 0x00001023
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TemporalSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002E3A File Offset: 0x0000103A
		[CompilerGenerated]
		protected TemporalSpec(TemporalSpec original) : base(original)
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002691 File Offset: 0x00000891
		public TemporalSpec()
		{
		}
	}
}
