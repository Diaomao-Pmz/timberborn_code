using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using UnityEngine;

namespace Timberborn.Hauling
{
	// Token: 0x02000009 RID: 9
	public class HaulCandidate : BaseComponent, IAwakableComponent, IRegisteredComponent, IFinishedStateListener
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000014 RID: 20 RVA: 0x000023B4 File Offset: 0x000005B4
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x000023EC File Offset: 0x000005EC
		public event EventHandler InHaulingCenterRangeChanged;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002421 File Offset: 0x00000621
		public bool IsInHaulingCenterRange
		{
			get
			{
				return this._districtHaulCandidates && this._districtHaulCandidates.HasHaulingCenter;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000243D File Offset: 0x0000063D
		public void Awake()
		{
			this._haulPrioritizable = base.GetComponent<HaulPrioritizable>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			base.GetComponents<IHaulBehaviorProvider>(this._providers);
			base.DisableComponent();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002469 File Offset: 0x00000669
		public void OnEnterFinishedState()
		{
			if (this._districtBuilding)
			{
				base.EnableComponent();
				this._districtBuilding.ReassignedDistrict += this.OnReassignedDistrict;
				this.SetDistrictHaulCandidates();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000249B File Offset: 0x0000069B
		public void OnExitFinishedState()
		{
			if (this._districtBuilding)
			{
				base.DisableComponent();
				this._districtBuilding.ReassignedDistrict -= this.OnReassignedDistrict;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024C8 File Offset: 0x000006C8
		public void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors)
		{
			foreach (IHaulBehaviorProvider haulBehaviorProvider in this._providers)
			{
				haulBehaviorProvider.GetWeightedBehaviors(this._weightedBehaviorsCache);
				foreach (WeightedBehavior weightedBehavior in this._weightedBehaviorsCache)
				{
					weightedBehaviors.Add(new WeightedBehavior(this.PrioritizeAndValidate(weightedBehavior.Weight), weightedBehavior.WorkplaceBehavior));
				}
				this._weightedBehaviorsCache.Clear();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002584 File Offset: 0x00000784
		public void OnReassignedDistrict(object sender, EventArgs e)
		{
			this.ClearDistrictHaulCandidates();
			this.SetDistrictHaulCandidates();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002592 File Offset: 0x00000792
		public void ClearDistrictHaulCandidates()
		{
			if (this._districtHaulCandidates)
			{
				this._districtHaulCandidates.HasHaulingCenterChanged -= this.OnHasHaulingCenterChanged;
				this._districtHaulCandidates = null;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025C0 File Offset: 0x000007C0
		public void SetDistrictHaulCandidates()
		{
			DistrictCenter district = this._districtBuilding.District;
			if (district)
			{
				this._districtHaulCandidates = district.GetComponent<DistrictHaulCandidates>();
				this._districtHaulCandidates.HasHaulingCenterChanged += this.OnHasHaulingCenterChanged;
				this.InvokeInHaulingCenterRangeChanged();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000260A File Offset: 0x0000080A
		public void OnHasHaulingCenterChanged(object sender, EventArgs e)
		{
			this.InvokeInHaulingCenterRangeChanged();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002612 File Offset: 0x00000812
		public void InvokeInHaulingCenterRangeChanged()
		{
			EventHandler inHaulingCenterRangeChanged = this.InHaulingCenterRangeChanged;
			if (inHaulingCenterRangeChanged == null)
			{
				return;
			}
			inHaulingCenterRangeChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000262A File Offset: 0x0000082A
		public float PrioritizeAndValidate(float weight)
		{
			if (weight < 0f || weight > 1f)
			{
				Debug.LogWarning("weight should be between 0 and 1!");
			}
			if (this._haulPrioritizable.Prioritized && (double)weight >= 0.5)
			{
				return weight + HaulCandidate.PriorityFactor;
			}
			return weight;
		}

		// Token: 0x0400000E RID: 14
		public static readonly float PriorityFactor = 0.5f;

		// Token: 0x04000010 RID: 16
		public HaulPrioritizable _haulPrioritizable;

		// Token: 0x04000011 RID: 17
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000012 RID: 18
		public DistrictHaulCandidates _districtHaulCandidates;

		// Token: 0x04000013 RID: 19
		public readonly List<IHaulBehaviorProvider> _providers = new List<IHaulBehaviorProvider>();

		// Token: 0x04000014 RID: 20
		public readonly List<WeightedBehavior> _weightedBehaviorsCache = new List<WeightedBehavior>();
	}
}
