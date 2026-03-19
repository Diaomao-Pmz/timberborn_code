using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class UndergroundWaterSourceSpec : ComponentSpec, IEquatable<UndergroundWaterSourceSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002CC1 File Offset: 0x00000EC1
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(UndergroundWaterSourceSpec);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002CD0 File Offset: 0x00000ED0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UndergroundWaterSourceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000022B8 File Offset: 0x000004B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002D1C File Offset: 0x00000F1C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UndergroundWaterSourceSpec left, UndergroundWaterSourceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002D28 File Offset: 0x00000F28
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UndergroundWaterSourceSpec left, UndergroundWaterSourceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000022E1 File Offset: 0x000004E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002D3C File Offset: 0x00000F3C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UndergroundWaterSourceSpec);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002300 File Offset: 0x00000500
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UndergroundWaterSourceSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002317 File Offset: 0x00000517
		[CompilerGenerated]
		protected UndergroundWaterSourceSpec(UndergroundWaterSourceSpec original) : base(original)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002320 File Offset: 0x00000520
		public UndergroundWaterSourceSpec()
		{
		}
	}
}
