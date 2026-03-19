using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterMoverSpec : ComponentSpec, IEquatable<WaterMoverSpec>
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00007F28 File Offset: 0x00006128
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterMoverSpec);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00007F34 File Offset: 0x00006134
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00007F3C File Offset: 0x0000613C
		[Serialize]
		public float WaterPerSecond { get; set; }

		// Token: 0x060002A7 RID: 679 RVA: 0x00007F48 File Offset: 0x00006148
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterMoverSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007F94 File Offset: 0x00006194
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WaterPerSecond = ");
			builder.Append(this.WaterPerSecond.ToString());
			return true;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007FDE File Offset: 0x000061DE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterMoverSpec left, WaterMoverSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007FEA File Offset: 0x000061EA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterMoverSpec left, WaterMoverSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007FFE File Offset: 0x000061FE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<WaterPerSecond>k__BackingField);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000801D File Offset: 0x0000621D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterMoverSpec);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000802B File Offset: 0x0000622B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterMoverSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<WaterPerSecond>k__BackingField, other.<WaterPerSecond>k__BackingField));
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000805C File Offset: 0x0000625C
		[CompilerGenerated]
		protected WaterMoverSpec(WaterMoverSpec original) : base(original)
		{
			this.WaterPerSecond = original.<WaterPerSecond>k__BackingField;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00002CBC File Offset: 0x00000EBC
		public WaterMoverSpec()
		{
		}
	}
}
