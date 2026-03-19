using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Pollination
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class HiveSpec : ComponentSpec, IEquatable<HiveSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002587 File Offset: 0x00000787
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(HiveSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002593 File Offset: 0x00000793
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000259B File Offset: 0x0000079B
		[Serialize]
		public int PollinationRadius { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000025AC File Offset: 0x000007AC
		[Serialize]
		public float HoursBetweenPollinations { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000025B5 File Offset: 0x000007B5
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000025BD File Offset: 0x000007BD
		[Serialize]
		public float GrowthTimeReduction { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025C6 File Offset: 0x000007C6
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000025CE File Offset: 0x000007CE
		[Serialize]
		public int PlantsPerPollination { get; set; }

		// Token: 0x06000027 RID: 39 RVA: 0x000025D8 File Offset: 0x000007D8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("HiveSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002624 File Offset: 0x00000824
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PollinationRadius = ");
			builder.Append(this.PollinationRadius.ToString());
			builder.Append(", HoursBetweenPollinations = ");
			builder.Append(this.HoursBetweenPollinations.ToString());
			builder.Append(", GrowthTimeReduction = ");
			builder.Append(this.GrowthTimeReduction.ToString());
			builder.Append(", PlantsPerPollination = ");
			builder.Append(this.PlantsPerPollination.ToString());
			return true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026E3 File Offset: 0x000008E3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(HiveSpec left, HiveSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026EF File Offset: 0x000008EF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(HiveSpec left, HiveSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002704 File Offset: 0x00000904
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PollinationRadius>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<HoursBetweenPollinations>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<GrowthTimeReduction>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PlantsPerPollination>k__BackingField);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002773 File Offset: 0x00000973
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HiveSpec);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002781 File Offset: 0x00000981
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000278C File Offset: 0x0000098C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(HiveSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<PollinationRadius>k__BackingField, other.<PollinationRadius>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<HoursBetweenPollinations>k__BackingField, other.<HoursBetweenPollinations>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<GrowthTimeReduction>k__BackingField, other.<GrowthTimeReduction>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PlantsPerPollination>k__BackingField, other.<PlantsPerPollination>k__BackingField));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002810 File Offset: 0x00000A10
		[CompilerGenerated]
		protected HiveSpec(HiveSpec original) : base(original)
		{
			this.PollinationRadius = original.<PollinationRadius>k__BackingField;
			this.HoursBetweenPollinations = original.<HoursBetweenPollinations>k__BackingField;
			this.GrowthTimeReduction = original.<GrowthTimeReduction>k__BackingField;
			this.PlantsPerPollination = original.<PlantsPerPollination>k__BackingField;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002849 File Offset: 0x00000A49
		public HiveSpec()
		{
		}
	}
}
