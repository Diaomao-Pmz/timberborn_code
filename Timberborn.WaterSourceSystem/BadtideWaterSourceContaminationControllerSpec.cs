using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class BadtideWaterSourceContaminationControllerSpec : ComponentSpec, IEquatable<BadtideWaterSourceContaminationControllerSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000225E File Offset: 0x0000045E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BadtideWaterSourceContaminationControllerSpec);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000226C File Offset: 0x0000046C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BadtideWaterSourceContaminationControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022B8 File Offset: 0x000004B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C1 File Offset: 0x000004C1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BadtideWaterSourceContaminationControllerSpec left, BadtideWaterSourceContaminationControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022CD File Offset: 0x000004CD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BadtideWaterSourceContaminationControllerSpec left, BadtideWaterSourceContaminationControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022E1 File Offset: 0x000004E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022E9 File Offset: 0x000004E9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BadtideWaterSourceContaminationControllerSpec);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002300 File Offset: 0x00000500
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BadtideWaterSourceContaminationControllerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002317 File Offset: 0x00000517
		[CompilerGenerated]
		protected BadtideWaterSourceContaminationControllerSpec(BadtideWaterSourceContaminationControllerSpec original) : base(original)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002320 File Offset: 0x00000520
		public BadtideWaterSourceContaminationControllerSpec()
		{
		}
	}
}
