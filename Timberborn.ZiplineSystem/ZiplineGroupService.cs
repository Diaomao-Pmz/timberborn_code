using System;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000019 RID: 25
	public class ZiplineGroupService : ILoadableSingleton
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003F05 File Offset: 0x00002105
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003F0D File Offset: 0x0000210D
		public int RegularGroupId { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003F16 File Offset: 0x00002116
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003F1E File Offset: 0x0000211E
		public int PathStartGroupId { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003F27 File Offset: 0x00002127
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003F2F File Offset: 0x0000212F
		public int TurnGroupId { get; private set; }

		// Token: 0x060000C2 RID: 194 RVA: 0x00003F38 File Offset: 0x00002138
		public ZiplineGroupService(NavMeshGroupService navMeshGroupService)
		{
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003F48 File Offset: 0x00002148
		public void Load()
		{
			this.RegularGroupId = this._navMeshGroupService.GetOrAddGroupId(ZiplineGroupService.RegularGroupName);
			this.PathStartGroupId = this._navMeshGroupService.GetOrAddGroupId(ZiplineGroupService.PathStartGroupName);
			this.TurnGroupId = this._navMeshGroupService.GetOrAddGroupId(ZiplineGroupService.TurnGroupName);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003F97 File Offset: 0x00002197
		public bool IsRegularEdge(int fromGroupId, int toGroupId)
		{
			return fromGroupId == this.RegularGroupId && toGroupId == this.RegularGroupId;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003FAD File Offset: 0x000021AD
		public bool IsTurnEdge(int fromGroupId, int toGroupId)
		{
			return fromGroupId == this.RegularGroupId && toGroupId == this.TurnGroupId;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003FC3 File Offset: 0x000021C3
		public bool IsAnyZiplineGroup(int groupId)
		{
			return groupId == this.RegularGroupId || groupId == this.PathStartGroupId || groupId == this.TurnGroupId;
		}

		// Token: 0x04000048 RID: 72
		public static readonly string RegularGroupName = "Zipline";

		// Token: 0x04000049 RID: 73
		public static readonly string PathStartGroupName = "ZiplinePathStart";

		// Token: 0x0400004A RID: 74
		public static readonly string TurnGroupName = "ZiplineTurn";

		// Token: 0x0400004E RID: 78
		public readonly NavMeshGroupService _navMeshGroupService;
	}
}
