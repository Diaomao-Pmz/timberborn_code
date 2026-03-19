using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.GameWaterSourceSystem
{
	// Token: 0x0200000E RID: 14
	public class WaterSourceActivatorOverrider : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002584 File Offset: 0x00000784
		public void Awake()
		{
			this._waterSourceRegulator = base.GetComponent<WaterSourceRegulator>();
			this._underlyingWaterSource = base.GetComponent<UnderlyingWaterSource>();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000025A0 File Offset: 0x000007A0
		public void OnEnterFinishedState()
		{
			WaterSource waterSource = this._underlyingWaterSource.WaterSource;
			if (waterSource != null)
			{
				this._waterSourceActivator = waterSource.GetComponent<WaterSourceActivator>();
				this._waterSourceRegulator.OpenStateChanged += this.OnOpenStateChanged;
				this.UpdateActivatorForcedState(this._waterSourceRegulator.IsOpen);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025F0 File Offset: 0x000007F0
		public void OnExitFinishedState()
		{
			if (this._underlyingWaterSource.WaterSource)
			{
				this._waterSourceRegulator.OpenStateChanged -= this.OnOpenStateChanged;
				this.UpdateActivatorForcedState(false);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002622 File Offset: 0x00000822
		public void OnOpenStateChanged(object sender, bool isOpen)
		{
			this.UpdateActivatorForcedState(isOpen);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000262B File Offset: 0x0000082B
		public void UpdateActivatorForcedState(bool isOpen)
		{
			if (isOpen)
			{
				this._waterSourceActivator.ForceActive();
				return;
			}
			this._waterSourceActivator.DisableForceActive();
		}

		// Token: 0x0400001A RID: 26
		public WaterSourceRegulator _waterSourceRegulator;

		// Token: 0x0400001B RID: 27
		public UnderlyingWaterSource _underlyingWaterSource;

		// Token: 0x0400001C RID: 28
		public WaterSourceActivator _waterSourceActivator;
	}
}
