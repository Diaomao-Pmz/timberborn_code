using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.Navigation;
using Timberborn.TickSystem;

namespace Timberborn.DwellingSystem
{
	// Token: 0x02000013 RID: 19
	public class UnreachableHomeUnassigner : TickableComponent, IAwakableComponent, INavMeshListener, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000033F4 File Offset: 0x000015F4
		public UnreachableHomeUnassigner(INavMeshListenerEntityRegistry navMeshListenerEntityRegistry)
		{
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003403 File Offset: 0x00001603
		public void Awake()
		{
			this._dweller = base.GetComponent<Dweller>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000341D File Offset: 0x0000161D
		public override void Tick()
		{
			if (this._checkHomeReachability)
			{
				this.UnassignFromHomeIfNotInDistrict();
				this._checkHomeReachability = false;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003434 File Offset: 0x00001634
		public void InitializeEntity()
		{
			this._citizen.ChangedAssignedDistrict += delegate(object _, ChangeAssignedDistrictEventArgs _)
			{
				this.UnassignFromHomeIfNotInDistrict();
			};
			this._navMeshListenerEntityRegistry.RegisterNavMeshListener(this);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003459 File Offset: 0x00001659
		public void DeleteEntity()
		{
			this._navMeshListenerEntityRegistry.UnregisterNavMeshListener(this);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003467 File Offset: 0x00001667
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.ScheduleDwellingReachabilityCheck();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000346F File Offset: 0x0000166F
		public void ScheduleDwellingReachabilityCheck()
		{
			this._checkHomeReachability = true;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003478 File Offset: 0x00001678
		public void UnassignFromHomeIfNotInDistrict()
		{
			if (this._dweller.HasHome)
			{
				DistrictBuilding component = this._dweller.Home.GetComponent<DistrictBuilding>();
				if (!this._citizen.HasAssignedDistrict || component.District != this._citizen.AssignedDistrict)
				{
					this._dweller.UnassignFromHome();
				}
			}
		}

		// Token: 0x0400002B RID: 43
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;

		// Token: 0x0400002C RID: 44
		public Dweller _dweller;

		// Token: 0x0400002D RID: 45
		public Citizen _citizen;

		// Token: 0x0400002E RID: 46
		public bool _checkHomeReachability;
	}
}
