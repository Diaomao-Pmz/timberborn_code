using System;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EnterableSystem;
using Timberborn.EntitySystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000007 RID: 7
	public class FixedSlotManager : BaseComponent, IAwakableComponent, IPostInitializableEntity, IUpdatableComponent, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._slotManager = base.GetComponent<SlotManager>();
			base.DisableComponent();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public void PostInitializeEntity()
		{
			this.ValidateSlots();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002128 File Offset: 0x00000328
		public void Update()
		{
			this._slotManager.UpdateAssignedSlots();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002135 File Offset: 0x00000335
		public void OnEnterFinishedState()
		{
			this.SubscribeToEvents();
			base.EnableComponent();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002143 File Offset: 0x00000343
		public void OnExitFinishedState()
		{
			this.UnsubscribeFromEvents();
			base.DisableComponent();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002151 File Offset: 0x00000351
		public void SubscribeToEvents()
		{
			this._enterable.EntererAdded += this.OnEntererAdded;
			this._enterable.EntererRemoved += this.OnEntererRemoved;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002181 File Offset: 0x00000381
		public void UnsubscribeFromEvents()
		{
			this._enterable.EntererAdded -= this.OnEntererAdded;
			this._enterable.EntererRemoved -= this.OnEntererRemoved;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B4 File Offset: 0x000003B4
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			if (!this._slotManager.AddEnterer(e.Enterer))
			{
				throw new InvalidOperationException(string.Format("No unassigned slots left out of total {0} at {1}", this._slotManager.SlotsCount, base.Name) + string.Format("\n{0} tried to enter.\n{1}", e.Enterer, this._slotManager.GetSlotsOccupation()));
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000221A File Offset: 0x0000041A
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			this._slotManager.RemoveEnterer(e.Enterer);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002230 File Offset: 0x00000430
		public void ValidateSlots()
		{
			EnterableSpec component = base.GetComponent<EnterableSpec>();
			if (!component.LimitedCapacityFinished)
			{
				throw new InvalidOperationException("FixedSlotManager does not support unlimited Enterables");
			}
			int capacityFinished = component.CapacityFinished;
			if (this._slotManager.SlotsCount < capacityFinished)
			{
				string name = base.GetComponent<Enterable>().Name;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("There are not enough slots in " + name + " for all visitors!");
				stringBuilder.AppendLine(string.Format(" There are {0} slots but {1} visitors.", this._slotManager.SlotsCount, capacityFinished));
				throw new InvalidOperationException(stringBuilder.ToString());
			}
		}

		// Token: 0x04000008 RID: 8
		public Enterable _enterable;

		// Token: 0x04000009 RID: 9
		public SlotManager _slotManager;
	}
}
