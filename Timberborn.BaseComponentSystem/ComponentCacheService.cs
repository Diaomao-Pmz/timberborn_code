using System;
using System.Collections.Generic;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x0200000B RID: 11
	public class ComponentCacheService
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002B38 File Offset: 0x00000D38
		public int GetComponentsCount(string name)
		{
			return CollectionExtensions.GetValueOrDefault<string, int>(this._components, name, ComponentCacheService.MinimumComponentsCount);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B4C File Offset: 0x00000D4C
		public void SaveComponentsCount(string name, int count)
		{
			int num;
			if (!this._components.TryGetValue(name, out num) || count > num)
			{
				this._components[name] = count;
			}
		}

		// Token: 0x0400001C RID: 28
		public static readonly int MinimumComponentsCount = 25;

		// Token: 0x0400001D RID: 29
		public readonly Dictionary<string, int> _components = new Dictionary<string, int>();
	}
}
