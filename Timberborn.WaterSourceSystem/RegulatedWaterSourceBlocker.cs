using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200000F RID: 15
	public class RegulatedWaterSourceBlocker : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000282D File Offset: 0x00000A2D
		public void Awake()
		{
			this._waterSourceRegulator = base.GetComponent<WaterSourceRegulator>();
			this._blockObjectBelowBlocker = base.GetComponent<BlockObjectBelowBlocker>();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002847 File Offset: 0x00000A47
		public void OnEnterFinishedState()
		{
			this._waterSourceRegulator.OpenStateChanged += this.OnOpenStateChanged;
			if (!this._waterSourceRegulator.IsOpen)
			{
				this._blockObjectBelowBlocker.Block();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002878 File Offset: 0x00000A78
		public void OnExitFinishedState()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000287A File Offset: 0x00000A7A
		public void OnOpenStateChanged(object sender, bool isOpen)
		{
			if (this._waterSourceRegulator.IsOpen)
			{
				this._blockObjectBelowBlocker.Unblock();
				return;
			}
			this._blockObjectBelowBlocker.Block();
		}

		// Token: 0x0400001D RID: 29
		public WaterSourceRegulator _waterSourceRegulator;

		// Token: 0x0400001E RID: 30
		public BlockObjectBelowBlocker _blockObjectBelowBlocker;
	}
}
