using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200001B RID: 27
	public class DistrictConnections : ISingletonInstantNavMeshListener, ILoadableSingleton
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00003C93 File Offset: 0x00001E93
		public DistrictConnections(DistrictCenterRegistry districtCenterRegistry, EventBus eventBus)
		{
			this._districtCenterRegistry = districtCenterRegistry;
			this._eventBus = eventBus;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003CC2 File Offset: 0x00001EC2
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.ReassignDistricts();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003CC2 File Offset: 0x00001EC2
		[OnEvent]
		public void OnDistrictCenterRegistryChanged(DistrictCenterRegistryChangedEvent districtCenterRegistryChangedEvent)
		{
			this.ReassignDistricts();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003CCA File Offset: 0x00001ECA
		public IEnumerable<DistrictCenter> GetDistrictsConnectedWith(DistrictCenter districtCenter)
		{
			return this.GetDistrictCluster(districtCenter).GetDistrictsOtherThan(districtCenter);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003CD9 File Offset: 0x00001ED9
		public bool AreDistrictsConnected(DistrictCenter firstDistrict, DistrictCenter secondDistrict)
		{
			return this.GetDistrictCluster(firstDistrict).Contains(secondDistrict);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public DistrictCenter GetFirstConnectedOrAny(DistrictCenter districtCenter)
		{
			using (IEnumerator<DistrictCenter> enumerator = this.GetDistrictsConnectedWith(districtCenter).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			for (int i = 0; i < this.DistrictCenters.Count; i++)
			{
				if (this.DistrictCenters[i] != districtCenter)
				{
					return this.DistrictCenters[i];
				}
			}
			return districtCenter;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003D70 File Offset: 0x00001F70
		public ReadOnlyList<DistrictCenter> DistrictCenters
		{
			get
			{
				return this._districtCenterRegistry.FinishedDistrictCenters;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003D80 File Offset: 0x00001F80
		public DistrictCluster GetDistrictCluster(DistrictCenter districtCenter)
		{
			for (int i = 0; i < this._districtClusters.Count; i++)
			{
				if (this._districtClusters[i].Contains(districtCenter))
				{
					return this._districtClusters[i];
				}
			}
			throw new NotSupportedException("Found DistrictCenter: " + districtCenter.DistrictName + " not assigned to any DistrictCluster");
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public void ReassignDistricts()
		{
			this._districtClusters.Clear();
			for (int i = 0; i < this._districtCenterRegistry.FinishedDistrictCenters.Count; i++)
			{
				this.AddDistrictToCluster(this._districtCenterRegistry.FinishedDistrictCenters[i]);
			}
			this._eventBus.Post(new DistrictConnectionsChangedEvent());
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003E40 File Offset: 0x00002040
		public void AddDistrictToCluster(DistrictCenter districtCenter)
		{
			if (!this.TryAddDistrictToExistingCluster(districtCenter))
			{
				this.AddNewClusterWithDistrict(districtCenter);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003E54 File Offset: 0x00002054
		public bool TryAddDistrictToExistingCluster(DistrictCenter districtCenter)
		{
			for (int i = 0; i < this._districtClusters.Count; i++)
			{
				if (this._districtClusters[i].TryAddDistrict(districtCenter))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003E90 File Offset: 0x00002090
		public void AddNewClusterWithDistrict(DistrictCenter districtCenter)
		{
			DistrictCluster item = new DistrictCluster(districtCenter);
			this._districtClusters.Add(item);
		}

		// Token: 0x0400004F RID: 79
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000050 RID: 80
		public readonly EventBus _eventBus;

		// Token: 0x04000051 RID: 81
		public readonly List<DistrictCluster> _districtClusters = new List<DistrictCluster>();
	}
}
