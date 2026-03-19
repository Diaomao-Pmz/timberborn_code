using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.Bots;
using Timberborn.Common;
using Timberborn.EntitySystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000021 RID: 33
	public class DistrictPopulation : BaseComponent, IAwakableComponent
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060000FA RID: 250 RVA: 0x00004354 File Offset: 0x00002554
		// (remove) Token: 0x060000FB RID: 251 RVA: 0x0000438C File Offset: 0x0000258C
		public event EventHandler<CitizenAssignedEventArgs> CitizenAssigned;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060000FC RID: 252 RVA: 0x000043C4 File Offset: 0x000025C4
		// (remove) Token: 0x060000FD RID: 253 RVA: 0x000043FC File Offset: 0x000025FC
		public event EventHandler<CitizenUnassignedEventArgs> CitizenUnassigned;

		// Token: 0x060000FE RID: 254 RVA: 0x00004431 File Offset: 0x00002631
		public DistrictPopulation(EntityComponentRegistryFactory entityComponentRegistryFactory)
		{
			this._entityComponentRegistryFactory = entityComponentRegistryFactory;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004456 File Offset: 0x00002656
		public ReadOnlyList<Bot> Bots
		{
			get
			{
				return this._bots.AsReadOnlyList<Bot>();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004463 File Offset: 0x00002663
		public ReadOnlyList<Beaver> Adults
		{
			get
			{
				return this._beaverCollection.Adults;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004470 File Offset: 0x00002670
		public ReadOnlyList<Beaver> Children
		{
			get
			{
				return this._beaverCollection.Children;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000447D File Offset: 0x0000267D
		public ReadOnlyList<Beaver> Beavers
		{
			get
			{
				return this._beaverCollection.Beavers;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000448A File Offset: 0x0000268A
		public int NumberOfAdults
		{
			get
			{
				return this._beaverCollection.NumberOfAdults;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004497 File Offset: 0x00002697
		public int NumberOfChildren
		{
			get
			{
				return this._beaverCollection.NumberOfChildren;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000044A4 File Offset: 0x000026A4
		public int NumberOfBots
		{
			get
			{
				return this._bots.Count;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000044B1 File Offset: 0x000026B1
		public void Awake()
		{
			this._entityComponentRegistry = this._entityComponentRegistryFactory.Create();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000044C4 File Offset: 0x000026C4
		public IEnumerable<T> GetEnabledCharacters<T>() where T : BaseComponent, IRegisteredComponent
		{
			return this._entityComponentRegistry.GetEnabled<T>();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000044D4 File Offset: 0x000026D4
		public void AssignCitizen(Citizen citizen)
		{
			Beaver component = citizen.GetComponent<Beaver>();
			if (component != null)
			{
				this._beaverCollection.AddBeaver(component);
			}
			Bot component2 = citizen.GetComponent<Bot>();
			if (component2 != null)
			{
				this._bots.Add(component2);
			}
			this._entityComponentRegistry.Register(citizen.GetComponent<EntityComponent>());
			EventHandler<CitizenAssignedEventArgs> citizenAssigned = this.CitizenAssigned;
			if (citizenAssigned == null)
			{
				return;
			}
			citizenAssigned(this, new CitizenAssignedEventArgs(citizen));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004538 File Offset: 0x00002738
		public void UnassignCitizen(Citizen citizen)
		{
			Beaver component = citizen.GetComponent<Beaver>();
			if (component != null)
			{
				this._beaverCollection.RemoveBeaver(component);
			}
			Bot component2 = citizen.GetComponent<Bot>();
			if (component2 != null)
			{
				this._bots.Remove(component2);
			}
			this._entityComponentRegistry.Unregister(citizen.GetComponent<EntityComponent>());
			EventHandler<CitizenUnassignedEventArgs> citizenUnassigned = this.CitizenUnassigned;
			if (citizenUnassigned == null)
			{
				return;
			}
			citizenUnassigned(this, new CitizenUnassignedEventArgs(citizen));
		}

		// Token: 0x0400005E RID: 94
		public readonly EntityComponentRegistryFactory _entityComponentRegistryFactory;

		// Token: 0x0400005F RID: 95
		public EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000060 RID: 96
		public readonly BeaverCollection _beaverCollection = new BeaverCollection();

		// Token: 0x04000061 RID: 97
		public readonly List<Bot> _bots = new List<Bot>();
	}
}
