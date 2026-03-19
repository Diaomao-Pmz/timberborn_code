using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.WorkSystem;

namespace Timberborn.Hauling
{
	// Token: 0x02000007 RID: 7
	public class DistrictHaulCandidates : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler HasHaulingCenterChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		public bool HasHaulingCenter
		{
			get
			{
				return this._haulingCenters.Count > 0;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217D File Offset: 0x0000037D
		public void Awake()
		{
			DistrictBuildingRegistry component = base.GetComponent<DistrictBuildingRegistry>();
			component.FinishedBuildingRegistered += this.OnFinishedBuildingRegistered;
			component.FinishedBuildingUnregistered += this.OnFinishedBuildingUnregistered;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021A8 File Offset: 0x000003A8
		public void GetWorkplaceBehaviorsOrdered(IList<WorkplaceBehavior> workplaceBehaviors)
		{
			foreach (HaulCandidate haulCandidate in this._haulCandidates)
			{
				haulCandidate.GetWeightedBehaviors(this._weightedBehaviors);
			}
			this._weightedBehaviors.Sort((WeightedBehavior a, WeightedBehavior b) => b.Weight.CompareTo(a.Weight));
			foreach (WeightedBehavior weightedBehavior in this._weightedBehaviors)
			{
				workplaceBehaviors.Add(weightedBehavior.WorkplaceBehavior);
			}
			this._weightedBehaviors.Clear();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000227C File Offset: 0x0000047C
		public void OnFinishedBuildingRegistered(object sender, FinishedBuildingRegisteredEventArgs e)
		{
			EntityComponent building = e.Building;
			HaulCandidate component = building.GetComponent<HaulCandidate>();
			if (component != null)
			{
				this._haulCandidates.Add(component);
			}
			HaulingCenter component2 = building.GetComponent<HaulingCenter>();
			if (component2 != null)
			{
				this.Add(component2);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022B8 File Offset: 0x000004B8
		public void OnFinishedBuildingUnregistered(object sender, FinishedBuildingUnregisteredEventArgs e)
		{
			EntityComponent building = e.Building;
			HaulCandidate component = building.GetComponent<HaulCandidate>();
			if (component != null)
			{
				this._haulCandidates.Remove(component);
			}
			HaulingCenter component2 = building.GetComponent<HaulingCenter>();
			if (component2 != null)
			{
				this.Remove(component2);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022F2 File Offset: 0x000004F2
		public void Add(HaulingCenter haulingCenter)
		{
			if (this._haulingCenters.Add(haulingCenter) && this._haulingCenters.Count == 1)
			{
				EventHandler hasHaulingCenterChanged = this.HasHaulingCenterChanged;
				if (hasHaulingCenterChanged == null)
				{
					return;
				}
				hasHaulingCenterChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002326 File Offset: 0x00000526
		public void Remove(HaulingCenter haulingCenter)
		{
			if (this._haulingCenters.Remove(haulingCenter) && this._haulingCenters.Count == 0)
			{
				EventHandler hasHaulingCenterChanged = this.HasHaulingCenterChanged;
				if (hasHaulingCenterChanged == null)
				{
					return;
				}
				hasHaulingCenterChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly HashSet<HaulCandidate> _haulCandidates = new HashSet<HaulCandidate>();

		// Token: 0x0400000A RID: 10
		public readonly HashSet<HaulingCenter> _haulingCenters = new HashSet<HaulingCenter>();

		// Token: 0x0400000B RID: 11
		public readonly List<WeightedBehavior> _weightedBehaviors = new List<WeightedBehavior>();
	}
}
