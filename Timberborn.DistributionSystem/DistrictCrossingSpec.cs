using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.DistributionSystem
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class DistrictCrossingSpec : ComponentSpec, IEquatable<DistrictCrossingSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002E47 File Offset: 0x00001047
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DistrictCrossingSpec);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E54 File Offset: 0x00001054
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistrictCrossingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EA0 File Offset: 0x000010A0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EA9 File Offset: 0x000010A9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DistrictCrossingSpec left, DistrictCrossingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EB5 File Offset: 0x000010B5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DistrictCrossingSpec left, DistrictCrossingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EC9 File Offset: 0x000010C9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002ED1 File Offset: 0x000010D1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DistrictCrossingSpec);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002EDF File Offset: 0x000010DF
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002EE8 File Offset: 0x000010E8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DistrictCrossingSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002EFF File Offset: 0x000010FF
		[CompilerGenerated]
		protected DistrictCrossingSpec(DistrictCrossingSpec original) : base(original)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F08 File Offset: 0x00001108
		public DistrictCrossingSpec()
		{
		}
	}
}
