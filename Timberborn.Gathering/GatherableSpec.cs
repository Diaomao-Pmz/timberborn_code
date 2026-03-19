using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Yielding;

namespace Timberborn.Gathering
{
	// Token: 0x0200000B RID: 11
	public class GatherableSpec : ComponentSpec, IYielderDecorable, IEquatable<GatherableSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025C9 File Offset: 0x000007C9
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GatherableSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025D5 File Offset: 0x000007D5
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000025DD File Offset: 0x000007DD
		[Serialize]
		public float YieldGrowthTimeInDays { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000025E6 File Offset: 0x000007E6
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000025EE File Offset: 0x000007EE
		[Serialize]
		public YielderSpec Yielder { get; set; }

		// Token: 0x06000032 RID: 50 RVA: 0x000025F8 File Offset: 0x000007F8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GatherableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002644 File Offset: 0x00000844
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("YieldGrowthTimeInDays = ");
			builder.Append(this.YieldGrowthTimeInDays.ToString());
			builder.Append(", Yielder = ");
			builder.Append(this.Yielder);
			return true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026A7 File Offset: 0x000008A7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GatherableSpec left, GatherableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026B3 File Offset: 0x000008B3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GatherableSpec left, GatherableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026C7 File Offset: 0x000008C7
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<YieldGrowthTimeInDays>k__BackingField)) * -1521134295 + EqualityComparer<YielderSpec>.Default.GetHashCode(this.<Yielder>k__BackingField);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026FD File Offset: 0x000008FD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GatherableSpec);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000270B File Offset: 0x0000090B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002714 File Offset: 0x00000914
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GatherableSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<YieldGrowthTimeInDays>k__BackingField, other.<YieldGrowthTimeInDays>k__BackingField) && EqualityComparer<YielderSpec>.Default.Equals(this.<Yielder>k__BackingField, other.<Yielder>k__BackingField));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002768 File Offset: 0x00000968
		[CompilerGenerated]
		protected GatherableSpec([Nullable(1)] GatherableSpec original) : base(original)
		{
			this.YieldGrowthTimeInDays = original.<YieldGrowthTimeInDays>k__BackingField;
			this.Yielder = original.<Yielder>k__BackingField;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002789 File Offset: 0x00000989
		public GatherableSpec()
		{
		}
	}
}
