using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public class PipeIntersectionAllowerSpec : ComponentSpec, IEquatable<PipeIntersectionAllowerSpec>
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003C0A File Offset: 0x00001E0A
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PipeIntersectionAllowerSpec);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003C18 File Offset: 0x00001E18
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PipeIntersectionAllowerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002F68 File Offset: 0x00001168
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003C64 File Offset: 0x00001E64
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PipeIntersectionAllowerSpec left, PipeIntersectionAllowerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003C70 File Offset: 0x00001E70
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PipeIntersectionAllowerSpec left, PipeIntersectionAllowerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002F91 File Offset: 0x00001191
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003C84 File Offset: 0x00001E84
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PipeIntersectionAllowerSpec);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002FA7 File Offset: 0x000011A7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PipeIntersectionAllowerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002FBE File Offset: 0x000011BE
		[CompilerGenerated]
		protected PipeIntersectionAllowerSpec(PipeIntersectionAllowerSpec original) : base(original)
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002CBC File Offset: 0x00000EBC
		public PipeIntersectionAllowerSpec()
		{
		}
	}
}
