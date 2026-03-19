using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Illumination;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000010 RID: 16
	public class DistrictBuildingIlluminator : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002FA8 File Offset: 0x000011A8
		public void Awake()
		{
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FC7 File Offset: 0x000011C7
		public void OnEnterFinishedState()
		{
			this._districtBuilding.ReassignedInstantDistrict += this.OnReassignedInstantDistrict;
			this.UpdateIlluminator();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FE6 File Offset: 0x000011E6
		public void OnExitFinishedState()
		{
			this._districtBuilding.ReassignedInstantDistrict -= this.OnReassignedInstantDistrict;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FFF File Offset: 0x000011FF
		public void OnReassignedInstantDistrict(object sender, EventArgs e)
		{
			this.UpdateIlluminator();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003007 File Offset: 0x00001207
		public void UpdateIlluminator()
		{
			if (this._districtBuilding.InstantDistrict)
			{
				this._illuminatorToggle.TurnOn();
				return;
			}
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x0400002D RID: 45
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x0400002E RID: 46
		public DistrictBuilding _districtBuilding;
	}
}
