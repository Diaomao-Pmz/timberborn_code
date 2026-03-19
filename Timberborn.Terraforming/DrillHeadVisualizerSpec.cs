using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Terraforming
{
	// Token: 0x02000009 RID: 9
	public class DrillHeadVisualizerSpec : ComponentSpec, IEquatable<DrillHeadVisualizerSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024EE File Offset: 0x000006EE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DrillHeadVisualizerSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024FA File Offset: 0x000006FA
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002502 File Offset: 0x00000702
		[Serialize]
		public string HeadTransformName { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000250B File Offset: 0x0000070B
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002513 File Offset: 0x00000713
		[Serialize]
		public float HeadOffset { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x0000251C File Offset: 0x0000071C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DrillHeadVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002568 File Offset: 0x00000768
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("HeadTransformName = ");
			builder.Append(this.HeadTransformName);
			builder.Append(", HeadOffset = ");
			builder.Append(this.HeadOffset.ToString());
			return true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025CB File Offset: 0x000007CB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DrillHeadVisualizerSpec left, DrillHeadVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025D7 File Offset: 0x000007D7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DrillHeadVisualizerSpec left, DrillHeadVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025EB File Offset: 0x000007EB
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HeadTransformName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<HeadOffset>k__BackingField);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002621 File Offset: 0x00000821
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DrillHeadVisualizerSpec);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000262F File Offset: 0x0000082F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002638 File Offset: 0x00000838
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DrillHeadVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<HeadTransformName>k__BackingField, other.<HeadTransformName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<HeadOffset>k__BackingField, other.<HeadOffset>k__BackingField));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000268C File Offset: 0x0000088C
		[CompilerGenerated]
		protected DrillHeadVisualizerSpec([Nullable(1)] DrillHeadVisualizerSpec original) : base(original)
		{
			this.HeadTransformName = original.<HeadTransformName>k__BackingField;
			this.HeadOffset = original.<HeadOffset>k__BackingField;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026AD File Offset: 0x000008AD
		public DrillHeadVisualizerSpec()
		{
		}
	}
}
