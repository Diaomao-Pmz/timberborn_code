using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Wellbeing
{
	// Token: 0x0200001A RID: 26
	public class WellbeingTierSpec : ComponentSpec, IEquatable<WellbeingTierSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002D08 File Offset: 0x00000F08
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WellbeingTierSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002D14 File Offset: 0x00000F14
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002D1C File Offset: 0x00000F1C
		[Serialize]
		public string CharacterType { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002D25 File Offset: 0x00000F25
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002D2D File Offset: 0x00000F2D
		[Serialize]
		public string BonusId { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002D36 File Offset: 0x00000F36
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002D3E File Offset: 0x00000F3E
		[Serialize]
		public ImmutableArray<WellbeingTierBonusSpec> Bonuses { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002D47 File Offset: 0x00000F47
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002D4F File Offset: 0x00000F4F
		[Serialize]
		public int WellbeingThreshold { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002D58 File Offset: 0x00000F58
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002D60 File Offset: 0x00000F60
		[Serialize]
		public float MultiplierIncrement { get; set; }

		// Token: 0x06000071 RID: 113 RVA: 0x00002D6C File Offset: 0x00000F6C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WellbeingTierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002DB8 File Offset: 0x00000FB8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CharacterType = ");
			builder.Append(this.CharacterType);
			builder.Append(", BonusId = ");
			builder.Append(this.BonusId);
			builder.Append(", Bonuses = ");
			builder.Append(this.Bonuses.ToString());
			builder.Append(", WellbeingThreshold = ");
			builder.Append(this.WellbeingThreshold.ToString());
			builder.Append(", MultiplierIncrement = ");
			builder.Append(this.MultiplierIncrement.ToString());
			return true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E82 File Offset: 0x00001082
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WellbeingTierSpec left, WellbeingTierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002E8E File Offset: 0x0000108E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WellbeingTierSpec left, WellbeingTierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002EA4 File Offset: 0x000010A4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CharacterType>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BonusId>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<WellbeingTierBonusSpec>>.Default.GetHashCode(this.<Bonuses>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<WellbeingThreshold>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MultiplierIncrement>k__BackingField);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F2A File Offset: 0x0000112A
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WellbeingTierSpec);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002F38 File Offset: 0x00001138
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002F44 File Offset: 0x00001144
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WellbeingTierSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<CharacterType>k__BackingField, other.<CharacterType>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BonusId>k__BackingField, other.<BonusId>k__BackingField) && EqualityComparer<ImmutableArray<WellbeingTierBonusSpec>>.Default.Equals(this.<Bonuses>k__BackingField, other.<Bonuses>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<WellbeingThreshold>k__BackingField, other.<WellbeingThreshold>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MultiplierIncrement>k__BackingField, other.<MultiplierIncrement>k__BackingField));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002FE4 File Offset: 0x000011E4
		[CompilerGenerated]
		protected WellbeingTierSpec([Nullable(1)] WellbeingTierSpec original) : base(original)
		{
			this.CharacterType = original.<CharacterType>k__BackingField;
			this.BonusId = original.<BonusId>k__BackingField;
			this.Bonuses = original.<Bonuses>k__BackingField;
			this.WellbeingThreshold = original.<WellbeingThreshold>k__BackingField;
			this.MultiplierIncrement = original.<MultiplierIncrement>k__BackingField;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003034 File Offset: 0x00001234
		public WellbeingTierSpec()
		{
		}
	}
}
