using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;

namespace Timberborn.Wellbeing
{
	// Token: 0x0200001C RID: 28
	public class WellbeingTrackerRegistrar : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000031A2 File Offset: 0x000013A2
		public WellbeingTrackerRegistrar(GlobalWellbeingTrackerRegistry globalWellbeingTrackerRegistry)
		{
			this._globalWellbeingTrackerRegistry = globalWellbeingTrackerRegistry;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000031B1 File Offset: 0x000013B1
		public void Awake()
		{
			this._wellbeingTracker = base.GetComponent<WellbeingTracker>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000031CC File Offset: 0x000013CC
		public void InitializeEntity()
		{
			Character component = base.GetComponent<Character>();
			if (component != null && component.Alive)
			{
				this._globalWellbeingTrackerRegistry.Registry.Register(this._wellbeingTracker);
				this._citizen.ChangedAssignedDistrict += this.OnChangedAssignedDistrict;
				component.Died += this.OnDied;
				this.UpdateDistrictWellbeingTrackerRegistry();
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003230 File Offset: 0x00001430
		public void OnDied(object sender, EventArgs e)
		{
			this._globalWellbeingTrackerRegistry.Registry.Unregister(this._wellbeingTracker);
			DistrictWellbeingTrackerRegistry districtWellbeingTrackerRegistry = this._districtWellbeingTrackerRegistry;
			if (districtWellbeingTrackerRegistry == null)
			{
				return;
			}
			districtWellbeingTrackerRegistry.Registry.Unregister(this._wellbeingTracker);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003263 File Offset: 0x00001463
		public void OnChangedAssignedDistrict(object sender, ChangeAssignedDistrictEventArgs e)
		{
			this.UpdateDistrictWellbeingTrackerRegistry();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000326C File Offset: 0x0000146C
		public void UpdateDistrictWellbeingTrackerRegistry()
		{
			DistrictWellbeingTrackerRegistry districtWellbeingTrackerRegistry = this._districtWellbeingTrackerRegistry;
			if (districtWellbeingTrackerRegistry != null)
			{
				districtWellbeingTrackerRegistry.Registry.Unregister(this._wellbeingTracker);
			}
			DistrictCenter assignedDistrict = this._citizen.AssignedDistrict;
			if (assignedDistrict != null)
			{
				this._districtWellbeingTrackerRegistry = assignedDistrict.GetComponent<DistrictWellbeingTrackerRegistry>();
				this._districtWellbeingTrackerRegistry.Registry.Register(this._wellbeingTracker);
			}
		}

		// Token: 0x0400003D RID: 61
		public readonly GlobalWellbeingTrackerRegistry _globalWellbeingTrackerRegistry;

		// Token: 0x0400003E RID: 62
		public WellbeingTracker _wellbeingTracker;

		// Token: 0x0400003F RID: 63
		public Citizen _citizen;

		// Token: 0x04000040 RID: 64
		public DistrictWellbeingTrackerRegistry _districtWellbeingTrackerRegistry;
	}
}
