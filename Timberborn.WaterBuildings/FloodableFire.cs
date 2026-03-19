using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.WaterObjects;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200000D RID: 13
	public class FloodableFire : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002FC7 File Offset: 0x000011C7
		public void Awake()
		{
			this._fire = base.GetComponent<Fire>();
			this._floodableObject = base.GetComponent<FloodableObject>();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002FE4 File Offset: 0x000011E4
		public void OnEnterFinishedState()
		{
			this._floodableObject.Flooded += this.OnFlooded;
			this._floodableObject.Unflooded += this.OnUnflooded;
			if (!this._floodableObject.IsFlooded)
			{
				this._fire.Enable();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003037 File Offset: 0x00001237
		public void OnExitFinishedState()
		{
			this._floodableObject.Flooded -= this.OnFlooded;
			this._floodableObject.Unflooded -= this.OnUnflooded;
			this._fire.Disable();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003072 File Offset: 0x00001272
		public void OnFlooded(object sender, EventArgs e)
		{
			this._fire.Disable();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000307F File Offset: 0x0000127F
		public void OnUnflooded(object sender, EventArgs e)
		{
			this._fire.Enable();
		}

		// Token: 0x0400002B RID: 43
		public Fire _fire;

		// Token: 0x0400002C RID: 44
		public FloodableObject _floodableObject;
	}
}
