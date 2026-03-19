using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Terraforming
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public class DrillScrewRotatorSpec : ComponentSpec, IEquatable<DrillScrewRotatorSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002D5F File Offset: 0x00000F5F
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DrillScrewRotatorSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002D6B File Offset: 0x00000F6B
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002D73 File Offset: 0x00000F73
		[Serialize]
		public float MinimumRotationSpeed { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002D7C File Offset: 0x00000F7C
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002D84 File Offset: 0x00000F84
		[Serialize]
		public float RotationSpeedPerWorker { get; set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00002D90 File Offset: 0x00000F90
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DrillScrewRotatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DDC File Offset: 0x00000FDC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinimumRotationSpeed = ");
			builder.Append(this.MinimumRotationSpeed.ToString());
			builder.Append(", RotationSpeedPerWorker = ");
			builder.Append(this.RotationSpeedPerWorker.ToString());
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E4D File Offset: 0x0000104D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DrillScrewRotatorSpec left, DrillScrewRotatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E59 File Offset: 0x00001059
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DrillScrewRotatorSpec left, DrillScrewRotatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E6D File Offset: 0x0000106D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinimumRotationSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RotationSpeedPerWorker>k__BackingField);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002EA3 File Offset: 0x000010A3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DrillScrewRotatorSpec);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000262F File Offset: 0x0000082F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002EB4 File Offset: 0x000010B4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DrillScrewRotatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MinimumRotationSpeed>k__BackingField, other.<MinimumRotationSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RotationSpeedPerWorker>k__BackingField, other.<RotationSpeedPerWorker>k__BackingField));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F08 File Offset: 0x00001108
		[CompilerGenerated]
		protected DrillScrewRotatorSpec(DrillScrewRotatorSpec original) : base(original)
		{
			this.MinimumRotationSpeed = original.<MinimumRotationSpeed>k__BackingField;
			this.RotationSpeedPerWorker = original.<RotationSpeedPerWorker>k__BackingField;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000026AD File Offset: 0x000008AD
		public DrillScrewRotatorSpec()
		{
		}
	}
}
