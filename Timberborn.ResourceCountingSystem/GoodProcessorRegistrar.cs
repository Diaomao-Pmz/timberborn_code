using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x02000006 RID: 6
	public class GoodProcessorRegistrar : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002455 File Offset: 0x00000655
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._goodProcessor = base.GetComponent<IGoodProcessor>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000246F File Offset: 0x0000066F
		public void OnEnterFinishedState()
		{
			if (this._districtBuilding)
			{
				this.AddRegisteredGoodProcessor();
				this._districtBuilding.ReassignedDistrict += this.OnReassignedDistrict;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000249B File Offset: 0x0000069B
		public void OnExitFinishedState()
		{
			if (this._districtBuilding)
			{
				this.RemoveRegisteredGoodProcessor();
				this._districtBuilding.ReassignedDistrict -= this.OnReassignedDistrict;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024C7 File Offset: 0x000006C7
		public void OnReassignedDistrict(object sender, EventArgs e)
		{
			this.RemoveRegisteredGoodProcessor();
			this.AddRegisteredGoodProcessor();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024D5 File Offset: 0x000006D5
		public void RemoveRegisteredGoodProcessor()
		{
			if (this._districtResourceCounter)
			{
				this._districtResourceCounter.Remove(this._goodProcessor);
				this._districtResourceCounter = null;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024FC File Offset: 0x000006FC
		public void AddRegisteredGoodProcessor()
		{
			DistrictBuilding districtBuilding = this._districtBuilding;
			DistrictCenter districtCenter = (districtBuilding != null) ? districtBuilding.District : null;
			if (districtCenter)
			{
				this._districtResourceCounter = districtCenter.GetComponent<DistrictResourceCounter>();
				this._districtResourceCounter.Add(this._goodProcessor);
			}
		}

		// Token: 0x04000010 RID: 16
		public IGoodProcessor _goodProcessor;

		// Token: 0x04000011 RID: 17
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000012 RID: 18
		public DistrictResourceCounter _districtResourceCounter;
	}
}
