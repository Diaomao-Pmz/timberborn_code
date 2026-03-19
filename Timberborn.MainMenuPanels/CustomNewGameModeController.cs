using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Timberborn.CoreUI;
using Timberborn.NewGameConfigurationSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuPanels
{
	// Token: 0x02000005 RID: 5
	public class CustomNewGameModeController
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002350 File Offset: 0x00000550
		public void Initialize(VisualElement root, GameModeSpec defaultGameMode, Action gameModeChangedCallback)
		{
			this._gameModeChangedCallback = gameModeChangedCallback;
			this._integerFields.Clear();
			this._startingAdults = this.QInitializedIntField(root, "StartingAdults", defaultGameMode.StartingAdults, 1, 250);
			ValueTuple<IntegerField, IntegerField> valueTuple = this.QInitializedMinMaxPercentFields(root, "MinAdultAgeProgress", "MaxAdultAgeProgress", defaultGameMode.AdultAgeProgress);
			this._minAdultAgeProgress = valueTuple.Item1;
			this._maxAdultAgeProgress = valueTuple.Item2;
			this._startingChildren = this.QInitializedIntField(root, "StartingChildren", defaultGameMode.StartingChildren, 0, 250);
			valueTuple = this.QInitializedMinMaxPercentFields(root, "MinChildAgeProgress", "MaxChildAgeProgress", defaultGameMode.ChildAgeProgress);
			this._minChildAgeProgress = valueTuple.Item1;
			this._maxChildAgeProgress = valueTuple.Item2;
			this._foodConsumption = this.QInitializedPercentField(root, "FoodConsumption", defaultGameMode.FoodConsumption, 10, 1000);
			this._waterConsumption = this.QInitializedPercentField(root, "WaterConsumption", defaultGameMode.WaterConsumption, 10, 1000);
			this._startingFood = this.QInitializedIntField(root, "StartingFood", defaultGameMode.StartingFood, 0, 999999);
			this._startingWater = this.QInitializedIntField(root, "StartingWater", defaultGameMode.StartingWater, 0, 999999);
			valueTuple = this.QInitializedMinMaxIntFields(root, "MinTemperateWeatherDuration", "MaxTemperateWeatherDuration", defaultGameMode.TemperateWeatherDuration, 3);
			this._minTemperateWeatherDuration = valueTuple.Item1;
			this._maxTemperateWeatherDuration = valueTuple.Item2;
			this._droughtEnabled = this.QInitializedToggle(root, "DroughtEnabled", true);
			this._droughtWrapper = UQueryExtensions.Q<VisualElement>(root, "DroughtWrapper", null);
			valueTuple = this.QInitializedMinMaxIntFields(root, "MinDroughtDuration", "MaxDroughtDuration", defaultGameMode.DroughtDuration, 0);
			this._minDroughtDuration = valueTuple.Item1;
			this._maxDroughtDuration = valueTuple.Item2;
			this._droughtDurationHandicapMultiplier = this.QInitializedPercentField(root, "DroughtDurationHandicapMultiplier", defaultGameMode.DroughtDurationHandicapMultiplier, 0, 100);
			this._droughtDurationHandicapCycles = this.QInitializedIntField(root, "DroughtDurationHandicapCycles", defaultGameMode.DroughtDurationHandicapCycles, 0, int.MaxValue);
			this._badtideEnabled = this.QInitializedToggle(root, "BadtideEnabled", true);
			this._badtideWrapper = UQueryExtensions.Q<VisualElement>(root, "BadtideWrapper", null);
			this._cyclesBeforeRandomizingBadtide = this.QInitializedIntField(root, "CyclesBeforeRandomizingBadtide", defaultGameMode.CyclesBeforeRandomizingBadtide, 0, int.MaxValue);
			this._chanceForBadtide = this.QInitializedPercentField(root, "ChanceForBadtide", defaultGameMode.ChanceForBadtide, 0, 100);
			valueTuple = this.QInitializedMinMaxIntFields(root, "MinBadtideDuration", "MaxBadtideDuration", defaultGameMode.BadtideDuration, 0);
			this._minBadtideDuration = valueTuple.Item1;
			this._maxBadtideDuration = valueTuple.Item2;
			this._badtideDurationHandicapMultiplier = this.QInitializedPercentField(root, "BadtideDurationHandicapMultiplier", defaultGameMode.BadtideDurationHandicapMultiplier, 0, 100);
			this._badtideDurationHandicapCycles = this.QInitializedIntField(root, "BadtideDurationHandicapCycles", defaultGameMode.BadtideDurationHandicapCycles, 0, int.MaxValue);
			this._injuryChance = this.QInitializedPercentField(root, "InjuryChance", defaultGameMode.InjuryChance, 0, 1000);
			this._demolishableRecoveryRate = this.QInitializedPercentField(root, "DemolishableRecoveryRate", defaultGameMode.DemolishableRecoveryRate, 0, 100);
			UQueryExtensions.Q<ScrollView>(root, "CustomModeSettings", null).scrollOffset = Vector2.zero;
			this.UpdateFieldVisibility();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002664 File Offset: 0x00000864
		public bool TryGetValidatedGameMode(out GameModeSpec gameMode)
		{
			GameModeSpec gameMode2 = this.GetGameMode();
			if (this.ValidateGameMode(gameMode2))
			{
				gameMode = gameMode2;
				return true;
			}
			gameMode = null;
			return false;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000268A File Offset: 0x0000088A
		public bool IsDroughtEnabled
		{
			get
			{
				return this._droughtEnabled.value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002697 File Offset: 0x00000897
		public bool IsBadtideEnabled
		{
			get
			{
				return this._badtideEnabled.value;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026A4 File Offset: 0x000008A4
		[return: TupleElementNames(new string[]
		{
			"min",
			"max"
		})]
		public ValueTuple<IntegerField, IntegerField> QInitializedMinMaxPercentFields(VisualElement root, string minName, string maxName, MinMaxSpec<float> startingValue)
		{
			return new ValueTuple<IntegerField, IntegerField>(this.QInitializedPercentField(root, minName, startingValue.Min, 0, 100), this.QInitializedPercentField(root, maxName, startingValue.Max, 0, 100));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026CF File Offset: 0x000008CF
		[return: TupleElementNames(new string[]
		{
			"min",
			"max"
		})]
		public ValueTuple<IntegerField, IntegerField> QInitializedMinMaxIntFields(VisualElement root, string minName, string maxName, MinMaxSpec<int> startingValue, int minValue = 0)
		{
			return new ValueTuple<IntegerField, IntegerField>(this.QInitializedIntField(root, minName, startingValue.Min, minValue, int.MaxValue), this.QInitializedIntField(root, maxName, startingValue.Max, minValue, int.MaxValue));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002702 File Offset: 0x00000902
		public IntegerField QInitializedPercentField(VisualElement root, string name, float startingValue, int minValue = 0, int maxValue = 100)
		{
			return this.QInitializedIntField(root, name, CustomNewGameModeController.FloatToPercent(startingValue), minValue, maxValue);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002718 File Offset: 0x00000918
		public IntegerField QInitializedIntField(VisualElement root, string name, int startingValue, int minValue = 0, int maxValue = 2147483647)
		{
			IntegerField integerField = UQueryExtensions.Q<IntegerField>(root, name, null);
			TextFields.InitializeIntegerField(integerField, startingValue, minValue, maxValue, delegate(int _)
			{
				this.OnGameModeChanged();
			});
			this._integerFields.Add(integerField);
			return integerField;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002752 File Offset: 0x00000952
		public Toggle QInitializedToggle(VisualElement root, string name, bool startingValue)
		{
			Toggle toggle = UQueryExtensions.Q<Toggle>(root, name, null);
			toggle.SetValueWithoutNotify(startingValue);
			toggle.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnGameModeChanged();
			}, 0);
			return toggle;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002776 File Offset: 0x00000976
		public void OnGameModeChanged()
		{
			this.ValidateGameMode(this.GetGameMode());
			this.UpdateFieldVisibility();
			this._gameModeChangedCallback();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002798 File Offset: 0x00000998
		public GameModeSpec GetGameMode()
		{
			int @int = CustomNewGameModeController.GetInt(this._startingAdults);
			MinMaxSpec<float> floatsFromPercents = CustomNewGameModeController.GetFloatsFromPercents(this._minAdultAgeProgress, this._maxAdultAgeProgress);
			int int2 = CustomNewGameModeController.GetInt(this._startingChildren);
			MinMaxSpec<float> floatsFromPercents2 = CustomNewGameModeController.GetFloatsFromPercents(this._minChildAgeProgress, this._maxChildAgeProgress);
			float floatFromPercent = CustomNewGameModeController.GetFloatFromPercent(this._foodConsumption);
			float floatFromPercent2 = CustomNewGameModeController.GetFloatFromPercent(this._waterConsumption);
			int int3 = CustomNewGameModeController.GetInt(this._startingFood);
			int int4 = CustomNewGameModeController.GetInt(this._startingWater);
			MinMaxSpec<int> ints = CustomNewGameModeController.GetInts(this._minTemperateWeatherDuration, this._maxTemperateWeatherDuration);
			MinMaxSpec<int> droughtDuration = this.IsDroughtEnabled ? CustomNewGameModeController.GetInts(this._minDroughtDuration, this._maxDroughtDuration) : new MinMaxSpec<int>();
			float droughtDurationHandicapMultiplier = this.IsDroughtEnabled ? CustomNewGameModeController.GetFloatFromPercent(this._droughtDurationHandicapMultiplier) : 0f;
			int droughtDurationHandicapCycles = this.IsDroughtEnabled ? CustomNewGameModeController.GetInt(this._droughtDurationHandicapCycles) : 0;
			int cyclesBeforeRandomizingBadtide = this.IsBadtideEnabled ? CustomNewGameModeController.GetInt(this._cyclesBeforeRandomizingBadtide) : 0;
			float chanceForBadtide = CustomNewGameModeController.GetFloatFromPercent(this._chanceForBadtide);
			if (!this.IsBadtideEnabled)
			{
				chanceForBadtide = 0f;
			}
			else if (!this.IsDroughtEnabled)
			{
				chanceForBadtide = 1f;
			}
			MinMaxSpec<int> badtideDuration = this.IsBadtideEnabled ? CustomNewGameModeController.GetInts(this._minBadtideDuration, this._maxBadtideDuration) : new MinMaxSpec<int>();
			float badtideDurationHandicapMultiplier = this.IsBadtideEnabled ? CustomNewGameModeController.GetFloatFromPercent(this._badtideDurationHandicapMultiplier) : 0f;
			int badtideDurationHandicapCycles = this.IsBadtideEnabled ? CustomNewGameModeController.GetInt(this._badtideDurationHandicapCycles) : 0;
			float floatFromPercent3 = CustomNewGameModeController.GetFloatFromPercent(this._injuryChance);
			float floatFromPercent4 = CustomNewGameModeController.GetFloatFromPercent(this._demolishableRecoveryRate);
			return new GameModeSpec
			{
				StartingAdults = @int,
				AdultAgeProgress = floatsFromPercents,
				StartingChildren = int2,
				ChildAgeProgress = floatsFromPercents2,
				FoodConsumption = floatFromPercent,
				WaterConsumption = floatFromPercent2,
				StartingFood = int3,
				StartingWater = int4,
				TemperateWeatherDuration = ints,
				DroughtDuration = droughtDuration,
				DroughtDurationHandicapMultiplier = droughtDurationHandicapMultiplier,
				DroughtDurationHandicapCycles = droughtDurationHandicapCycles,
				CyclesBeforeRandomizingBadtide = cyclesBeforeRandomizingBadtide,
				ChanceForBadtide = chanceForBadtide,
				BadtideDuration = badtideDuration,
				BadtideDurationHandicapMultiplier = badtideDurationHandicapMultiplier,
				BadtideDurationHandicapCycles = badtideDurationHandicapCycles,
				InjuryChance = floatFromPercent3,
				DemolishableRecoveryRate = floatFromPercent4
			};
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ValidateGameMode(GameModeSpec gameMode)
		{
			foreach (IntegerField integerField in this._integerFields)
			{
				integerField.RemoveFromClassList(CustomNewGameModeController.InvalidInputClass);
			}
			bool flag = false;
			MinMaxSpec<float> adultAgeProgress = gameMode.AdultAgeProgress;
			if (adultAgeProgress.Min > adultAgeProgress.Max)
			{
				this._minAdultAgeProgress.AddToClassList(CustomNewGameModeController.InvalidInputClass);
				this._maxAdultAgeProgress.AddToClassList(CustomNewGameModeController.InvalidInputClass);
				flag = true;
			}
			MinMaxSpec<float> childAgeProgress = gameMode.ChildAgeProgress;
			if (childAgeProgress.Min > childAgeProgress.Max)
			{
				this._minChildAgeProgress.AddToClassList(CustomNewGameModeController.InvalidInputClass);
				this._maxChildAgeProgress.AddToClassList(CustomNewGameModeController.InvalidInputClass);
				flag = true;
			}
			MinMaxSpec<int> temperateWeatherDuration = gameMode.TemperateWeatherDuration;
			if (temperateWeatherDuration.Min > temperateWeatherDuration.Max)
			{
				this._minTemperateWeatherDuration.AddToClassList(CustomNewGameModeController.InvalidInputClass);
				this._maxTemperateWeatherDuration.AddToClassList(CustomNewGameModeController.InvalidInputClass);
				flag = true;
			}
			if (this.IsDroughtEnabled)
			{
				MinMaxSpec<int> droughtDuration = gameMode.DroughtDuration;
				if (droughtDuration.Min > droughtDuration.Max)
				{
					this._minDroughtDuration.AddToClassList(CustomNewGameModeController.InvalidInputClass);
					this._maxDroughtDuration.AddToClassList(CustomNewGameModeController.InvalidInputClass);
					flag = true;
				}
			}
			if (this.IsBadtideEnabled)
			{
				MinMaxSpec<int> badtideDuration = gameMode.BadtideDuration;
				if (badtideDuration.Min > badtideDuration.Max)
				{
					this._minBadtideDuration.AddToClassList(CustomNewGameModeController.InvalidInputClass);
					this._maxBadtideDuration.AddToClassList(CustomNewGameModeController.InvalidInputClass);
					flag = true;
				}
			}
			return !flag;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B54 File Offset: 0x00000D54
		public void UpdateFieldVisibility()
		{
			this._droughtWrapper.ToggleDisplayStyle(this.IsDroughtEnabled);
			this._badtideWrapper.ToggleDisplayStyle(this.IsBadtideEnabled);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B78 File Offset: 0x00000D78
		public static MinMaxSpec<int> GetInts(IntegerField minIntegerField, IntegerField maxIntegerField)
		{
			return new MinMaxSpec<int>
			{
				Min = CustomNewGameModeController.GetInt(minIntegerField),
				Max = CustomNewGameModeController.GetInt(maxIntegerField)
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B97 File Offset: 0x00000D97
		public static int GetInt(IntegerField integerField)
		{
			return integerField.value;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B9F File Offset: 0x00000D9F
		public static MinMaxSpec<float> GetFloatsFromPercents(IntegerField minIntegerField, IntegerField maxIntegerField)
		{
			return new MinMaxSpec<float>
			{
				Min = CustomNewGameModeController.GetFloatFromPercent(minIntegerField),
				Max = CustomNewGameModeController.GetFloatFromPercent(maxIntegerField)
			};
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002BBE File Offset: 0x00000DBE
		public static float GetFloatFromPercent(IntegerField integerField)
		{
			return (float)CustomNewGameModeController.GetInt(integerField) / 100f;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002BCD File Offset: 0x00000DCD
		public static int FloatToPercent(float value)
		{
			return Mathf.RoundToInt(value * 100f);
		}

		// Token: 0x04000013 RID: 19
		public static readonly string InvalidInputClass = "new-game-mode-panel__setting-input--invalid";

		// Token: 0x04000014 RID: 20
		public readonly List<IntegerField> _integerFields = new List<IntegerField>();

		// Token: 0x04000015 RID: 21
		public Action _gameModeChangedCallback;

		// Token: 0x04000016 RID: 22
		public IntegerField _startingAdults;

		// Token: 0x04000017 RID: 23
		public IntegerField _minAdultAgeProgress;

		// Token: 0x04000018 RID: 24
		public IntegerField _maxAdultAgeProgress;

		// Token: 0x04000019 RID: 25
		public IntegerField _startingChildren;

		// Token: 0x0400001A RID: 26
		public IntegerField _minChildAgeProgress;

		// Token: 0x0400001B RID: 27
		public IntegerField _maxChildAgeProgress;

		// Token: 0x0400001C RID: 28
		public IntegerField _foodConsumption;

		// Token: 0x0400001D RID: 29
		public IntegerField _waterConsumption;

		// Token: 0x0400001E RID: 30
		public IntegerField _startingFood;

		// Token: 0x0400001F RID: 31
		public IntegerField _startingWater;

		// Token: 0x04000020 RID: 32
		public IntegerField _minTemperateWeatherDuration;

		// Token: 0x04000021 RID: 33
		public IntegerField _maxTemperateWeatherDuration;

		// Token: 0x04000022 RID: 34
		public Toggle _droughtEnabled;

		// Token: 0x04000023 RID: 35
		public VisualElement _droughtWrapper;

		// Token: 0x04000024 RID: 36
		public IntegerField _minDroughtDuration;

		// Token: 0x04000025 RID: 37
		public IntegerField _maxDroughtDuration;

		// Token: 0x04000026 RID: 38
		public IntegerField _droughtDurationHandicapMultiplier;

		// Token: 0x04000027 RID: 39
		public IntegerField _droughtDurationHandicapCycles;

		// Token: 0x04000028 RID: 40
		public Toggle _badtideEnabled;

		// Token: 0x04000029 RID: 41
		public VisualElement _badtideWrapper;

		// Token: 0x0400002A RID: 42
		public IntegerField _cyclesBeforeRandomizingBadtide;

		// Token: 0x0400002B RID: 43
		public IntegerField _chanceForBadtide;

		// Token: 0x0400002C RID: 44
		public IntegerField _minBadtideDuration;

		// Token: 0x0400002D RID: 45
		public IntegerField _maxBadtideDuration;

		// Token: 0x0400002E RID: 46
		public IntegerField _badtideDurationHandicapMultiplier;

		// Token: 0x0400002F RID: 47
		public IntegerField _badtideDurationHandicapCycles;

		// Token: 0x04000030 RID: 48
		public IntegerField _injuryChance;

		// Token: 0x04000031 RID: 49
		public IntegerField _demolishableRecoveryRate;
	}
}
