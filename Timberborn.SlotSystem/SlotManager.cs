using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EnterableSystem;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000017 RID: 23
	public class SlotManager : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000098 RID: 152 RVA: 0x00003350 File Offset: 0x00001550
		// (remove) Token: 0x06000099 RID: 153 RVA: 0x00003388 File Offset: 0x00001588
		public event EventHandler<Enterer> EntererUnassignedFromSlot;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600009A RID: 154 RVA: 0x000033C0 File Offset: 0x000015C0
		// (remove) Token: 0x0600009B RID: 155 RVA: 0x000033F8 File Offset: 0x000015F8
		public event EventHandler<Enterer> EntererAssignedToSlot;

		// Token: 0x0600009C RID: 156 RVA: 0x0000342D File Offset: 0x0000162D
		public SlotManager(IRandomNumberGenerator randomNumberGenerator)
		{
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003452 File Offset: 0x00001652
		public int SlotsCount
		{
			get
			{
				return this._slots.Count;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000345F File Offset: 0x0000165F
		public void Awake()
		{
			this._customSlotRetriever = base.GetComponent<ICustomSlotRetriever>();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000346D File Offset: 0x0000166D
		public void InitializeEntity()
		{
			this.InitializeSlots();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003475 File Offset: 0x00001675
		public void DeleteEntity()
		{
			this.ClearSlots();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003480 File Offset: 0x00001680
		public void UpdateAssignedSlots()
		{
			float deltaTime = Time.deltaTime;
			for (int i = 0; i < this._slots.Count; i++)
			{
				ISlot slot = this._slots[i];
				if (slot.AssignedEnterer)
				{
					slot.Update(deltaTime);
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000034CC File Offset: 0x000016CC
		public void ReassignAllSlots()
		{
			foreach (ISlot slot in this._slots)
			{
				Enterer assignedEnterer = slot.AssignedEnterer;
				if (assignedEnterer)
				{
					slot.UnassignEnterer();
					EventHandler<Enterer> entererUnassignedFromSlot = this.EntererUnassignedFromSlot;
					if (entererUnassignedFromSlot != null)
					{
						entererUnassignedFromSlot(this, assignedEnterer);
					}
					this.AddEnterer(assignedEnterer);
				}
			}
			for (int i = 0; i < this._unassignedEnterers.Count; i++)
			{
				this.AssignFirstUnassigned();
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003564 File Offset: 0x00001764
		public bool AddEnterer(Enterer enterer)
		{
			ISlot slot;
			if (this.TryGetUnassignedSlot(out slot))
			{
				slot.AssignEnterer(enterer);
				EventHandler<Enterer> entererAssignedToSlot = this.EntererAssignedToSlot;
				if (entererAssignedToSlot != null)
				{
					entererAssignedToSlot(this, enterer);
				}
				return true;
			}
			this._unassignedEnterers.Add(enterer);
			return false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000035A8 File Offset: 0x000017A8
		public void RemoveEnterer(Enterer enterer)
		{
			ISlot slot2 = this._slots.Find((ISlot slot) => slot.AssignedEnterer == enterer);
			if (slot2 != null)
			{
				this.Unassign(slot2);
				this.AssignFirstUnassigned();
				return;
			}
			this._unassignedEnterers.Remove(enterer);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000035FD File Offset: 0x000017FD
		public string GetSlotsOccupation()
		{
			return this._slots.CollectionToString("Slots occupied by: ");
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000360F File Offset: 0x0000180F
		public IEnumerable<ISlot> AvailableSlots
		{
			get
			{
				return from slot in this._slots
				where slot.IsAvailable
				select slot;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000363C File Offset: 0x0000183C
		public void ClearSlots()
		{
			foreach (ISlot slot in this._slots)
			{
				this.Unassign(slot);
			}
			this._slots.Clear();
			this._unassignedEnterers.Clear();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000036A8 File Offset: 0x000018A8
		public void InitializeSlots()
		{
			foreach (SlotInitializer slotInitializer in base.GetComponentsAllocating<SlotInitializer>())
			{
				this._slots.AddRange(slotInitializer.InitializeSlots());
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003708 File Offset: 0x00001908
		public bool TryGetUnassignedSlot(out ISlot unassignedSlot)
		{
			if (this._customSlotRetriever != null && this._customSlotRetriever.TryGetUnassignedSlot(this.AvailableSlots, out unassignedSlot))
			{
				return true;
			}
			IEnumerable<ISlot> source = from slot in this.AvailableSlots
			where slot.IsAvailable && !slot.AssignedEnterer
			select slot;
			return this._randomNumberGenerator.TryGetEnumerableElement<ISlot>(source, out unassignedSlot);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000376C File Offset: 0x0000196C
		public void Unassign(ISlot slot)
		{
			Enterer assignedEnterer = slot.AssignedEnterer;
			if (assignedEnterer)
			{
				slot.UnassignEnterer();
				EventHandler<Enterer> entererUnassignedFromSlot = this.EntererUnassignedFromSlot;
				if (entererUnassignedFromSlot == null)
				{
					return;
				}
				entererUnassignedFromSlot(this, assignedEnterer);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000037A0 File Offset: 0x000019A0
		public void AssignFirstUnassigned()
		{
			if (this._unassignedEnterers.Count > 0)
			{
				Enterer enterer = this._unassignedEnterers.First<Enterer>();
				this._unassignedEnterers.Remove(enterer);
				this.AddEnterer(enterer);
			}
		}

		// Token: 0x0400002F RID: 47
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000030 RID: 48
		public ICustomSlotRetriever _customSlotRetriever;

		// Token: 0x04000031 RID: 49
		public readonly List<ISlot> _slots = new List<ISlot>();

		// Token: 0x04000032 RID: 50
		public readonly HashSet<Enterer> _unassignedEnterers = new HashSet<Enterer>();
	}
}
