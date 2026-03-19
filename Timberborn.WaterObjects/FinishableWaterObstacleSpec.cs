using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterObjects
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class FinishableWaterObstacleSpec : ComponentSpec, IEquatable<FinishableWaterObstacleSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002464 File Offset: 0x00000664
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FinishableWaterObstacleSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002470 File Offset: 0x00000670
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002478 File Offset: 0x00000678
		[Serialize]
		public float Height { get; set; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002484 File Offset: 0x00000684
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FinishableWaterObstacleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000024D0 File Offset: 0x000006D0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Height = ");
			builder.Append(this.Height.ToString());
			return true;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000251A File Offset: 0x0000071A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FinishableWaterObstacleSpec left, FinishableWaterObstacleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002526 File Offset: 0x00000726
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FinishableWaterObstacleSpec left, FinishableWaterObstacleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000253A File Offset: 0x0000073A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Height>k__BackingField);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002559 File Offset: 0x00000759
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FinishableWaterObstacleSpec);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002567 File Offset: 0x00000767
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FinishableWaterObstacleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<Height>k__BackingField, other.<Height>k__BackingField));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002598 File Offset: 0x00000798
		[CompilerGenerated]
		protected FinishableWaterObstacleSpec(FinishableWaterObstacleSpec original) : base(original)
		{
			this.Height = original.<Height>k__BackingField;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002290 File Offset: 0x00000490
		public FinishableWaterObstacleSpec()
		{
		}
	}
}
