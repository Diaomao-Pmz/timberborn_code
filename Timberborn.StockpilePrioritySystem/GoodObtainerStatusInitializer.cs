using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Hauling;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x02000005 RID: 5
	public class GoodObtainerStatusInitializer : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002216 File Offset: 0x00000416
		public void Awake()
		{
			this._goodObtainer = base.GetComponent<GoodObtainer>();
			this._noHaulingPostStatus = base.GetComponent<NoHaulingPostStatus>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002230 File Offset: 0x00000430
		public void OnEnterFinishedState()
		{
			this._noHaulingPostStatus.Initialize(() => this._goodObtainer.IsObtaining);
			this._goodObtainer.GoodObtainingChanged += this.UpdateStatus;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002260 File Offset: 0x00000460
		public void OnExitFinishedState()
		{
			this._goodObtainer.GoodObtainingChanged -= this.UpdateStatus;
			this._noHaulingPostStatus.Disable();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002284 File Offset: 0x00000484
		public void UpdateStatus(object sender, EventArgs e)
		{
			this._noHaulingPostStatus.UpdateStatus();
		}

		// Token: 0x0400000A RID: 10
		public GoodObtainer _goodObtainer;

		// Token: 0x0400000B RID: 11
		public NoHaulingPostStatus _noHaulingPostStatus;
	}
}
