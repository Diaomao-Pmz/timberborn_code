using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.TickSystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.GameWaterSourceSystem
{
	// Token: 0x0200000B RID: 11
	public class UndergroundWaterSourceDrillSounds : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000023EA File Offset: 0x000005EA
		public void Awake()
		{
			this._buildingSounds = base.GetComponent<BuildingSounds>();
			this._underlyingWaterSource = base.GetComponent<UnderlyingWaterSource>();
			base.DisableComponent();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000240A File Offset: 0x0000060A
		public override void Tick()
		{
			this.UpdateSound();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002412 File Offset: 0x00000612
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000241A File Offset: 0x0000061A
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002422 File Offset: 0x00000622
		public void UpdateSound()
		{
			this._buildingSounds.ToggleSound(this._underlyingWaterSource.WaterSource.CurrentStrength > 0f);
		}

		// Token: 0x04000015 RID: 21
		public BuildingSounds _buildingSounds;

		// Token: 0x04000016 RID: 22
		public UnderlyingWaterSource _underlyingWaterSource;
	}
}
