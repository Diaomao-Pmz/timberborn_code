using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ScienceSystem
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class ScienceNeedingBuildingSpec : ComponentSpec, IEquatable<ScienceNeedingBuildingSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002662 File Offset: 0x00000862
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ScienceNeedingBuildingSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000266E File Offset: 0x0000086E
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002676 File Offset: 0x00000876
		[Serialize]
		public int ScienceUsedPerHour { get; set; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002680 File Offset: 0x00000880
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ScienceNeedingBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026CC File Offset: 0x000008CC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ScienceUsedPerHour = ");
			builder.Append(this.ScienceUsedPerHour.ToString());
			return true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002716 File Offset: 0x00000916
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ScienceNeedingBuildingSpec left, ScienceNeedingBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002722 File Offset: 0x00000922
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ScienceNeedingBuildingSpec left, ScienceNeedingBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002736 File Offset: 0x00000936
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ScienceUsedPerHour>k__BackingField);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002755 File Offset: 0x00000955
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ScienceNeedingBuildingSpec);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002763 File Offset: 0x00000963
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000276C File Offset: 0x0000096C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ScienceNeedingBuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<ScienceUsedPerHour>k__BackingField, other.<ScienceUsedPerHour>k__BackingField));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000279D File Offset: 0x0000099D
		[CompilerGenerated]
		protected ScienceNeedingBuildingSpec(ScienceNeedingBuildingSpec original) : base(original)
		{
			this.ScienceUsedPerHour = original.<ScienceUsedPerHour>k__BackingField;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027B2 File Offset: 0x000009B2
		public ScienceNeedingBuildingSpec()
		{
		}
	}
}
