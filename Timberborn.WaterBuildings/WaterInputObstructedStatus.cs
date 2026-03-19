using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000031 RID: 49
	public class WaterInputObstructedStatus : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00007252 File Offset: 0x00005452
		public WaterInputObstructedStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007261 File Offset: 0x00005461
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._statusToggle = StatusToggle.CreateNormalStatusWithFloatingIcon("BuildingNeedsWater", this._loc.T(WaterInputObstructedStatus.PipeObstructedLocKey), 0f);
			this._waterInputCoordinates = base.GetComponent<WaterInputCoordinates>();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000072A0 File Offset: 0x000054A0
		public void InitializeEntity()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
			base.GetComponent<WaterInputCoordinates>().CoordinatesChanged += delegate(object _, Vector3Int _)
			{
				this.OnCoordinatesChanged();
			};
			this.OnCoordinatesChanged();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000072D0 File Offset: 0x000054D0
		public void OnCoordinatesChanged()
		{
			if (this._blockObject.IsFinished)
			{
				if (!this._statusToggle.IsActive && this._waterInputCoordinates.Depth == 0 && !this.IsWaterInputLimitedToZero())
				{
					this._statusToggle.Activate();
					return;
				}
				if ((this._statusToggle.IsActive && this._waterInputCoordinates.Depth != 0) || this.IsWaterInputLimitedToZero())
				{
					this._statusToggle.Deactivate();
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007345 File Offset: 0x00005545
		public bool IsWaterInputLimitedToZero()
		{
			return this._waterInputCoordinates.UseDepthLimit && this._waterInputCoordinates.DepthLimit == 0;
		}

		// Token: 0x040000DF RID: 223
		public static readonly string PipeObstructedLocKey = "Status.Buildings.PipeObstructed";

		// Token: 0x040000E0 RID: 224
		public readonly ILoc _loc;

		// Token: 0x040000E1 RID: 225
		public BlockObject _blockObject;

		// Token: 0x040000E2 RID: 226
		public StatusToggle _statusToggle;

		// Token: 0x040000E3 RID: 227
		public WaterInputCoordinates _waterInputCoordinates;
	}
}
