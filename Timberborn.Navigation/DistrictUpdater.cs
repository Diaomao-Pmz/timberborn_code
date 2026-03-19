using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x02000018 RID: 24
	public class DistrictUpdater
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00003D54 File Offset: 0x00001F54
		public DistrictUpdater(DistrictMap districtMap, InstantDistrictMap instantDistrictMap, PreviewDistrictMap previewDistrictMap, DistrictObstacleService districtObstacleService, InstantDistrictObstacleService instantDistrictObstacleService, PreviewDistrictObstacleService previewDistrictObstacleService)
		{
			this._districtMap = districtMap;
			this._instantDistrictMap = instantDistrictMap;
			this._previewDistrictMap = previewDistrictMap;
			this._districtObstacleService = districtObstacleService;
			this._instantDistrictObstacleService = instantDistrictObstacleService;
			this._previewDistrictObstacleService = previewDistrictObstacleService;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003DAA File Offset: 0x00001FAA
		public void EnqueueChange(DistrictChange districtChange)
		{
			this._enqueuedInstantChanges.Enqueue(districtChange);
			this._enqueuedRegularChanges.Enqueue(districtChange);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public void ApplyPreviewChange(DistrictChange districtChange)
		{
			districtChange.ApplyChange(this._previewDistrictMap, this._previewDistrictObstacleService, null);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003DDC File Offset: 0x00001FDC
		public void ProcessRegularChanges(NavMeshUpdate.Builder navMeshUpdateBuilder)
		{
			if (!this._enqueuedRegularChanges.IsEmpty<DistrictChange>())
			{
				while (!this._enqueuedRegularChanges.IsEmpty<DistrictChange>())
				{
					this._enqueuedRegularChanges.Dequeue().ApplyChange(this._districtMap, this._districtObstacleService, navMeshUpdateBuilder);
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003E28 File Offset: 0x00002028
		public void ProcessInstantChanges(NavMeshUpdate.Builder navMeshUpdateBuilder)
		{
			if (!this._enqueuedInstantChanges.IsEmpty<DistrictChange>())
			{
				while (!this._enqueuedInstantChanges.IsEmpty<DistrictChange>())
				{
					DistrictChange districtChange = this._enqueuedInstantChanges.Dequeue();
					districtChange.ApplyChange(this._instantDistrictMap, this._instantDistrictObstacleService, navMeshUpdateBuilder);
					this.ApplyPreviewChange(districtChange);
				}
			}
		}

		// Token: 0x04000058 RID: 88
		public readonly DistrictMap _districtMap;

		// Token: 0x04000059 RID: 89
		public readonly InstantDistrictMap _instantDistrictMap;

		// Token: 0x0400005A RID: 90
		public readonly PreviewDistrictMap _previewDistrictMap;

		// Token: 0x0400005B RID: 91
		public readonly DistrictObstacleService _districtObstacleService;

		// Token: 0x0400005C RID: 92
		public readonly InstantDistrictObstacleService _instantDistrictObstacleService;

		// Token: 0x0400005D RID: 93
		public readonly PreviewDistrictObstacleService _previewDistrictObstacleService;

		// Token: 0x0400005E RID: 94
		public readonly Queue<DistrictChange> _enqueuedRegularChanges = new Queue<DistrictChange>();

		// Token: 0x0400005F RID: 95
		public readonly Queue<DistrictChange> _enqueuedInstantChanges = new Queue<DistrictChange>();
	}
}
