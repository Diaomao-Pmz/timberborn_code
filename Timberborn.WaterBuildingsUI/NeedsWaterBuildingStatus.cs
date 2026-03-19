using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using Timberborn.WaterBuildings;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200000B RID: 11
	public class NeedsWaterBuildingStatus : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002EFA File Offset: 0x000010FA
		public NeedsWaterBuildingStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002F0C File Offset: 0x0000110C
		public void Awake()
		{
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("BuildingNeedsWater", this._loc.T(NeedsWaterBuildingStatus.NeedsWaterLocKey), this._loc.T(NeedsWaterBuildingStatus.NeedsWaterShortLocKey), 0f);
			this._tickableWaterBuilding = base.GetComponent<IWaterNeedingBuilding>();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002F5C File Offset: 0x0000115C
		public void InitializeEntity()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
			this._tickableWaterBuilding.StartedNeedingWater += this.OnStartedNeedingWater;
			this._tickableWaterBuilding.StoppedNeedingWater += this.OnStoppedNeedingWater;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002FA8 File Offset: 0x000011A8
		public void OnStartedNeedingWater(object o, EventArgs eventArgs)
		{
			this._statusToggle.Activate();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002FB5 File Offset: 0x000011B5
		public void OnStoppedNeedingWater(object o, EventArgs eventArgs)
		{
			this._statusToggle.Deactivate();
		}

		// Token: 0x0400003B RID: 59
		public static readonly string NeedsWaterLocKey = "Status.Buildings.NeedsWater";

		// Token: 0x0400003C RID: 60
		public static readonly string NeedsWaterShortLocKey = "Status.Buildings.NeedsWater.Short";

		// Token: 0x0400003D RID: 61
		public readonly ILoc _loc;

		// Token: 0x0400003E RID: 62
		public StatusToggle _statusToggle;

		// Token: 0x0400003F RID: 63
		public IWaterNeedingBuilding _tickableWaterBuilding;
	}
}
