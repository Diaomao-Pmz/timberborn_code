using System;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x0200000E RID: 14
	public class BlockObjectNavMeshGroupInitializer : ILoadableSingleton
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002BF5 File Offset: 0x00000DF5
		public BlockObjectNavMeshGroupInitializer(TemplateService templateService, NavMeshGroupService navMeshGroupService)
		{
			this._templateService = templateService;
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002C0C File Offset: 0x00000E0C
		public void Load()
		{
			foreach (BlockObjectNavMeshSettingsSpec blockObjectNavMeshSettingsSpec in this._templateService.GetAll<BlockObjectNavMeshSettingsSpec>())
			{
				foreach (BlockObjectNavMeshEdgeGroupSpec blockObjectNavMeshEdgeGroupSpec in blockObjectNavMeshSettingsSpec.EdgeGroups)
				{
					if (blockObjectNavMeshEdgeGroupSpec.UseGroup)
					{
						this._navMeshGroupService.GetOrAddGroupId(blockObjectNavMeshEdgeGroupSpec.GroupName);
					}
				}
			}
		}

		// Token: 0x0400001B RID: 27
		public readonly TemplateService _templateService;

		// Token: 0x0400001C RID: 28
		public readonly NavMeshGroupService _navMeshGroupService;
	}
}
