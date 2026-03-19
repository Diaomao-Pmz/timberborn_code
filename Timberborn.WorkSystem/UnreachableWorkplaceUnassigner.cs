using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.Navigation;
using Timberborn.TickSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000013 RID: 19
	public class UnreachableWorkplaceUnassigner : TickableComponent, IAwakableComponent, INavMeshListener, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002B02 File Offset: 0x00000D02
		public UnreachableWorkplaceUnassigner(INavMeshListenerEntityRegistry navMeshListenerEntityRegistry)
		{
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B11 File Offset: 0x00000D11
		public void Awake()
		{
			this._worker = base.GetComponent<Worker>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B2B File Offset: 0x00000D2B
		public override void Tick()
		{
			if (this._checkWorkplaceReachability)
			{
				this.UnemployIfWorkplaceIsNotInDistrict();
				this._checkWorkplaceReachability = false;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B42 File Offset: 0x00000D42
		public void InitializeEntity()
		{
			this._citizen.ChangedAssignedDistrict += delegate(object _, ChangeAssignedDistrictEventArgs _)
			{
				this.UnemployIfWorkplaceIsNotInDistrict();
			};
			this._navMeshListenerEntityRegistry.RegisterNavMeshListener(this);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B67 File Offset: 0x00000D67
		public void DeleteEntity()
		{
			this._navMeshListenerEntityRegistry.UnregisterNavMeshListener(this);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B75 File Offset: 0x00000D75
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.ScheduleWorkplaceReachabilityCheck();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B7D File Offset: 0x00000D7D
		public void ScheduleWorkplaceReachabilityCheck()
		{
			this._checkWorkplaceReachability = true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B88 File Offset: 0x00000D88
		public void UnemployIfWorkplaceIsNotInDistrict()
		{
			if (this._worker.Employed)
			{
				DistrictBuilding component = this._worker.Workplace.GetComponent<DistrictBuilding>();
				if (!this._citizen.HasAssignedDistrict || component.District != this._citizen.AssignedDistrict)
				{
					this._worker.Unemploy();
				}
			}
		}

		// Token: 0x0400001D RID: 29
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;

		// Token: 0x0400001E RID: 30
		public Worker _worker;

		// Token: 0x0400001F RID: 31
		public Citizen _citizen;

		// Token: 0x04000020 RID: 32
		public bool _checkWorkplaceReachability;
	}
}
