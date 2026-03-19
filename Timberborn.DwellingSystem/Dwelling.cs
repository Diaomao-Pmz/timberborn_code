using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.NeedSpecs;
using Timberborn.RelationSystem;
using UnityEngine;

namespace Timberborn.DwellingSystem
{
	// Token: 0x0200000C RID: 12
	public class Dwelling : BaseComponent, IAwakableComponent, IFinishedStateListener, IRegisteredComponent, IRelationOwner
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600003D RID: 61 RVA: 0x00002984 File Offset: 0x00000B84
		// (remove) Token: 0x0600003E RID: 62 RVA: 0x000029BC File Offset: 0x00000BBC
		public event EventHandler RelationsChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600003F RID: 63 RVA: 0x000029F4 File Offset: 0x00000BF4
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x00002A2C File Offset: 0x00000C2C
		public event EventHandler NumberOfDwellersChanged;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A61 File Offset: 0x00000C61
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002A69 File Offset: 0x00000C69
		public int ChildSlots { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002A72 File Offset: 0x00000C72
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002A7A File Offset: 0x00000C7A
		public int AdultSlots { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002A83 File Offset: 0x00000C83
		public int MaxBeavers
		{
			get
			{
				return this._dwellingSpec.MaxBeavers;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A90 File Offset: 0x00000C90
		public IReadOnlyCollection<ContinuousEffectSpec> SleepEffects
		{
			get
			{
				return this._dwellingSpec.SleepEffects;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002AA2 File Offset: 0x00000CA2
		public IEnumerable<Dweller> AdultDwellers
		{
			get
			{
				return this._adults.AsReadOnlyEnumerable<Dweller>();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002AAF File Offset: 0x00000CAF
		public IEnumerable<Dweller> ChildDwellers
		{
			get
			{
				return this._children.AsReadOnlyEnumerable<Dweller>();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002ABC File Offset: 0x00000CBC
		public int NumberOfDwellers
		{
			get
			{
				return this.NumberOfAdultDwellers + this.NumberOfChildDwellers;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002ACB File Offset: 0x00000CCB
		public int NumberOfAdultDwellers
		{
			get
			{
				return this._adults.Count;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public int NumberOfChildDwellers
		{
			get
			{
				return this._children.Count;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002AE5 File Offset: 0x00000CE5
		public bool HasFreeSlots
		{
			get
			{
				return this.NumberOfAdultDwellers + this.NumberOfChildDwellers < this.MaxBeavers;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002AFC File Offset: 0x00000CFC
		public bool IsEmpty
		{
			get
			{
				return this.NumberOfAdultDwellers == 0 && this.NumberOfChildDwellers == 0;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002B11 File Offset: 0x00000D11
		public bool OverpopulatedByAdults
		{
			get
			{
				return this.NumberOfAdultDwellers > this.AdultSlots;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002B21 File Offset: 0x00000D21
		public int FreeAdultSlots
		{
			get
			{
				return this.AdultSlots - this.NumberOfAdultDwellers;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002B30 File Offset: 0x00000D30
		public bool OverpopulatedByChildren
		{
			get
			{
				return this.DesiredNumberOfChildren < this.NumberOfChildDwellers;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002B40 File Offset: 0x00000D40
		public bool UnderpopulatedByChildren
		{
			get
			{
				return this.DesiredNumberOfChildren > this.NumberOfChildDwellers && this.ChildSlots > 0;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002B5B File Offset: 0x00000D5B
		public bool HasFreeChildSlots
		{
			get
			{
				return this.HasFreeSlots && this.ChildSlots > this.NumberOfChildDwellers;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002B75 File Offset: 0x00000D75
		public int TotalSlots
		{
			get
			{
				return this.AdultSlots + this.ChildSlots;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B84 File Offset: 0x00000D84
		public void Awake()
		{
			this._dwellingSpec = base.GetComponent<DwellingSpec>();
			this.ChildSlots = Mathf.FloorToInt((float)this.MaxBeavers / 3f);
			this.AdultSlots = this.MaxBeavers - this.ChildSlots;
			base.DisableComponent();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002BCB File Offset: 0x00000DCB
		public void OnExitFinishedState()
		{
			this.UnassignAllDwellers();
			base.DisableComponent();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void AssignDweller(Dweller dweller)
		{
			if (!this.HasFreeSlots)
			{
				throw new InvalidOperationException("Tried to assign a dweller to a full dwelling!");
			}
			dweller.AssignToHome(this);
			if (dweller.GetComponent<Child>())
			{
				this._children.Add(dweller);
			}
			else
			{
				this._adults.Add(dweller);
			}
			EventHandler relationsChanged = this.RelationsChanged;
			if (relationsChanged != null)
			{
				relationsChanged(this, EventArgs.Empty);
			}
			EventHandler numberOfDwellersChanged = this.NumberOfDwellersChanged;
			if (numberOfDwellersChanged == null)
			{
				return;
			}
			numberOfDwellersChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002C5C File Offset: 0x00000E5C
		public void UnassignDweller(Dweller dweller)
		{
			this._adults.Remove(dweller);
			this._children.Remove(dweller);
			dweller.UnassignFromHome();
			EventHandler relationsChanged = this.RelationsChanged;
			if (relationsChanged != null)
			{
				relationsChanged(this, EventArgs.Empty);
			}
			EventHandler numberOfDwellersChanged = this.NumberOfDwellersChanged;
			if (numberOfDwellersChanged == null)
			{
				return;
			}
			numberOfDwellersChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public void UnassignAllDwellers()
		{
			foreach (Dweller dweller in this._adults.Concat(this._children).ToList<Dweller>())
			{
				this.UnassignDweller(dweller);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D1C File Offset: 0x00000F1C
		public IEnumerable<BaseComponent> GetRelations()
		{
			foreach (Dweller dweller in this._children)
			{
				yield return dweller;
			}
			HashSet<Dweller>.Enumerator enumerator = default(HashSet<Dweller>.Enumerator);
			foreach (Dweller dweller2 in this._adults)
			{
				yield return dweller2;
			}
			enumerator = default(HashSet<Dweller>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002D2C File Offset: 0x00000F2C
		public int DesiredNumberOfChildren
		{
			get
			{
				return this.NumberOfAdultDwellers / 2;
			}
		}

		// Token: 0x0400001C RID: 28
		public DwellingSpec _dwellingSpec;

		// Token: 0x0400001D RID: 29
		public readonly HashSet<Dweller> _adults = new HashSet<Dweller>();

		// Token: 0x0400001E RID: 30
		public readonly HashSet<Dweller> _children = new HashSet<Dweller>();
	}
}
