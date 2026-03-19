using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200001C RID: 28
	public class WaterSourceDischarger : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003716 File Offset: 0x00001916
		public void Awake()
		{
			this._underlyingWaterSource = base.GetComponent<UnderlyingWaterSource>();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003724 File Offset: 0x00001924
		public void OnEnterFinishedState()
		{
			this._underlyingWaterSource.DisableDroughtInfluence();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003731 File Offset: 0x00001931
		public void OnExitFinishedState()
		{
			this._underlyingWaterSource.EnableDroughtInfluence();
		}

		// Token: 0x04000049 RID: 73
		public UnderlyingWaterSource _underlyingWaterSource;
	}
}
