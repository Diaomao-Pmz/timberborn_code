using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EnterableSystem;
using Timberborn.SlotSystem;
using Timberborn.TickSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000019 RID: 25
	public class WorkshopWorkerHider : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00003623 File Offset: 0x00001823
		public void Awake()
		{
			SlotManager component = base.GetComponent<SlotManager>();
			component.EntererAssignedToSlot += this.OnEntererAssignedToSlot;
			component.EntererUnassignedFromSlot += delegate(object _, Enterer e)
			{
				this._workerInSlots.Remove(e.GetComponent<WorkshopWorker>());
			};
			base.DisableComponent();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000280E File Offset: 0x00000A0E
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002816 File Offset: 0x00000A16
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003654 File Offset: 0x00001854
		public override void Tick()
		{
			this.UpdateVisibility();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000365C File Offset: 0x0000185C
		public void OnEntererAssignedToSlot(object sender, Enterer e)
		{
			WorkshopWorker component = e.GetComponent<WorkshopWorker>();
			this._workerInSlots.Add(component);
			component.UpdateVisibility();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003684 File Offset: 0x00001884
		public void UpdateVisibility()
		{
			foreach (WorkshopWorker workshopWorker in this._workerInSlots)
			{
				workshopWorker.UpdateVisibility();
			}
		}

		// Token: 0x0400003C RID: 60
		public readonly List<WorkshopWorker> _workerInSlots = new List<WorkshopWorker>();
	}
}
