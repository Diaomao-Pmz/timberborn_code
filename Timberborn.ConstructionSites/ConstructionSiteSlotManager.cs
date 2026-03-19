using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EnterableSystem;
using Timberborn.EntitySystem;
using Timberborn.SlotSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200001F RID: 31
	public class ConstructionSiteSlotManager : BaseComponent, IAwakableComponent, IDeletableEntity
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004004 File Offset: 0x00002204
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._constructionSite = base.GetComponent<ConstructionSite>();
			this._slotManager = base.GetComponent<SlotManager>();
			this._slotManager.EntererAssignedToSlot += ConstructionSiteSlotManager.OnEntererAssignedToSlot;
			this._enterable.EntererAdded += this.OnEntererAdded;
			this._enterable.EntererRemoved += this.OnEntererRemoved;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000407A File Offset: 0x0000227A
		public void DeleteEntity()
		{
			this.DetachEventHandlers();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004082 File Offset: 0x00002282
		public void DetachEventHandlers()
		{
			this._enterable.EntererAdded -= this.OnEntererAdded;
			this._enterable.EntererRemoved -= this.OnEntererRemoved;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000040B4 File Offset: 0x000022B4
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			Enterer enterer = e.Enterer;
			if (this._constructionSite.Enabled && this.SlotsAreFull(enterer))
			{
				enterer.GetComponent<CharacterModel>().Hide();
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000040EC File Offset: 0x000022EC
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			Enterer enterer = e.Enterer;
			this._slotManager.RemoveEnterer(enterer);
			ConstructionSiteSlotManager.ShowEnterer(enterer);
			if (!this._constructionSite.Enabled && this._enterable.NumberOfEnterersInside == 0)
			{
				this.DetachEventHandlers();
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004132 File Offset: 0x00002332
		public static void OnEntererAssignedToSlot(object sender, Enterer e)
		{
			ConstructionSiteSlotManager.ShowEnterer(e);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000413C File Offset: 0x0000233C
		public bool SlotsAreFull(Enterer enterer)
		{
			Builder component = enterer.GetComponent<Builder>();
			return component && component.ReservedConstructionSite == this._constructionSite && !this._slotManager.AddEnterer(enterer);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004177 File Offset: 0x00002377
		public static void ShowEnterer(Enterer enterer)
		{
			enterer.GetComponent<CharacterModel>().Show();
		}

		// Token: 0x04000064 RID: 100
		public Enterable _enterable;

		// Token: 0x04000065 RID: 101
		public ConstructionSite _constructionSite;

		// Token: 0x04000066 RID: 102
		public SlotManager _slotManager;
	}
}
