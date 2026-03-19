using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class ConstructionSiteBuildersLimiterSpec : ComponentSpec, IEquatable<ConstructionSiteBuildersLimiterSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000335E File Offset: 0x0000155E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ConstructionSiteBuildersLimiterSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000336A File Offset: 0x0000156A
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003372 File Offset: 0x00001572
		[Serialize]
		public int MaxAllowedBuilders { get; set; }

		// Token: 0x06000073 RID: 115 RVA: 0x0000337C File Offset: 0x0000157C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConstructionSiteBuildersLimiterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033C8 File Offset: 0x000015C8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxAllowedBuilders = ");
			builder.Append(this.MaxAllowedBuilders.ToString());
			return true;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003412 File Offset: 0x00001612
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConstructionSiteBuildersLimiterSpec left, ConstructionSiteBuildersLimiterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000341E File Offset: 0x0000161E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConstructionSiteBuildersLimiterSpec left, ConstructionSiteBuildersLimiterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003432 File Offset: 0x00001632
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxAllowedBuilders>k__BackingField);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003451 File Offset: 0x00001651
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConstructionSiteBuildersLimiterSpec);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000345F File Offset: 0x0000165F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003468 File Offset: 0x00001668
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConstructionSiteBuildersLimiterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxAllowedBuilders>k__BackingField, other.<MaxAllowedBuilders>k__BackingField));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003499 File Offset: 0x00001699
		[CompilerGenerated]
		protected ConstructionSiteBuildersLimiterSpec(ConstructionSiteBuildersLimiterSpec original) : base(original)
		{
			this.MaxAllowedBuilders = original.<MaxAllowedBuilders>k__BackingField;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000034AE File Offset: 0x000016AE
		public ConstructionSiteBuildersLimiterSpec()
		{
			this.MaxAllowedBuilders = 1;
			base..ctor();
		}
	}
}
