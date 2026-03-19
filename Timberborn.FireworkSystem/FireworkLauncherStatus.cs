using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;
using Timberborn.Hauling;
using Timberborn.InventorySystem;
using Timberborn.Workshops;

namespace Timberborn.FireworkSystem
{
	// Token: 0x0200000C RID: 12
	public class FireworkLauncherStatus : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002FD9 File Offset: 0x000011D9
		public void Awake()
		{
			this._fireworkLauncher = base.GetComponent<FireworkLauncher>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._noHaulingPostStatus = base.GetComponent<NoHaulingPostStatus>();
			this._lackOfResourcesStatus = base.GetComponent<LackOfResourcesStatus>();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000300C File Offset: 0x0000120C
		public void OnEnterFinishedState()
		{
			this._lackOfResourcesStatus.Initialize(new Func<bool>(this.CanShowLackOfResourcesStatus));
			this._noHaulingPostStatus.Initialize(() => true);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000305A File Offset: 0x0000125A
		public void OnExitFinishedState()
		{
			this._lackOfResourcesStatus.Disable();
			this._noHaulingPostStatus.Disable();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003072 File Offset: 0x00001272
		public bool CanShowLackOfResourcesStatus()
		{
			Inventory inventory = this._fireworkLauncher.Inventory;
			return inventory != null && inventory.IsEmpty && this._districtBuilding.District;
		}

		// Token: 0x04000044 RID: 68
		public FireworkLauncher _fireworkLauncher;

		// Token: 0x04000045 RID: 69
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000046 RID: 70
		public NoHaulingPostStatus _noHaulingPostStatus;

		// Token: 0x04000047 RID: 71
		public LackOfResourcesStatus _lackOfResourcesStatus;
	}
}
