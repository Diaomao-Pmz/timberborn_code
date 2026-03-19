using System;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000051 RID: 81
	public class NavigationSynchronizer : ITickableSingleton, IUpdatableSingleton, ILoadableSingleton, IPostLoadableSingleton, INavigationPhase
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000584C File Offset: 0x00003A4C
		public NavigationSynchronizer(NavMeshUpdater navMeshUpdater, NavMeshUpdateNotifier navMeshUpdateNotifier, DistrictUpdater districtUpdater, NavMeshUpdateBuilderFactory navMeshUpdateBuilderFactory, RestrictedNodeUpdater restrictedNodeUpdater)
		{
			this._navMeshUpdater = navMeshUpdater;
			this._navMeshUpdateNotifier = navMeshUpdateNotifier;
			this._districtUpdater = districtUpdater;
			this._navMeshUpdateBuilderFactory = navMeshUpdateBuilderFactory;
			this._restrictedNodeUpdater = restrictedNodeUpdater;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005879 File Offset: 0x00003A79
		public void Load()
		{
			this._regularNavMeshUpdateBuilder = this._navMeshUpdateBuilderFactory.Create();
			this._previewNavMeshUpdateBuilder = this._navMeshUpdateBuilderFactory.Create();
			this._instantNavMeshUpdateBuilder = this._navMeshUpdateBuilderFactory.Create();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000058AE File Offset: 0x00003AAE
		public void PostLoad()
		{
			this.ProcessPreviewChanges();
			this.ProcessInstantChanges();
			this.ProcessRegularChanges();
			this._navMeshUpdater.TrimExcess();
			this.NotifyAllNavmeshChanges();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000058D3 File Offset: 0x00003AD3
		public void Tick()
		{
			this.ProcessRegularChanges();
			this.NotifyAllNavmeshChanges();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000058E1 File Offset: 0x00003AE1
		public void UpdateSingleton()
		{
			this.ProcessPreviewChanges();
			this.ProcessInstantChanges();
			this.NotifyAllNavmeshChanges();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000058F5 File Offset: 0x00003AF5
		public void ProcessRegularChanges()
		{
			this._navMeshUpdater.ProcessRegularChanges(this._regularNavMeshUpdateBuilder);
			this._districtUpdater.ProcessRegularChanges(this._regularNavMeshUpdateBuilder);
			this._restrictedNodeUpdater.ProcessRegularChanges();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005924 File Offset: 0x00003B24
		public void ProcessPreviewChanges()
		{
			this._navMeshUpdater.ProcessPreviewChanges(this._previewNavMeshUpdateBuilder);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005937 File Offset: 0x00003B37
		public void ProcessInstantChanges()
		{
			this._navMeshUpdater.ProcessInstantChanges(this._instantNavMeshUpdateBuilder);
			this._districtUpdater.ProcessInstantChanges(this._instantNavMeshUpdateBuilder);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000595C File Offset: 0x00003B5C
		public void NotifyAllNavmeshChanges()
		{
			if (!this._regularNavMeshUpdateBuilder.IsEmpty)
			{
				NavMeshUpdate navMeshUpdate = this._regularNavMeshUpdateBuilder.Build();
				this._navMeshUpdateNotifier.NotifyOfNavMeshUpdates(navMeshUpdate);
				this._regularNavMeshUpdateBuilder.Reset();
			}
			if (!this._previewNavMeshUpdateBuilder.IsEmpty)
			{
				NavMeshUpdate navMeshUpdate2 = this._previewNavMeshUpdateBuilder.Build();
				this._navMeshUpdateNotifier.NotifyOfPreviewNavMeshUpdates(navMeshUpdate2);
				this._previewNavMeshUpdateBuilder.Reset();
			}
			if (!this._instantNavMeshUpdateBuilder.IsEmpty)
			{
				NavMeshUpdate navMeshUpdate3 = this._instantNavMeshUpdateBuilder.Build();
				this._navMeshUpdateNotifier.NotifyOfInstantNavMeshUpdates(navMeshUpdate3);
				this._navMeshUpdateNotifier.NotifyOfPreviewNavMeshUpdates(navMeshUpdate3);
				this._instantNavMeshUpdateBuilder.Reset();
			}
		}

		// Token: 0x04000099 RID: 153
		public readonly NavMeshUpdater _navMeshUpdater;

		// Token: 0x0400009A RID: 154
		public readonly NavMeshUpdateNotifier _navMeshUpdateNotifier;

		// Token: 0x0400009B RID: 155
		public readonly DistrictUpdater _districtUpdater;

		// Token: 0x0400009C RID: 156
		public readonly NavMeshUpdateBuilderFactory _navMeshUpdateBuilderFactory;

		// Token: 0x0400009D RID: 157
		public readonly RestrictedNodeUpdater _restrictedNodeUpdater;

		// Token: 0x0400009E RID: 158
		public NavMeshUpdate.Builder _regularNavMeshUpdateBuilder;

		// Token: 0x0400009F RID: 159
		public NavMeshUpdate.Builder _previewNavMeshUpdateBuilder;

		// Token: 0x040000A0 RID: 160
		public NavMeshUpdate.Builder _instantNavMeshUpdateBuilder;
	}
}
