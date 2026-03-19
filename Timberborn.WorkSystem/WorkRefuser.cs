using System;
using Timberborn.BaseComponentSystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200002D RID: 45
	public class WorkRefuser : BaseComponent, IAwakableComponent
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600016A RID: 362 RVA: 0x000050EC File Offset: 0x000032EC
		// (remove) Token: 0x0600016B RID: 363 RVA: 0x00005124 File Offset: 0x00003324
		public event EventHandler RefusesWorkChanged;

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005159 File Offset: 0x00003359
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00005161 File Offset: 0x00003361
		public bool RefusesWork { get; private set; }

		// Token: 0x0600016E RID: 366 RVA: 0x0000516A File Offset: 0x0000336A
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._needManager.NeedChangedCriticalState += this.OnNeedChangedCriticalState;
			this._worker = base.GetComponent<Worker>();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000519B File Offset: 0x0000339B
		public void OnNeedChangedCriticalState(object sender, NeedChangedCriticalStateEventArgs e)
		{
			this.UpdateRefuseWork();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000051A4 File Offset: 0x000033A4
		public void UpdateRefuseWork()
		{
			bool flag = this.ShouldRefuseWork();
			if (this.RefusesWork != flag)
			{
				this.RefusesWork = flag;
				if (this.RefusesWork)
				{
					this._worker.Unemploy();
				}
				EventHandler refusesWorkChanged = this.RefusesWorkChanged;
				if (refusesWorkChanged == null)
				{
					return;
				}
				refusesWorkChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000051F4 File Offset: 0x000033F4
		public bool ShouldRefuseWork()
		{
			foreach (NeedSpec needSpec in this._needManager.NeedSpecs)
			{
				if (this._needManager.NeedIsInCriticalState(needSpec.Id) && needSpec.HasSpec<NeedPreventingWorkSpec>())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400008A RID: 138
		public NeedManager _needManager;

		// Token: 0x0400008B RID: 139
		public Worker _worker;
	}
}
