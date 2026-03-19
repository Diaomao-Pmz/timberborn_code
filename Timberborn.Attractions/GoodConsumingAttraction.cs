using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EnterableSystem;
using Timberborn.GoodConsumingBuildingSystem;

namespace Timberborn.Attractions
{
	// Token: 0x02000010 RID: 16
	public class GoodConsumingAttraction : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002E88 File Offset: 0x00001088
		public void Awake()
		{
			this._goodConsumingToggle = base.GetComponent<GoodConsumingBuilding>().GetGoodConsumingToggle();
			this._enterable = base.GetComponent<Enterable>();
			this._enterable.EntererAdded += this.OnEntererAdded;
			this._enterable.EntererRemoved += this.OnEntererRemoved;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void OnEnterFinishedState()
		{
			this.UpdateState();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002EE8 File Offset: 0x000010E8
		public void OnExitFinishedState()
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			this.UpdateState();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			this.UpdateState();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002EEA File Offset: 0x000010EA
		public void UpdateState()
		{
			if (this._enterable.NumberOfEnterersInside > 0)
			{
				this._goodConsumingToggle.ResumeConsumption();
				return;
			}
			this._goodConsumingToggle.PauseConsumption();
		}

		// Token: 0x04000030 RID: 48
		public Enterable _enterable;

		// Token: 0x04000031 RID: 49
		public GoodConsumingToggle _goodConsumingToggle;
	}
}
