using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.TickSystem;
using Timberborn.Workshops;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000017 RID: 23
	public class WorkshopSounds : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x060000AC RID: 172 RVA: 0x0000359B File Offset: 0x0000179B
		public void Awake()
		{
			this._buildingSounds = base.GetComponent<BuildingSounds>();
			this._workshop = base.GetComponent<Workshop>();
			base.DisableComponent();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000035BB File Offset: 0x000017BB
		public override void Tick()
		{
			this.UpdateSound();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000280E File Offset: 0x00000A0E
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002816 File Offset: 0x00000A16
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000035C3 File Offset: 0x000017C3
		public void UpdateSound()
		{
			this._buildingSounds.ToggleSound(this._workshop.CurrentlyWorking);
		}

		// Token: 0x04000038 RID: 56
		public BuildingSounds _buildingSounds;

		// Token: 0x04000039 RID: 57
		public Workshop _workshop;
	}
}
