using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.WorkSystem;

namespace Timberborn.Hauling
{
	// Token: 0x0200000C RID: 12
	public class HaulingCenter : BaseComponent, IAwakableComponent, IFinishedStateListener, IRegisteredComponent
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002766 File Offset: 0x00000966
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			base.DisableComponent();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000277A File Offset: 0x0000097A
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002782 File Offset: 0x00000982
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000278C File Offset: 0x0000098C
		public void GetWorkplaceBehaviorsOrdered(IList<WorkplaceBehavior> workplaceBehaviors)
		{
			DistrictCenter district = this._districtBuilding.District;
			if (district)
			{
				district.GetComponent<DistrictHaulCandidates>().GetWorkplaceBehaviorsOrdered(workplaceBehaviors);
			}
		}

		// Token: 0x04000017 RID: 23
		public DistrictBuilding _districtBuilding;
	}
}
