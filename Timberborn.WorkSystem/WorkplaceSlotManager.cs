using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EnterableSystem;
using Timberborn.SlotSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000027 RID: 39
	public class WorkplaceSlotManager : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000044C8 File Offset: 0x000026C8
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._workplace = base.GetComponent<Workplace>();
			this._slotManager = base.GetComponent<SlotManager>();
			this._slotManager.EntererAssignedToSlot += WorkplaceSlotManager.OnEntererAssignedToSlot;
			base.DisableComponent();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004516 File Offset: 0x00002716
		public void OnEnterFinishedState()
		{
			this._enterable.EntererAdded += this.OnEntererAdded;
			this._enterable.EntererRemoved += this.OnEntererRemoved;
			base.EnableComponent();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000454C File Offset: 0x0000274C
		public void OnExitFinishedState()
		{
			this._enterable.EntererAdded -= this.OnEntererAdded;
			this._enterable.EntererRemoved -= this.OnEntererRemoved;
			base.DisableComponent();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004584 File Offset: 0x00002784
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			Enterer enterer = e.Enterer;
			if (this.SlotsAreFull(enterer))
			{
				enterer.GetComponent<CharacterModel>().Hide();
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000045AC File Offset: 0x000027AC
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			Enterer enterer = e.Enterer;
			this._slotManager.RemoveEnterer(enterer);
			WorkplaceSlotManager.ShowEnterer(enterer);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000045D2 File Offset: 0x000027D2
		public static void OnEntererAssignedToSlot(object sender, Enterer e)
		{
			WorkplaceSlotManager.ShowEnterer(e);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000045DC File Offset: 0x000027DC
		public bool SlotsAreFull(Enterer enterer)
		{
			Worker component = enterer.GetComponent<Worker>();
			return component && component.Workplace == this._workplace && !this._slotManager.AddEnterer(enterer);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004617 File Offset: 0x00002817
		public static void ShowEnterer(Enterer enterer)
		{
			enterer.GetComponent<CharacterModel>().Show();
		}

		// Token: 0x04000065 RID: 101
		public Enterable _enterable;

		// Token: 0x04000066 RID: 102
		public Workplace _workplace;

		// Token: 0x04000067 RID: 103
		public SlotManager _slotManager;
	}
}
