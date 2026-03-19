using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterWheelSpec : ComponentSpec, IEquatable<WaterWheelSpec>
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00008615 File Offset: 0x00006815
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterWheelSpec);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00008624 File Offset: 0x00006824
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterWheelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00002F68 File Offset: 0x00001168
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008670 File Offset: 0x00006870
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterWheelSpec left, WaterWheelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000867C File Offset: 0x0000687C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterWheelSpec left, WaterWheelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00002F91 File Offset: 0x00001191
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00008690 File Offset: 0x00006890
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterWheelSpec);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00002FA7 File Offset: 0x000011A7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterWheelSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00002FBE File Offset: 0x000011BE
		[CompilerGenerated]
		protected WaterWheelSpec(WaterWheelSpec original) : base(original)
		{
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00002CBC File Offset: 0x00000EBC
		public WaterWheelSpec()
		{
		}
	}
}
