using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class DistrictBuildingIlluminatorSpec : ComponentSpec, IEquatable<DistrictBuildingIlluminatorSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003032 File Offset: 0x00001232
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DistrictBuildingIlluminatorSpec);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003040 File Offset: 0x00001240
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistrictBuildingIlluminatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000308C File Offset: 0x0000128C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003095 File Offset: 0x00001295
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DistrictBuildingIlluminatorSpec left, DistrictBuildingIlluminatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000030A1 File Offset: 0x000012A1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DistrictBuildingIlluminatorSpec left, DistrictBuildingIlluminatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000030B5 File Offset: 0x000012B5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030BD File Offset: 0x000012BD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DistrictBuildingIlluminatorSpec);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030CB File Offset: 0x000012CB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030D4 File Offset: 0x000012D4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DistrictBuildingIlluminatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000030EB File Offset: 0x000012EB
		[CompilerGenerated]
		protected DistrictBuildingIlluminatorSpec(DistrictBuildingIlluminatorSpec original) : base(original)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000030F4 File Offset: 0x000012F4
		public DistrictBuildingIlluminatorSpec()
		{
		}
	}
}
