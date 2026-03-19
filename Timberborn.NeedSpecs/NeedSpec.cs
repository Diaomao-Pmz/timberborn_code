using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.NeedSpecs
{
	// Token: 0x02000011 RID: 17
	public class NeedSpec : ComponentSpec, IEquatable<NeedSpec>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002DA1 File Offset: 0x00000FA1
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(NeedSpec);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002DAD File Offset: 0x00000FAD
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002DB5 File Offset: 0x00000FB5
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002DBE File Offset: 0x00000FBE
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002DC6 File Offset: 0x00000FC6
		[Serialize]
		public ImmutableArray<string> BackwardCompatibleIds { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002DCF File Offset: 0x00000FCF
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002DD7 File Offset: 0x00000FD7
		[Serialize]
		public int Order { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002DE0 File Offset: 0x00000FE0
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002DE8 File Offset: 0x00000FE8
		[Serialize]
		public string NeedGroupId { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002DF1 File Offset: 0x00000FF1
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002DF9 File Offset: 0x00000FF9
		[Serialize]
		public string CharacterType { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002E02 File Offset: 0x00001002
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002E0A File Offset: 0x0000100A
		[Serialize("DisplayNameLocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002E13 File Offset: 0x00001013
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002E1B File Offset: 0x0000101B
		[Serialize]
		public float StartingValue { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002E24 File Offset: 0x00001024
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002E2C File Offset: 0x0000102C
		[Serialize]
		public float MinimumValue { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002E35 File Offset: 0x00001035
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002E3D File Offset: 0x0000103D
		[Serialize]
		public float MaximumValue { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002E46 File Offset: 0x00001046
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002E4E File Offset: 0x0000104E
		[Serialize]
		public float DailyDelta { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002E57 File Offset: 0x00001057
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002E5F File Offset: 0x0000105F
		[Serialize]
		public float ImportanceMultiplier { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002E68 File Offset: 0x00001068
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002E70 File Offset: 0x00001070
		[Serialize]
		public float Effectiveness { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002E79 File Offset: 0x00001079
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002E81 File Offset: 0x00001081
		[Serialize]
		public bool Wastable { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002E8A File Offset: 0x0000108A
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002E92 File Offset: 0x00001092
		[Serialize]
		public float HoursWarningThreshold { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002E9B File Offset: 0x0000109B
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002EA3 File Offset: 0x000010A3
		[Serialize]
		public string DisplayNameLocKey { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002EAC File Offset: 0x000010AC
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002EB4 File Offset: 0x000010B4
		[Serialize]
		private int FavorableWellbeing { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002EBD File Offset: 0x000010BD
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002EC5 File Offset: 0x000010C5
		[Serialize]
		private int UnfavorableWellbeing { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002ECE File Offset: 0x000010CE
		public bool IsNeverNegative
		{
			get
			{
				return this.MinimumValue >= 0f;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002EE0 File Offset: 0x000010E0
		public bool IsNeverPositive
		{
			get
			{
				return this.MaximumValue <= 0f;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002EF2 File Offset: 0x000010F2
		public bool AffectsWellbeing
		{
			get
			{
				return this.FavorableWellbeing != 0 || this.UnfavorableWellbeing != 0;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002F07 File Offset: 0x00001107
		public int GetFavorableWellbeing()
		{
			return this.GetWellbeing(true);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002F10 File Offset: 0x00001110
		public int GetUnfavorableWellbeing()
		{
			return this.GetWellbeing(false);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002F19 File Offset: 0x00001119
		public int GetWellbeing(bool isFavourable)
		{
			if (!isFavourable)
			{
				return this.UnfavorableWellbeing;
			}
			return this.FavorableWellbeing;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002F2C File Offset: 0x0000112C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NeedSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002F78 File Offset: 0x00001178
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", BackwardCompatibleIds = ");
			builder.Append(this.BackwardCompatibleIds.ToString());
			builder.Append(", Order = ");
			builder.Append(this.Order.ToString());
			builder.Append(", NeedGroupId = ");
			builder.Append(this.NeedGroupId);
			builder.Append(", CharacterType = ");
			builder.Append(this.CharacterType);
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", StartingValue = ");
			builder.Append(this.StartingValue.ToString());
			builder.Append(", MinimumValue = ");
			builder.Append(this.MinimumValue.ToString());
			builder.Append(", MaximumValue = ");
			builder.Append(this.MaximumValue.ToString());
			builder.Append(", DailyDelta = ");
			builder.Append(this.DailyDelta.ToString());
			builder.Append(", ImportanceMultiplier = ");
			builder.Append(this.ImportanceMultiplier.ToString());
			builder.Append(", Effectiveness = ");
			builder.Append(this.Effectiveness.ToString());
			builder.Append(", Wastable = ");
			builder.Append(this.Wastable.ToString());
			builder.Append(", HoursWarningThreshold = ");
			builder.Append(this.HoursWarningThreshold.ToString());
			builder.Append(", DisplayNameLocKey = ");
			builder.Append(this.DisplayNameLocKey);
			builder.Append(", IsNeverNegative = ");
			builder.Append(this.IsNeverNegative.ToString());
			builder.Append(", IsNeverPositive = ");
			builder.Append(this.IsNeverPositive.ToString());
			builder.Append(", AffectsWellbeing = ");
			builder.Append(this.AffectsWellbeing.ToString());
			return true;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003213 File Offset: 0x00001413
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NeedSpec left, NeedSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000321F File Offset: 0x0000141F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NeedSpec left, NeedSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003234 File Offset: 0x00001434
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<BackwardCompatibleIds>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NeedGroupId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CharacterType>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<StartingValue>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinimumValue>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaximumValue>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DailyDelta>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ImportanceMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Effectiveness>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Wastable>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<HoursWarningThreshold>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<FavorableWellbeing>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<UnfavorableWellbeing>k__BackingField);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000033CE File Offset: 0x000015CE
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NeedSpec);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000271B File Offset: 0x0000091B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000033DC File Offset: 0x000015DC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NeedSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<BackwardCompatibleIds>k__BackingField, other.<BackwardCompatibleIds>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NeedGroupId>k__BackingField, other.<NeedGroupId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CharacterType>k__BackingField, other.<CharacterType>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<StartingValue>k__BackingField, other.<StartingValue>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinimumValue>k__BackingField, other.<MinimumValue>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaximumValue>k__BackingField, other.<MaximumValue>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DailyDelta>k__BackingField, other.<DailyDelta>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ImportanceMultiplier>k__BackingField, other.<ImportanceMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Effectiveness>k__BackingField, other.<Effectiveness>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Wastable>k__BackingField, other.<Wastable>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<HoursWarningThreshold>k__BackingField, other.<HoursWarningThreshold>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<FavorableWellbeing>k__BackingField, other.<FavorableWellbeing>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<UnfavorableWellbeing>k__BackingField, other.<UnfavorableWellbeing>k__BackingField));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000035C0 File Offset: 0x000017C0
		[CompilerGenerated]
		protected NeedSpec([Nullable(1)] NeedSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.BackwardCompatibleIds = original.<BackwardCompatibleIds>k__BackingField;
			this.Order = original.<Order>k__BackingField;
			this.NeedGroupId = original.<NeedGroupId>k__BackingField;
			this.CharacterType = original.<CharacterType>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.StartingValue = original.<StartingValue>k__BackingField;
			this.MinimumValue = original.<MinimumValue>k__BackingField;
			this.MaximumValue = original.<MaximumValue>k__BackingField;
			this.DailyDelta = original.<DailyDelta>k__BackingField;
			this.ImportanceMultiplier = original.<ImportanceMultiplier>k__BackingField;
			this.Effectiveness = original.<Effectiveness>k__BackingField;
			this.Wastable = original.<Wastable>k__BackingField;
			this.HoursWarningThreshold = original.<HoursWarningThreshold>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
			this.FavorableWellbeing = original.<FavorableWellbeing>k__BackingField;
			this.UnfavorableWellbeing = original.<UnfavorableWellbeing>k__BackingField;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000283C File Offset: 0x00000A3C
		public NeedSpec()
		{
		}
	}
}
