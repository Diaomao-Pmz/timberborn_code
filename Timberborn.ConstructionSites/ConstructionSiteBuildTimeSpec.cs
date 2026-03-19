using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class ConstructionSiteBuildTimeSpec : ComponentSpec, IEquatable<ConstructionSiteBuildTimeSpec>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003514 File Offset: 0x00001714
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ConstructionSiteBuildTimeSpec);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003520 File Offset: 0x00001720
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003528 File Offset: 0x00001728
		[Serialize]
		public float ConstructionTimeInHours { get; set; }

		// Token: 0x06000084 RID: 132 RVA: 0x00003534 File Offset: 0x00001734
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConstructionSiteBuildTimeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003580 File Offset: 0x00001780
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ConstructionTimeInHours = ");
			builder.Append(this.ConstructionTimeInHours.ToString());
			return true;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000035CA File Offset: 0x000017CA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConstructionSiteBuildTimeSpec left, ConstructionSiteBuildTimeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000035D6 File Offset: 0x000017D6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConstructionSiteBuildTimeSpec left, ConstructionSiteBuildTimeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000035EA File Offset: 0x000017EA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ConstructionTimeInHours>k__BackingField);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003609 File Offset: 0x00001809
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConstructionSiteBuildTimeSpec);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000345F File Offset: 0x0000165F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003617 File Offset: 0x00001817
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConstructionSiteBuildTimeSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<ConstructionTimeInHours>k__BackingField, other.<ConstructionTimeInHours>k__BackingField));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003648 File Offset: 0x00001848
		[CompilerGenerated]
		protected ConstructionSiteBuildTimeSpec(ConstructionSiteBuildTimeSpec original) : base(original)
		{
			this.ConstructionTimeInHours = original.<ConstructionTimeInHours>k__BackingField;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000365D File Offset: 0x0000185D
		public ConstructionSiteBuildTimeSpec()
		{
			this.ConstructionTimeInHours = 1f;
			base..ctor();
		}
	}
}
