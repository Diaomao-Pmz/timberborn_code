using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Effects;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.NeedBehaviorSystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.TimeSystem;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.Attractions
{
	// Token: 0x0200000D RID: 13
	public class AttractionNeedBehavior : NeedBehavior, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002964 File Offset: 0x00000B64
		public AttractionNeedBehavior(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000297E File Offset: 0x00000B7E
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._attraction = base.GetComponent<Attraction>();
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			base.DisableComponent();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029B8 File Offset: 0x00000BB8
		public override Vector3? ActionPosition(NeedManager needManager)
		{
			Enterer component = needManager.GetComponent<Enterer>();
			if (this._attraction.IsUsable && (this._enterable.CanReserveSlot || component.CurrentBuilding == this._enterable))
			{
				Vector3? unblockedSingleAccess = this._buildingAccessible.Accessible.UnblockedSingleAccess;
				if (unblockedSingleAccess != null)
				{
					Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
					return new Vector3?(valueOrDefault);
				}
			}
			return null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A28 File Offset: 0x00000C28
		public void OnEnterFinishedState()
		{
			this._districtBuilding.ReassignedDistrict += this.OnReassignedDistrict;
			base.EnableComponent();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A47 File Offset: 0x00000C47
		public void OnExitFinishedState()
		{
			this._districtBuilding.ReassignedDistrict -= this.OnReassignedDistrict;
			this.RemoveDistrictNeedBehaviorService();
			base.DisableComponent();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A6C File Offset: 0x00000C6C
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this._attraction.IsUsable || !base.Enabled || this.MaxValueNeedsAreActive(agent))
			{
				return Decision.ReleaseNextTick();
			}
			WalkInsideExecutor component = agent.GetComponent<WalkInsideExecutor>();
			AttractionAttender component2 = agent.GetComponent<AttractionAttender>();
			switch (component.Launch(this._enterable))
			{
			case ExecutorStatus.Success:
			{
				ApplyEffectExecutor component3 = agent.GetComponent<ApplyEffectExecutor>();
				if (component2.FirstVisit)
				{
					component2.FirstVisit = false;
					return this.ApplyEffect(component3, AttractionNeedBehavior.MinHoursSpentInside);
				}
				return this.ApplyEffect(component3, 0.05f);
			}
			case ExecutorStatus.Failure:
				component2.FirstVisit = true;
				return Decision.ReleaseNextTick();
			case ExecutorStatus.Running:
				component2.FirstVisit = true;
				return Decision.ReturnWhenFinished(component);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B1C File Offset: 0x00000D1C
		public Decision ApplyEffect(ApplyEffectExecutor applyEffectExecutor, float lengthOfStayInHours)
		{
			float timestamp = this._dayNightCycle.DayNumberHoursFromNow(lengthOfStayInHours);
			this._attraction.GetEfficiencyAdjustedEffects(this._effects);
			applyEffectExecutor.LaunchToTimestamp(this._effects, timestamp, null);
			this._effects.Clear();
			if (!this._attraction.SatisfiesAnyNeedToMaxValue)
			{
				return Decision.ReleaseWhenFinished(applyEffectExecutor);
			}
			return Decision.ReturnWhenFinished(applyEffectExecutor);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B7C File Offset: 0x00000D7C
		public void OnReassignedDistrict(object sender, EventArgs e)
		{
			this.RemoveDistrictNeedBehaviorService();
			DistrictCenter district = this._districtBuilding.District;
			if (district)
			{
				this._districtNeedBehaviorService = district.GetComponent<DistrictNeedBehaviorService>();
				this._districtNeedBehaviorService.AddNeedBehavior(this._attraction.Effects, this);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BC6 File Offset: 0x00000DC6
		public void RemoveDistrictNeedBehaviorService()
		{
			if (this._districtNeedBehaviorService)
			{
				this._districtNeedBehaviorService.RemoveNeedBehavior(this._attraction.Effects, this);
				this._districtNeedBehaviorService = null;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public bool MaxValueNeedsAreActive(BehaviorAgent agent)
		{
			NeedManager component = agent.GetComponent<NeedManager>();
			bool result = false;
			IReadOnlyList<ContinuousEffectSpec> effects = this._attraction.Effects;
			for (int i = 0; i < effects.Count; i++)
			{
				ContinuousEffectSpec continuousEffectSpec = effects[i];
				if (continuousEffectSpec.SatisfyToMaxValue)
				{
					if (component.NeedPointsToMax(continuousEffectSpec.NeedId) > AttractionNeedBehavior.MaxValueTolerance)
					{
						return false;
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000026 RID: 38
		public static readonly float MaxValueTolerance = 0.001f;

		// Token: 0x04000027 RID: 39
		public static readonly float MinHoursSpentInside = 0.5f;

		// Token: 0x04000028 RID: 40
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000029 RID: 41
		public Enterable _enterable;

		// Token: 0x0400002A RID: 42
		public Attraction _attraction;

		// Token: 0x0400002B RID: 43
		public BuildingAccessible _buildingAccessible;

		// Token: 0x0400002C RID: 44
		public DistrictBuilding _districtBuilding;

		// Token: 0x0400002D RID: 45
		public DistrictNeedBehaviorService _districtNeedBehaviorService;

		// Token: 0x0400002E RID: 46
		public readonly List<ContinuousEffect> _effects = new List<ContinuousEffect>();
	}
}
