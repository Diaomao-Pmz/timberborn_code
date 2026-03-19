using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200001A RID: 26
	public class WaterSourceDisabler : BaseComponent, IAwakableComponent, IWaterStrengthModifier, IFinishedStateListener
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00003655 File Offset: 0x00001855
		public void Awake()
		{
			this._underlyingWaterSource = base.GetComponent<UnderlyingWaterSource>();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003663 File Offset: 0x00001863
		public float GetStrengthModifier()
		{
			return 0f;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000366A File Offset: 0x0000186A
		public void OnEnterFinishedState()
		{
			this._underlyingWaterSource.AddWaterStrengthModifier(this);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003678 File Offset: 0x00001878
		public void OnExitFinishedState()
		{
			this._underlyingWaterSource.RemoveWaterStrengthModifier(this);
		}

		// Token: 0x04000048 RID: 72
		public UnderlyingWaterSource _underlyingWaterSource;
	}
}
