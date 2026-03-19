using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;

namespace Timberborn.LinkedBuildingSystem
{
	// Token: 0x0200000E RID: 14
	public class LinkedPausableConstructionSite : BaseComponent, IAwakableComponent, IUnfinishedStateListener
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002CAF File Offset: 0x00000EAF
		public void Awake()
		{
			this._pausableBuilding = base.GetComponent<PausableBuilding>();
			base.GetComponent<LinkedBuilding>().BuildingLinked += this.OnBuildingLinked;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public void OnEnterUnfinishedState()
		{
			this._pausableBuilding.PausedChanged += this.OnPausedChanged;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002CED File Offset: 0x00000EED
		public void OnExitUnfinishedState()
		{
			this._pausableBuilding.PausedChanged -= this.OnPausedChanged;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D06 File Offset: 0x00000F06
		public void OnBuildingLinked(object sender, LinkedBuilding e)
		{
			this._linked = e.GetComponent<LinkedPausableConstructionSite>();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D14 File Offset: 0x00000F14
		public void OnPausedChanged(object sender, EventArgs e)
		{
			if (this._mirrorOperationLock.IsUnlocked)
			{
				using (this._mirrorOperationLock.Lock())
				{
					this._linked.MirrorPauseState();
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D64 File Offset: 0x00000F64
		public void MirrorPauseState()
		{
			if (this._linked._pausableBuilding.Paused)
			{
				this._pausableBuilding.Pause();
				return;
			}
			this._pausableBuilding.Resume();
		}

		// Token: 0x0400001D RID: 29
		public PausableBuilding _pausableBuilding;

		// Token: 0x0400001E RID: 30
		public LinkedPausableConstructionSite _linked;

		// Token: 0x0400001F RID: 31
		public readonly MirrorOperationLock _mirrorOperationLock = new MirrorOperationLock();
	}
}
