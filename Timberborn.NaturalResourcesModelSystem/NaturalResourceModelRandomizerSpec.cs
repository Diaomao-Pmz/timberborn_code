using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NaturalResourcesModelSystem
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class NaturalResourceModelRandomizerSpec : ComponentSpec, IEquatable<NaturalResourceModelRandomizerSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000264D File Offset: 0x0000084D
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(NaturalResourceModelRandomizerSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002659 File Offset: 0x00000859
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002661 File Offset: 0x00000861
		[Serialize]
		public bool ConstrainProportion { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000266A File Offset: 0x0000086A
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002672 File Offset: 0x00000872
		[Serialize]
		public float MinHeightScaleFactor { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000267B File Offset: 0x0000087B
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002683 File Offset: 0x00000883
		[Serialize]
		public float MaxHeightScaleFactor { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000268C File Offset: 0x0000088C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002694 File Offset: 0x00000894
		[Serialize]
		public float MinWidthScaleFactor { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000269D File Offset: 0x0000089D
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000026A5 File Offset: 0x000008A5
		[Serialize]
		public float MaxWidthScaleFactor { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026AE File Offset: 0x000008AE
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000026B6 File Offset: 0x000008B6
		[Serialize]
		public RandomizeRotationMode RandomizedRotation { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000026BF File Offset: 0x000008BF
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000026C7 File Offset: 0x000008C7
		[Serialize]
		public float MinRotation { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000026D0 File Offset: 0x000008D0
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000026D8 File Offset: 0x000008D8
		[Serialize]
		public float MaxRotation { get; set; }

		// Token: 0x0600003B RID: 59 RVA: 0x000026E4 File Offset: 0x000008E4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NaturalResourceModelRandomizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002730 File Offset: 0x00000930
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ConstrainProportion = ");
			builder.Append(this.ConstrainProportion.ToString());
			builder.Append(", MinHeightScaleFactor = ");
			builder.Append(this.MinHeightScaleFactor.ToString());
			builder.Append(", MaxHeightScaleFactor = ");
			builder.Append(this.MaxHeightScaleFactor.ToString());
			builder.Append(", MinWidthScaleFactor = ");
			builder.Append(this.MinWidthScaleFactor.ToString());
			builder.Append(", MaxWidthScaleFactor = ");
			builder.Append(this.MaxWidthScaleFactor.ToString());
			builder.Append(", RandomizedRotation = ");
			builder.Append(this.RandomizedRotation.ToString());
			builder.Append(", MinRotation = ");
			builder.Append(this.MinRotation.ToString());
			builder.Append(", MaxRotation = ");
			builder.Append(this.MaxRotation.ToString());
			return true;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000288B File Offset: 0x00000A8B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NaturalResourceModelRandomizerSpec left, NaturalResourceModelRandomizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002897 File Offset: 0x00000A97
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NaturalResourceModelRandomizerSpec left, NaturalResourceModelRandomizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028AC File Offset: 0x00000AAC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ConstrainProportion>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinHeightScaleFactor>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxHeightScaleFactor>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinWidthScaleFactor>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxWidthScaleFactor>k__BackingField)) * -1521134295 + EqualityComparer<RandomizeRotationMode>.Default.GetHashCode(this.<RandomizedRotation>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinRotation>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxRotation>k__BackingField);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002977 File Offset: 0x00000B77
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NaturalResourceModelRandomizerSpec);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002985 File Offset: 0x00000B85
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002990 File Offset: 0x00000B90
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NaturalResourceModelRandomizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<ConstrainProportion>k__BackingField, other.<ConstrainProportion>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinHeightScaleFactor>k__BackingField, other.<MinHeightScaleFactor>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxHeightScaleFactor>k__BackingField, other.<MaxHeightScaleFactor>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinWidthScaleFactor>k__BackingField, other.<MinWidthScaleFactor>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxWidthScaleFactor>k__BackingField, other.<MaxWidthScaleFactor>k__BackingField) && EqualityComparer<RandomizeRotationMode>.Default.Equals(this.<RandomizedRotation>k__BackingField, other.<RandomizedRotation>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinRotation>k__BackingField, other.<MinRotation>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxRotation>k__BackingField, other.<MaxRotation>k__BackingField));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A80 File Offset: 0x00000C80
		[CompilerGenerated]
		protected NaturalResourceModelRandomizerSpec(NaturalResourceModelRandomizerSpec original) : base(original)
		{
			this.ConstrainProportion = original.<ConstrainProportion>k__BackingField;
			this.MinHeightScaleFactor = original.<MinHeightScaleFactor>k__BackingField;
			this.MaxHeightScaleFactor = original.<MaxHeightScaleFactor>k__BackingField;
			this.MinWidthScaleFactor = original.<MinWidthScaleFactor>k__BackingField;
			this.MaxWidthScaleFactor = original.<MaxWidthScaleFactor>k__BackingField;
			this.RandomizedRotation = original.<RandomizedRotation>k__BackingField;
			this.MinRotation = original.<MinRotation>k__BackingField;
			this.MaxRotation = original.<MaxRotation>k__BackingField;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public NaturalResourceModelRandomizerSpec()
		{
			this.MinHeightScaleFactor = 0.8f;
			this.MaxHeightScaleFactor = 1.2f;
			this.MinWidthScaleFactor = 0.8f;
			this.MaxWidthScaleFactor = 1.2f;
			this.MaxRotation = 360f;
			base..ctor();
		}
	}
}
