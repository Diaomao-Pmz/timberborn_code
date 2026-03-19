using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PathSystem
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	public class SpiralStairsSpec : ComponentSpec, IEquatable<SpiralStairsSpec>
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000039CE File Offset: 0x00001BCE
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SpiralStairsSpec);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000039DC File Offset: 0x00001BDC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SpiralStairsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003A28 File Offset: 0x00001C28
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003A31 File Offset: 0x00001C31
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SpiralStairsSpec left, SpiralStairsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003A3D File Offset: 0x00001C3D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SpiralStairsSpec left, SpiralStairsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003A51 File Offset: 0x00001C51
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003A59 File Offset: 0x00001C59
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SpiralStairsSpec);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002983 File Offset: 0x00000B83
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003A67 File Offset: 0x00001C67
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SpiralStairsSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A7E File Offset: 0x00001C7E
		[CompilerGenerated]
		protected SpiralStairsSpec(SpiralStairsSpec original) : base(original)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public SpiralStairsSpec()
		{
		}
	}
}
