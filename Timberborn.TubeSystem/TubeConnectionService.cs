using System;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using Timberborn.PathSystem;
using UnityEngine;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000009 RID: 9
	public class TubeConnectionService
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000023A6 File Offset: 0x000005A6
		public TubeConnectionService(INavMeshService navMeshService, IPathService pathService)
		{
			this._navMeshService = navMeshService;
			this._pathService = pathService;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023BC File Offset: 0x000005BC
		public bool CanConnectInDirection(Vector3Int origin, Direction3D direction3D)
		{
			Vector3Int target = origin + direction3D.ToOffset();
			return this.CanConnect(origin, target);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023DE File Offset: 0x000005DE
		public bool CanConnect(Vector3Int origin, Vector3Int target)
		{
			return this.IsPath(origin, target);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023E8 File Offset: 0x000005E8
		public bool IsPath(Vector3Int origin, Vector3Int target)
		{
			return this._navMeshService.AreConnectedRoadPreview(origin, target) && this._pathService.IsPath(origin) && this._pathService.IsPath(target);
		}

		// Token: 0x04000012 RID: 18
		public readonly INavMeshService _navMeshService;

		// Token: 0x04000013 RID: 19
		public readonly IPathService _pathService;
	}
}
