using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PathSystem
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	public class StraightStairsSpec : ComponentSpec, IEquatable<StraightStairsSpec>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003E6C File Offset: 0x0000206C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(StraightStairsSpec);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003E78 File Offset: 0x00002078
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StraightStairsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003A28 File Offset: 0x00001C28
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003EC4 File Offset: 0x000020C4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StraightStairsSpec left, StraightStairsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003ED0 File Offset: 0x000020D0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StraightStairsSpec left, StraightStairsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003A51 File Offset: 0x00001C51
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003EE4 File Offset: 0x000020E4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StraightStairsSpec);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002983 File Offset: 0x00000B83
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003A67 File Offset: 0x00001C67
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StraightStairsSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003A7E File Offset: 0x00001C7E
		[CompilerGenerated]
		protected StraightStairsSpec(StraightStairsSpec original) : base(original)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public StraightStairsSpec()
		{
		}
	}
}
