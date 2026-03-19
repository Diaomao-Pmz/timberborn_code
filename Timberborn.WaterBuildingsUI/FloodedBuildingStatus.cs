using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using Timberborn.WaterObjects;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000009 RID: 9
	public class FloodedBuildingStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000027E0 File Offset: 0x000009E0
		public FloodedBuildingStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027F0 File Offset: 0x000009F0
		public void Awake()
		{
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("FloodedBuilding", this._loc.T(FloodedBuildingStatus.FloodedLocKey), this._loc.T(FloodedBuildingStatus.FloodedShortLocKey), 0f);
			base.GetComponent<FloodableObject>().Flooded += delegate(object _, EventArgs _)
			{
				this._statusToggle.Activate();
			};
			base.GetComponent<FloodableObject>().Unflooded += delegate(object _, EventArgs _)
			{
				this._statusToggle.Deactivate();
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002860 File Offset: 0x00000A60
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x04000021 RID: 33
		public static readonly string FloodedLocKey = "Status.Buildings.Flooded";

		// Token: 0x04000022 RID: 34
		public static readonly string FloodedShortLocKey = "Status.Buildings.Flooded.Short";

		// Token: 0x04000023 RID: 35
		public readonly ILoc _loc;

		// Token: 0x04000024 RID: 36
		public StatusToggle _statusToggle;
	}
}
