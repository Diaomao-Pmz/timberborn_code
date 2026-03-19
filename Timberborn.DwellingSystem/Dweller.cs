using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using Timberborn.Persistence;
using Timberborn.RelationSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.DwellingSystem
{
	// Token: 0x02000009 RID: 9
	public class Dweller : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity, IInitializableEntity, IRelationOwner
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001C RID: 28 RVA: 0x000023CC File Offset: 0x000005CC
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x00002404 File Offset: 0x00000604
		public event EventHandler RelationsChanged;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002439 File Offset: 0x00000639
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002441 File Offset: 0x00000641
		public Dwelling Home { get; private set; }

		// Token: 0x06000020 RID: 32 RVA: 0x0000244A File Offset: 0x0000064A
		public Dweller(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002459 File Offset: 0x00000659
		public bool HasHome
		{
			get
			{
				return this.Home;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002466 File Offset: 0x00000666
		public Vector3? HomeAccess
		{
			get
			{
				return this.Home.GetEnabledComponent<Accessible>().UnblockedSingleAccess;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002478 File Offset: 0x00000678
		public void Awake()
		{
			this._isAdult = base.HasComponent<AdultSpec>();
			base.GetComponent<Character>().Died += delegate(object _, EventArgs _)
			{
				this.UnassignFromHome();
			};
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000249D File Offset: 0x0000069D
		public void InitializeEntity()
		{
			if (this._loaded)
			{
				this.AssignToDwellingAfterLoad();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024AD File Offset: 0x000006AD
		public void DeleteEntity()
		{
			this.UnassignFromHome();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024B5 File Offset: 0x000006B5
		public void AssignToHome(Dwelling home)
		{
			if (this.Home != home)
			{
				this.UnassignFromHome();
				this.Home = home;
				EventHandler relationsChanged = this.RelationsChanged;
				if (relationsChanged == null)
				{
					return;
				}
				relationsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024E3 File Offset: 0x000006E3
		public void UnassignFromHome()
		{
			if (this.Home)
			{
				Dwelling home = this.Home;
				this.Home = null;
				home.UnassignDweller(this);
				EventHandler relationsChanged = this.RelationsChanged;
				if (relationsChanged == null)
				{
					return;
				}
				relationsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000251B File Offset: 0x0000071B
		public bool IsLookingForBetterHome()
		{
			return !this.HasHome || this.HomeIsOverpopulated || this.HomeIsUnderpopulated;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002535 File Offset: 0x00000735
		public void Save(IEntitySaver entitySaver)
		{
			if (this.Home)
			{
				entitySaver.GetComponent(Dweller.DwellerKey).Set<Dwelling>(Dweller.HomeKey, this.Home, this._referenceSerializer.Of<Dwelling>());
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000256C File Offset: 0x0000076C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			Dwelling home;
			if (entityLoader.TryGetComponent(Dweller.DwellerKey, out objectLoader) && objectLoader.GetObsoletable<Dwelling>(Dweller.HomeKey, this._referenceSerializer.Of<Dwelling>(), out home))
			{
				this.Home = home;
				this._loaded = true;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025B0 File Offset: 0x000007B0
		public IEnumerable<BaseComponent> GetRelations()
		{
			if (!this.HasHome)
			{
				return Enumerable.Empty<BaseComponent>();
			}
			return Enumerables.One<Dwelling>(this.Home);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000025D8 File Offset: 0x000007D8
		public bool HomeIsOverpopulated
		{
			get
			{
				if (!this._isAdult)
				{
					return this.Home.OverpopulatedByChildren;
				}
				return this.Home.OverpopulatedByAdults;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025F9 File Offset: 0x000007F9
		public bool HomeIsUnderpopulated
		{
			get
			{
				return this._isAdult && this.Home.FreeAdultSlots >= 1 && this.Home.NumberOfDwellers == 1;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002624 File Offset: 0x00000824
		public void AssignToDwellingAfterLoad()
		{
			if (this.Home)
			{
				if (this.Home.HasFreeSlots)
				{
					this.Home.AssignDweller(this);
					return;
				}
				string firstName = base.GetComponent<Character>().FirstName;
				Debug.LogWarning("After loading " + firstName + " couldn't get assigned to their old home, " + this.Home.Name);
				this.Home = null;
			}
		}

		// Token: 0x0400000C RID: 12
		public static readonly ComponentKey DwellerKey = new ComponentKey("Dweller");

		// Token: 0x0400000D RID: 13
		public static readonly PropertyKey<Dwelling> HomeKey = new PropertyKey<Dwelling>("Home");

		// Token: 0x04000010 RID: 16
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000011 RID: 17
		public bool _isAdult;

		// Token: 0x04000012 RID: 18
		public bool _loaded;
	}
}
