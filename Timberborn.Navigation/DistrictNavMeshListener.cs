using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000012 RID: 18
	public class DistrictNavMeshListener : IPrioritizedSingletonNavMeshListener, IPrioritizedSingletonPreviewNavMeshListener, IPrioritizedSingletonInstantNavMeshListener
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000035DC File Offset: 0x000017DC
		public DistrictNavMeshListener(DistrictMap districtMap, PreviewDistrictMap previewDistrictMap, InstantDistrictMap instantDistrictMap)
		{
			this._districtMap = districtMap;
			this._previewDistrictMap = previewDistrictMap;
			this._instantDistrictMap = instantDistrictMap;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000035F9 File Offset: 0x000017F9
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._districtMap.OnNavMeshUpdated(navMeshUpdate);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003607 File Offset: 0x00001807
		public void OnPreviewNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._previewDistrictMap.OnNavMeshUpdated(navMeshUpdate);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003615 File Offset: 0x00001815
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._instantDistrictMap.OnNavMeshUpdated(navMeshUpdate);
		}

		// Token: 0x0400003A RID: 58
		public readonly DistrictMap _districtMap;

		// Token: 0x0400003B RID: 59
		public readonly PreviewDistrictMap _previewDistrictMap;

		// Token: 0x0400003C RID: 60
		public readonly InstantDistrictMap _instantDistrictMap;
	}
}
