using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x02000058 RID: 88
	public class NavMeshGroupService
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x0000605C File Offset: 0x0000425C
		public int GetOrAddGroupId(string groupName)
		{
			if (string.IsNullOrWhiteSpace(groupName))
			{
				return NavMeshGroupService.DefaultGroupId;
			}
			int num;
			if (!this._groupNameToId.TryGetValue(groupName, out num))
			{
				int num2 = this._maxId + 1;
				this._maxId = num2;
				num = num2;
				this._groupNameToId.Add(groupName, num);
				this._groupIds.Add(num);
			}
			return num;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000060B3 File Offset: 0x000042B3
		public int GetDefaultGroupId()
		{
			return NavMeshGroupService.DefaultGroupId;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000060BA File Offset: 0x000042BA
		public ReadOnlyList<int> GetAllGroupIds()
		{
			return this._groupIds.AsReadOnlyList<int>();
		}

		// Token: 0x040000B8 RID: 184
		public static readonly int DefaultGroupId;

		// Token: 0x040000B9 RID: 185
		public readonly Dictionary<string, int> _groupNameToId = new Dictionary<string, int>();

		// Token: 0x040000BA RID: 186
		public readonly List<int> _groupIds = new List<int>
		{
			NavMeshGroupService.DefaultGroupId
		};

		// Token: 0x040000BB RID: 187
		public int _maxId;
	}
}
