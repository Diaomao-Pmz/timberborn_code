using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x02000012 RID: 18
	public class TypeIndexMap
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002C8A File Offset: 0x00000E8A
		public object GetIndex(Type requested)
		{
			return this._typeIndex[requested];
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C98 File Offset: 0x00000E98
		public bool TryGetIndex(Type requested, out object index)
		{
			return this._typeIndex.TryGetValue(requested, out index);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public void CacheType<T>(ReadOnlyList<object> components)
		{
			Type typeFromHandle = typeof(T);
			int num = 0;
			for (int i = 0; i < components.Count; i++)
			{
				if (components[i] is T)
				{
					num++;
					this.CacheComponent(num, typeFromHandle, i);
				}
			}
			if (num == 0)
			{
				this._typeIndex[typeFromHandle] = null;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D00 File Offset: 0x00000F00
		public void CacheComponent(int count, Type key, int index)
		{
			if (count == 1)
			{
				this._typeIndex[key] = index;
				return;
			}
			if (count != 2)
			{
				((List<int>)this._typeIndex[key]).Add(index);
				return;
			}
			List<int> value = new List<int>
			{
				(int)this._typeIndex[key],
				index
			};
			this._typeIndex[key] = value;
		}

		// Token: 0x0400001F RID: 31
		public readonly Dictionary<Type, object> _typeIndex = new Dictionary<Type, object>();
	}
}
