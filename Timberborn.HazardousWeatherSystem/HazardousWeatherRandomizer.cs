using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x0200000B RID: 11
	public class HazardousWeatherRandomizer
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000028D0 File Offset: 0x00000AD0
		public HazardousWeatherRandomizer(DroughtWeather droughtWeather, BadtideWeather badtideWeather, HazardousWeatherHistory hazardousWeatherHistory, IRandomNumberGenerator randomNumberGenerator)
		{
			this._droughtWeather = droughtWeather;
			this._badtideWeather = badtideWeather;
			this._hazardousWeatherHistory = hazardousWeatherHistory;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028F5 File Offset: 0x00000AF5
		public IHazardousWeather GetRandomWeatherForCycle(int cycle)
		{
			if (this.ShouldBeBadtideWeather(cycle))
			{
				return this._badtideWeather;
			}
			return this._droughtWeather;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000290D File Offset: 0x00000B0D
		public float BaseBadtideChance
		{
			get
			{
				return this._badtideWeather.ChanceForBadtide;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000291A File Offset: 0x00000B1A
		public bool IsBadtideStreak
		{
			get
			{
				return this._hazardousWeatherHistory.CurrentStreakId == this._badtideWeather.Id;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002937 File Offset: 0x00000B37
		public bool ShouldBeBadtideWeather(int cycle)
		{
			return this._badtideWeather.CanOccurAtCycle(cycle) && this._randomNumberGenerator.CheckProbability(this.GetBadtideChance());
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000295A File Offset: 0x00000B5A
		public float GetBadtideChance()
		{
			if (this._hazardousWeatherHistory.CurrentStreak <= 0)
			{
				return this.BaseBadtideChance;
			}
			return this.GetModifiedBadtideChance();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002978 File Offset: 0x00000B78
		public float GetModifiedBadtideChance()
		{
			float num = this.IsBadtideStreak ? this.BaseBadtideChance : (1f - this.BaseBadtideChance);
			float num2 = Mathf.Pow(num, (float)(this._hazardousWeatherHistory.CurrentStreak + 1));
			if (num2 < HazardousWeatherRandomizer.StreakChanceResetThreshold)
			{
				num = 0f;
			}
			else if (num2 < HazardousWeatherRandomizer.StreakChanceDecreaseThreshold)
			{
				num *= HazardousWeatherRandomizer.DecreaseRatio;
			}
			if (!this.IsBadtideStreak)
			{
				return 1f - num;
			}
			return num;
		}

		// Token: 0x04000032 RID: 50
		public static readonly float StreakChanceDecreaseThreshold = 0.05f;

		// Token: 0x04000033 RID: 51
		public static readonly float StreakChanceResetThreshold = 0.025f;

		// Token: 0x04000034 RID: 52
		public static readonly float DecreaseRatio = 0.5f;

		// Token: 0x04000035 RID: 53
		public readonly DroughtWeather _droughtWeather;

		// Token: 0x04000036 RID: 54
		public readonly BadtideWeather _badtideWeather;

		// Token: 0x04000037 RID: 55
		public readonly HazardousWeatherHistory _hazardousWeatherHistory;

		// Token: 0x04000038 RID: 56
		public readonly IRandomNumberGenerator _randomNumberGenerator;
	}
}
