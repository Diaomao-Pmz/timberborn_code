using System;
using Timberborn.Effects;
using Timberborn.NeedSpecs;
using UnityEngine;

namespace Timberborn.NeedSystem
{
	// Token: 0x02000004 RID: 4
	public class Need
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public NeedSpec NeedSpec { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public bool IsCritical { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		public float Points { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020E9 File Offset: 0x000002E9
		public bool Enabled { get; private set; } = true;

		// Token: 0x06000009 RID: 9 RVA: 0x000020F4 File Offset: 0x000002F4
		public Need(NeedSpec needSpec, float deltaTimeInHours)
		{
			this.NeedSpec = needSpec;
			this._isNeverPositive = this.NeedSpec.IsNeverPositive;
			this.IsCritical = this.NeedSpec.HasSpec<CriticalNeedSpec>();
			this.Reset();
			float num = this.NeedSpec.DailyDelta / 24f;
			this._pointsWarningThreshold = Math.Abs(num) * this.NeedSpec.HoursWarningThreshold;
			this._deltaPointsPerUpdate = num * deltaTimeInHours;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002177 File Offset: 0x00000377
		public bool IsInCriticalState
		{
			get
			{
				return this.IsCritical && !this.IsFavorable;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000218C File Offset: 0x0000038C
		public bool IsFavorable
		{
			get
			{
				if (!this._isNeverPositive)
				{
					return this.Points > 0f;
				}
				return this.Points == 0f;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021B1 File Offset: 0x000003B1
		public bool IsBelowWarningThreshold
		{
			get
			{
				return this.Points <= this._pointsWarningThreshold;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021C4 File Offset: 0x000003C4
		public bool IsActive
		{
			get
			{
				return this.Enabled && this.Points != 0f;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021E0 File Offset: 0x000003E0
		public bool IsAtMinimumPoints
		{
			get
			{
				return this.Points <= this.NeedSpec.MinimumValue;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021F8 File Offset: 0x000003F8
		public float PointsToMax
		{
			get
			{
				return this.NeedSpec.MaximumValue - this.Points;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000220C File Offset: 0x0000040C
		public int Wellbeing
		{
			get
			{
				if (!this.Enabled)
				{
					return 0;
				}
				return this.WellbeingInternal;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000221E File Offset: 0x0000041E
		public void Update()
		{
			if (this.ShouldBeUpdated)
			{
				if (!this._appliedEffectSinceLastUpdate)
				{
					this.AddPoints(this._deltaPointsPerUpdate);
				}
				this._appliedEffectSinceLastUpdate = false;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002244 File Offset: 0x00000444
		public void Apply(in Effect effect)
		{
			float points;
			if (this.TryRawAppraise(effect, out points))
			{
				this.AddPoints(points);
				this._appliedEffectSinceLastUpdate = true;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000226C File Offset: 0x0000046C
		public bool TryAppraise(Effect effect, out float points)
		{
			float val;
			if (this.TryRawAppraise(effect, out val))
			{
				points = this.ApplyAttractiveness(Math.Min(val, this.PointsToMax));
				return true;
			}
			points = 0f;
			return false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022A4 File Offset: 0x000004A4
		public float EffectiveDurationInHours(ContinuousEffect effect)
		{
			float num = effect.PointsPerHour * this.NeedSpec.Effectiveness;
			return (this.PointsToMax - 0.01f) / num;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022D4 File Offset: 0x000004D4
		public void SetPoints(float points)
		{
			if (points < this.NeedSpec.MinimumValue)
			{
				this.Points = this.NeedSpec.MinimumValue;
				return;
			}
			this.Points = ((points > this.NeedSpec.MaximumValue) ? this.NeedSpec.MaximumValue : points);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002323 File Offset: 0x00000523
		public void EnableUpdate()
		{
			this._updateEnabled = true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000232C File Offset: 0x0000052C
		public void DisableUpdate()
		{
			this._updateEnabled = false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002335 File Offset: 0x00000535
		public void EnableNeed()
		{
			this.Enabled = true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000233E File Offset: 0x0000053E
		public void DisableNeed()
		{
			this.Enabled = false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002347 File Offset: 0x00000547
		public void Reset()
		{
			this.SetPoints(this.NeedSpec.StartingValue);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000235A File Offset: 0x0000055A
		public bool ShouldBeUpdated
		{
			get
			{
				return this._updateEnabled && this.Enabled;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000236C File Offset: 0x0000056C
		public int WellbeingInternal
		{
			get
			{
				if (!this.IsFavorable)
				{
					return this.NeedSpec.GetUnfavorableWellbeing();
				}
				return this.NeedSpec.GetFavorableWellbeing();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000238D File Offset: 0x0000058D
		public float PointToMin
		{
			get
			{
				return this.Points - this.NeedSpec.MinimumValue;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023A1 File Offset: 0x000005A1
		public void AddPoints(float points)
		{
			this.SetPoints(this.Points + points);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023B4 File Offset: 0x000005B4
		public bool TryRawAppraise(in Effect effect, out float points)
		{
			if (!this.Enabled)
			{
				points = 0f;
				return true;
			}
			float num = effect.Points * this.NeedSpec.Effectiveness;
			int num2 = this.NonWastingEffectCount(num, effect.Count);
			if (num2 > 0)
			{
				points = num * (float)num2;
				return true;
			}
			points = 0f;
			return false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002408 File Offset: 0x00000608
		public int NonWastingEffectCount(float effectPoints, int effectCount)
		{
			if (this.NeedSpec.Wastable)
			{
				return effectCount;
			}
			int val = Mathf.FloorToInt((effectPoints > 0f) ? (this.PointsToMax / effectPoints) : (-this.PointToMin / effectPoints));
			return Math.Min(effectCount, val);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000244C File Offset: 0x0000064C
		public float ApplyAttractiveness(float points)
		{
			float num = 1f + this.PointsToMax * 0.1f;
			return points * this.NeedSpec.ImportanceMultiplier * num;
		}

		// Token: 0x0400000A RID: 10
		public readonly float _deltaPointsPerUpdate;

		// Token: 0x0400000B RID: 11
		public readonly float _pointsWarningThreshold;

		// Token: 0x0400000C RID: 12
		public bool _appliedEffectSinceLastUpdate;

		// Token: 0x0400000D RID: 13
		public bool _updateEnabled = true;

		// Token: 0x0400000E RID: 14
		public readonly bool _isNeverPositive;
	}
}
