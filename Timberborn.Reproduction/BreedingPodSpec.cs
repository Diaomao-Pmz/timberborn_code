using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Goods;

namespace Timberborn.Reproduction
{
	// Token: 0x0200000B RID: 11
	public class BreedingPodSpec : ComponentSpec, IEquatable<BreedingPodSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002734 File Offset: 0x00000934
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BreedingPodSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002740 File Offset: 0x00000940
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002748 File Offset: 0x00000948
		[Serialize]
		public string EmbryoName { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002751 File Offset: 0x00000951
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002759 File Offset: 0x00000959
		[Serialize]
		public float CycleLengthInDays { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002762 File Offset: 0x00000962
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000276A File Offset: 0x0000096A
		[Serialize]
		public int CyclesUntilFullyGrown { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002773 File Offset: 0x00000973
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000277B File Offset: 0x0000097B
		[Serialize]
		public int CyclesCapacity { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002784 File Offset: 0x00000984
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000278C File Offset: 0x0000098C
		[Serialize]
		public ImmutableArray<GoodAmountSpec> NutrientsPerCycle { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002795 File Offset: 0x00000995
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000279D File Offset: 0x0000099D
		[Serialize]
		public bool SpawnAdults { get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x000027A8 File Offset: 0x000009A8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BreedingPodSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027F4 File Offset: 0x000009F4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("EmbryoName = ");
			builder.Append(this.EmbryoName);
			builder.Append(", CycleLengthInDays = ");
			builder.Append(this.CycleLengthInDays.ToString());
			builder.Append(", CyclesUntilFullyGrown = ");
			builder.Append(this.CyclesUntilFullyGrown.ToString());
			builder.Append(", CyclesCapacity = ");
			builder.Append(this.CyclesCapacity.ToString());
			builder.Append(", NutrientsPerCycle = ");
			builder.Append(this.NutrientsPerCycle.ToString());
			builder.Append(", SpawnAdults = ");
			builder.Append(this.SpawnAdults.ToString());
			return true;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028F3 File Offset: 0x00000AF3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BreedingPodSpec left, BreedingPodSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028FF File Offset: 0x00000AFF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BreedingPodSpec left, BreedingPodSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002914 File Offset: 0x00000B14
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<EmbryoName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<CycleLengthInDays>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CyclesUntilFullyGrown>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CyclesCapacity>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.GetHashCode(this.<NutrientsPerCycle>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SpawnAdults>k__BackingField);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029B1 File Offset: 0x00000BB1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BreedingPodSpec);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029BF File Offset: 0x00000BBF
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000029C8 File Offset: 0x00000BC8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BreedingPodSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<EmbryoName>k__BackingField, other.<EmbryoName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<CycleLengthInDays>k__BackingField, other.<CycleLengthInDays>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CyclesUntilFullyGrown>k__BackingField, other.<CyclesUntilFullyGrown>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CyclesCapacity>k__BackingField, other.<CyclesCapacity>k__BackingField) && EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.Equals(this.<NutrientsPerCycle>k__BackingField, other.<NutrientsPerCycle>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SpawnAdults>k__BackingField, other.<SpawnAdults>k__BackingField));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A84 File Offset: 0x00000C84
		[CompilerGenerated]
		protected BreedingPodSpec([Nullable(1)] BreedingPodSpec original) : base(original)
		{
			this.EmbryoName = original.<EmbryoName>k__BackingField;
			this.CycleLengthInDays = original.<CycleLengthInDays>k__BackingField;
			this.CyclesUntilFullyGrown = original.<CyclesUntilFullyGrown>k__BackingField;
			this.CyclesCapacity = original.<CyclesCapacity>k__BackingField;
			this.NutrientsPerCycle = original.<NutrientsPerCycle>k__BackingField;
			this.SpawnAdults = original.<SpawnAdults>k__BackingField;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public BreedingPodSpec()
		{
		}
	}
}
