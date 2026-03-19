using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using UnityEngine;

namespace Timberborn.DwellingSystem
{
	// Token: 0x02000007 RID: 7
	public class AutoAssignableDwelling : BaseComponent, IAwakableComponent, IFinishedPausable, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AutoAssignableDwelling(StaleAssignableDwellingService staleAssignableDwellingService)
		{
			this._staleAssignableDwellingService = staleAssignableDwellingService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210F File Offset: 0x0000030F
		public bool HasAssignableSlot
		{
			get
			{
				return this.Active && this._dwelling.HasFreeSlots;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002126 File Offset: 0x00000326
		public bool ShouldAssignAdult
		{
			get
			{
				return this._dwelling.IsEmpty || !this._dwelling.UnderpopulatedByChildren;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002145 File Offset: 0x00000345
		public void Awake()
		{
			this._dwelling = base.GetComponent<Dwelling>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._blockableObject.ObjectBlocked += delegate(object _, EventArgs _)
			{
				this._dwelling.UnassignAllDwellers();
			};
			base.DisableComponent();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217C File Offset: 0x0000037C
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._staleAssignableDwellingService.RegisterDwelling(this);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002190 File Offset: 0x00000390
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._staleAssignableDwellingService.UnregisterDwelling(this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A4 File Offset: 0x000003A4
		public bool CanAssignDweller(Dweller dweller)
		{
			return this.Active && dweller.Home != this._dwelling && this._dwelling.HasFreeSlots && this.CanMoveIn(dweller);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D2 File Offset: 0x000003D2
		public void AssignDweller(Dweller dweller)
		{
			if (!this.CanAssignDweller(dweller))
			{
				Debug.LogWarning(string.Format("Can't assign {0} to {1}", dweller, base.Name));
				return;
			}
			this._dwelling.AssignDweller(dweller);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002200 File Offset: 0x00000400
		public bool Active
		{
			get
			{
				return base.Enabled && this._blockableObject.IsUnblocked;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002217 File Offset: 0x00000417
		public bool CanMoveIn(Dweller dweller)
		{
			if (!dweller.HasHome)
			{
				return true;
			}
			if (!dweller.GetComponent<Child>())
			{
				return this.CanAdultMoveIn(dweller);
			}
			return this.CanChildMoveIn(dweller);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000223F File Offset: 0x0000043F
		public bool CanChildMoveIn(Dweller dweller)
		{
			return (dweller.Home.NumberOfAdultDwellers == 0 && this._dwelling.NumberOfAdultDwellers > 0) || this._dwelling.UnderpopulatedByChildren;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002269 File Offset: 0x00000469
		public bool CanAdultMoveIn(Dweller dweller)
		{
			return this._dwelling.FreeAdultSlots > dweller.Home.FreeAdultSlots + 1 || (dweller.Home.NumberOfDwellers == 1 && this._dwelling.NumberOfDwellers == 1);
		}

		// Token: 0x04000008 RID: 8
		public readonly StaleAssignableDwellingService _staleAssignableDwellingService;

		// Token: 0x04000009 RID: 9
		public Dwelling _dwelling;

		// Token: 0x0400000A RID: 10
		public BlockableObject _blockableObject;
	}
}
