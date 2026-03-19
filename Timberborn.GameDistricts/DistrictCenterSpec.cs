using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	public class DistrictCenterSpec : ComponentSpec, IEquatable<DistrictCenterSpec>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003743 File Offset: 0x00001943
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DistrictCenterSpec);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003750 File Offset: 0x00001950
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistrictCenterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000308C File Offset: 0x0000128C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000379C File Offset: 0x0000199C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DistrictCenterSpec left, DistrictCenterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000037A8 File Offset: 0x000019A8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DistrictCenterSpec left, DistrictCenterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000030B5 File Offset: 0x000012B5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000037BC File Offset: 0x000019BC
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DistrictCenterSpec);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000030CB File Offset: 0x000012CB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000030D4 File Offset: 0x000012D4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DistrictCenterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000030EB File Offset: 0x000012EB
		[CompilerGenerated]
		protected DistrictCenterSpec(DistrictCenterSpec original) : base(original)
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000030F4 File Offset: 0x000012F4
		public DistrictCenterSpec()
		{
		}
	}
}
