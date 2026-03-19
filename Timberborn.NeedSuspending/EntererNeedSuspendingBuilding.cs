using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EnterableSystem;

namespace Timberborn.NeedSuspending
{
	// Token: 0x02000007 RID: 7
	public class EntererNeedSuspendingBuilding : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._entererNeedSuspendingBuildingSpec = base.GetComponent<EntererNeedSuspendingBuildingSpec>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002115 File Offset: 0x00000315
		public void OnEnterFinishedState()
		{
			this._enterable.EntererAdded += this.OnEntererAdded;
			this._enterable.EntererRemoved += this.OnEntererRemoved;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002145 File Offset: 0x00000345
		public void OnExitFinishedState()
		{
			this._enterable.EntererAdded -= this.OnEntererAdded;
			this._enterable.EntererRemoved -= this.OnEntererRemoved;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002175 File Offset: 0x00000375
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			this._entererNeedSuspendingBuildingSpec.NeedSuspender.SuspendNeeds(e.Enterer);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000218D File Offset: 0x0000038D
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			this._entererNeedSuspendingBuildingSpec.NeedSuspender.ResumeNeeds(e.Enterer);
		}

		// Token: 0x04000008 RID: 8
		public Enterable _enterable;

		// Token: 0x04000009 RID: 9
		public EntererNeedSuspendingBuildingSpec _entererNeedSuspendingBuildingSpec;
	}
}
