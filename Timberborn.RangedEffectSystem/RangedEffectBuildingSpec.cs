using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	public class RangedEffectBuildingSpec : ComponentSpec, IEquatable<RangedEffectBuildingSpec>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000299D File Offset: 0x00000B9D
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RangedEffectBuildingSpec);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000029A9 File Offset: 0x00000BA9
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000029B1 File Offset: 0x00000BB1
		[Serialize]
		public int EffectRadius { get; set; }

		// Token: 0x06000056 RID: 86 RVA: 0x000029BC File Offset: 0x00000BBC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RangedEffectBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A08 File Offset: 0x00000C08
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("EffectRadius = ");
			builder.Append(this.EffectRadius.ToString());
			return true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002A52 File Offset: 0x00000C52
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RangedEffectBuildingSpec left, RangedEffectBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A5E File Offset: 0x00000C5E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RangedEffectBuildingSpec left, RangedEffectBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A72 File Offset: 0x00000C72
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<EffectRadius>k__BackingField);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A91 File Offset: 0x00000C91
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RangedEffectBuildingSpec);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000023FB File Offset: 0x000005FB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002A9F File Offset: 0x00000C9F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RangedEffectBuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<EffectRadius>k__BackingField, other.<EffectRadius>k__BackingField));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002AD0 File Offset: 0x00000CD0
		[CompilerGenerated]
		protected RangedEffectBuildingSpec(RangedEffectBuildingSpec original) : base(original)
		{
			this.EffectRadius = original.<EffectRadius>k__BackingField;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000244A File Offset: 0x0000064A
		public RangedEffectBuildingSpec()
		{
		}
	}
}
