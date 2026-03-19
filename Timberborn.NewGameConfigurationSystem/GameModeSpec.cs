using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NewGameConfigurationSystem
{
	// Token: 0x02000007 RID: 7
	public class GameModeSpec : ComponentSpec, IEquatable<GameModeSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GameModeSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public int Order { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize]
		public bool IsDefault { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		[Serialize]
		public string DisplayNameLocKey { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002145 File Offset: 0x00000345
		[Serialize]
		public string DescriptionLocKey { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002156 File Offset: 0x00000356
		[Serialize]
		public int StartingAdults { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000215F File Offset: 0x0000035F
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002167 File Offset: 0x00000367
		[Serialize]
		public MinMaxSpec<float> AdultAgeProgress { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002178 File Offset: 0x00000378
		[Serialize]
		public int StartingChildren { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002181 File Offset: 0x00000381
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002189 File Offset: 0x00000389
		[Serialize]
		public MinMaxSpec<float> ChildAgeProgress { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002192 File Offset: 0x00000392
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000219A File Offset: 0x0000039A
		[Serialize]
		public float FoodConsumption { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021A3 File Offset: 0x000003A3
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021AB File Offset: 0x000003AB
		[Serialize]
		public float WaterConsumption { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000021BC File Offset: 0x000003BC
		[Serialize]
		public int StartingFood { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021C5 File Offset: 0x000003C5
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000021CD File Offset: 0x000003CD
		[Serialize]
		public int StartingWater { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000021DE File Offset: 0x000003DE
		[Serialize]
		public MinMaxSpec<int> TemperateWeatherDuration { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021E7 File Offset: 0x000003E7
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000021EF File Offset: 0x000003EF
		[Serialize]
		public MinMaxSpec<int> DroughtDuration { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021F8 File Offset: 0x000003F8
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002200 File Offset: 0x00000400
		[Serialize]
		public float DroughtDurationHandicapMultiplier { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002209 File Offset: 0x00000409
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002211 File Offset: 0x00000411
		[Serialize]
		public int DroughtDurationHandicapCycles { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000221A File Offset: 0x0000041A
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002222 File Offset: 0x00000422
		[Serialize]
		public int CyclesBeforeRandomizingBadtide { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000222B File Offset: 0x0000042B
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002233 File Offset: 0x00000433
		[Serialize]
		public float ChanceForBadtide { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000223C File Offset: 0x0000043C
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002244 File Offset: 0x00000444
		[Serialize]
		public MinMaxSpec<int> BadtideDuration { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000224D File Offset: 0x0000044D
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002255 File Offset: 0x00000455
		[Serialize]
		public float BadtideDurationHandicapMultiplier { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000225E File Offset: 0x0000045E
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002266 File Offset: 0x00000466
		[Serialize]
		public int BadtideDurationHandicapCycles { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000226F File Offset: 0x0000046F
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002277 File Offset: 0x00000477
		[Serialize]
		public float InjuryChance { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002280 File Offset: 0x00000480
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002288 File Offset: 0x00000488
		[Serialize]
		public float DemolishableRecoveryRate { get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x00002294 File Offset: 0x00000494
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				string.Format("{0}: {1}\n", "Order", this.Order),
				"DisplayNameLocKey: ",
				this.DisplayNameLocKey,
				"\nDescriptionLocKey: ",
				this.DescriptionLocKey,
				"\n",
				string.Format("{0}: {1}\n", "StartingAdults", this.StartingAdults),
				string.Format("{0}: {1}\n", "AdultAgeProgress", this.AdultAgeProgress),
				string.Format("{0}: {1}\n", "StartingChildren", this.StartingChildren),
				string.Format("{0}: {1}\n", "ChildAgeProgress", this.ChildAgeProgress),
				string.Format("{0}: {1}\n", "FoodConsumption", this.FoodConsumption),
				string.Format("{0}: {1}\n", "WaterConsumption", this.WaterConsumption),
				string.Format("{0}: {1}\n", "StartingFood", this.StartingFood),
				string.Format("{0}: {1}\n", "StartingWater", this.StartingWater),
				string.Format("{0}: {1}\n", "TemperateWeatherDuration", this.TemperateWeatherDuration),
				string.Format("{0}: {1}\n", "DroughtDuration", this.DroughtDuration),
				string.Format("{0}: {1}\n", "DroughtDurationHandicapMultiplier", this.DroughtDurationHandicapMultiplier),
				string.Format("{0}: {1}\n", "DroughtDurationHandicapCycles", this.DroughtDurationHandicapCycles),
				string.Format("{0}: {1}\n", "CyclesBeforeRandomizingBadtide", this.CyclesBeforeRandomizingBadtide),
				string.Format("{0}: {1}\n", "ChanceForBadtide", this.ChanceForBadtide),
				string.Format("{0}: {1}\n", "BadtideDuration", this.BadtideDuration),
				string.Format("{0}: {1}\n", "BadtideDurationHandicapMultiplier", this.BadtideDurationHandicapMultiplier),
				string.Format("{0}: {1}\n", "BadtideDurationHandicapCycles", this.BadtideDurationHandicapCycles),
				string.Format("{0}: {1}\n", "InjuryChance", this.InjuryChance),
				string.Format("{0}: {1}", "DemolishableRecoveryRate", this.DemolishableRecoveryRate)
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002514 File Offset: 0x00000714
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Order = ");
			builder.Append(this.Order.ToString());
			builder.Append(", IsDefault = ");
			builder.Append(this.IsDefault.ToString());
			builder.Append(", DisplayNameLocKey = ");
			builder.Append(this.DisplayNameLocKey);
			builder.Append(", DescriptionLocKey = ");
			builder.Append(this.DescriptionLocKey);
			builder.Append(", StartingAdults = ");
			builder.Append(this.StartingAdults.ToString());
			builder.Append(", AdultAgeProgress = ");
			builder.Append(this.AdultAgeProgress);
			builder.Append(", StartingChildren = ");
			builder.Append(this.StartingChildren.ToString());
			builder.Append(", ChildAgeProgress = ");
			builder.Append(this.ChildAgeProgress);
			builder.Append(", FoodConsumption = ");
			builder.Append(this.FoodConsumption.ToString());
			builder.Append(", WaterConsumption = ");
			builder.Append(this.WaterConsumption.ToString());
			builder.Append(", StartingFood = ");
			builder.Append(this.StartingFood.ToString());
			builder.Append(", StartingWater = ");
			builder.Append(this.StartingWater.ToString());
			builder.Append(", TemperateWeatherDuration = ");
			builder.Append(this.TemperateWeatherDuration);
			builder.Append(", DroughtDuration = ");
			builder.Append(this.DroughtDuration);
			builder.Append(", DroughtDurationHandicapMultiplier = ");
			builder.Append(this.DroughtDurationHandicapMultiplier.ToString());
			builder.Append(", DroughtDurationHandicapCycles = ");
			builder.Append(this.DroughtDurationHandicapCycles.ToString());
			builder.Append(", CyclesBeforeRandomizingBadtide = ");
			builder.Append(this.CyclesBeforeRandomizingBadtide.ToString());
			builder.Append(", ChanceForBadtide = ");
			builder.Append(this.ChanceForBadtide.ToString());
			builder.Append(", BadtideDuration = ");
			builder.Append(this.BadtideDuration);
			builder.Append(", BadtideDurationHandicapMultiplier = ");
			builder.Append(this.BadtideDurationHandicapMultiplier.ToString());
			builder.Append(", BadtideDurationHandicapCycles = ");
			builder.Append(this.BadtideDurationHandicapCycles.ToString());
			builder.Append(", InjuryChance = ");
			builder.Append(this.InjuryChance.ToString());
			builder.Append(", DemolishableRecoveryRate = ");
			builder.Append(this.DemolishableRecoveryRate.ToString());
			return true;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002856 File Offset: 0x00000A56
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GameModeSpec left, GameModeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002862 File Offset: 0x00000A62
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GameModeSpec left, GameModeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002878 File Offset: 0x00000A78
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((((((((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDefault>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DescriptionLocKey>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<StartingAdults>k__BackingField)) * -1521134295 + EqualityComparer<MinMaxSpec<float>>.Default.GetHashCode(this.<AdultAgeProgress>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<StartingChildren>k__BackingField)) * -1521134295 + EqualityComparer<MinMaxSpec<float>>.Default.GetHashCode(this.<ChildAgeProgress>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<FoodConsumption>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<WaterConsumption>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<StartingFood>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<StartingWater>k__BackingField)) * -1521134295 + EqualityComparer<MinMaxSpec<int>>.Default.GetHashCode(this.<TemperateWeatherDuration>k__BackingField)) * -1521134295 + EqualityComparer<MinMaxSpec<int>>.Default.GetHashCode(this.<DroughtDuration>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DroughtDurationHandicapMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DroughtDurationHandicapCycles>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CyclesBeforeRandomizingBadtide>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ChanceForBadtide>k__BackingField)) * -1521134295 + EqualityComparer<MinMaxSpec<int>>.Default.GetHashCode(this.<BadtideDuration>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BadtideDurationHandicapMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<BadtideDurationHandicapCycles>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<InjuryChance>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DemolishableRecoveryRate>k__BackingField);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A9C File Offset: 0x00000C9C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GameModeSpec);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AAA File Offset: 0x00000CAA
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AB4 File Offset: 0x00000CB4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GameModeSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDefault>k__BackingField, other.<IsDefault>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DescriptionLocKey>k__BackingField, other.<DescriptionLocKey>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<StartingAdults>k__BackingField, other.<StartingAdults>k__BackingField) && EqualityComparer<MinMaxSpec<float>>.Default.Equals(this.<AdultAgeProgress>k__BackingField, other.<AdultAgeProgress>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<StartingChildren>k__BackingField, other.<StartingChildren>k__BackingField) && EqualityComparer<MinMaxSpec<float>>.Default.Equals(this.<ChildAgeProgress>k__BackingField, other.<ChildAgeProgress>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<FoodConsumption>k__BackingField, other.<FoodConsumption>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<WaterConsumption>k__BackingField, other.<WaterConsumption>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<StartingFood>k__BackingField, other.<StartingFood>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<StartingWater>k__BackingField, other.<StartingWater>k__BackingField) && EqualityComparer<MinMaxSpec<int>>.Default.Equals(this.<TemperateWeatherDuration>k__BackingField, other.<TemperateWeatherDuration>k__BackingField) && EqualityComparer<MinMaxSpec<int>>.Default.Equals(this.<DroughtDuration>k__BackingField, other.<DroughtDuration>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DroughtDurationHandicapMultiplier>k__BackingField, other.<DroughtDurationHandicapMultiplier>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<DroughtDurationHandicapCycles>k__BackingField, other.<DroughtDurationHandicapCycles>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CyclesBeforeRandomizingBadtide>k__BackingField, other.<CyclesBeforeRandomizingBadtide>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ChanceForBadtide>k__BackingField, other.<ChanceForBadtide>k__BackingField) && EqualityComparer<MinMaxSpec<int>>.Default.Equals(this.<BadtideDuration>k__BackingField, other.<BadtideDuration>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<BadtideDurationHandicapMultiplier>k__BackingField, other.<BadtideDurationHandicapMultiplier>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<BadtideDurationHandicapCycles>k__BackingField, other.<BadtideDurationHandicapCycles>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<InjuryChance>k__BackingField, other.<InjuryChance>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DemolishableRecoveryRate>k__BackingField, other.<DemolishableRecoveryRate>k__BackingField));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D3C File Offset: 0x00000F3C
		[CompilerGenerated]
		protected GameModeSpec([Nullable(1)] GameModeSpec original) : base(original)
		{
			this.Order = original.<Order>k__BackingField;
			this.IsDefault = original.<IsDefault>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
			this.DescriptionLocKey = original.<DescriptionLocKey>k__BackingField;
			this.StartingAdults = original.<StartingAdults>k__BackingField;
			this.AdultAgeProgress = original.<AdultAgeProgress>k__BackingField;
			this.StartingChildren = original.<StartingChildren>k__BackingField;
			this.ChildAgeProgress = original.<ChildAgeProgress>k__BackingField;
			this.FoodConsumption = original.<FoodConsumption>k__BackingField;
			this.WaterConsumption = original.<WaterConsumption>k__BackingField;
			this.StartingFood = original.<StartingFood>k__BackingField;
			this.StartingWater = original.<StartingWater>k__BackingField;
			this.TemperateWeatherDuration = original.<TemperateWeatherDuration>k__BackingField;
			this.DroughtDuration = original.<DroughtDuration>k__BackingField;
			this.DroughtDurationHandicapMultiplier = original.<DroughtDurationHandicapMultiplier>k__BackingField;
			this.DroughtDurationHandicapCycles = original.<DroughtDurationHandicapCycles>k__BackingField;
			this.CyclesBeforeRandomizingBadtide = original.<CyclesBeforeRandomizingBadtide>k__BackingField;
			this.ChanceForBadtide = original.<ChanceForBadtide>k__BackingField;
			this.BadtideDuration = original.<BadtideDuration>k__BackingField;
			this.BadtideDurationHandicapMultiplier = original.<BadtideDurationHandicapMultiplier>k__BackingField;
			this.BadtideDurationHandicapCycles = original.<BadtideDurationHandicapCycles>k__BackingField;
			this.InjuryChance = original.<InjuryChance>k__BackingField;
			this.DemolishableRecoveryRate = original.<DemolishableRecoveryRate>k__BackingField;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002E64 File Offset: 0x00001064
		public GameModeSpec()
		{
		}
	}
}
