using System;
using UnityEngine;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x0200000C RID: 12
	public static class GameObjectExtensions
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002B98 File Offset: 0x00000D98
		public static T GetComponentSlow<T>(this GameObject instance)
		{
			ComponentCache component = instance.GetComponent<ComponentCache>();
			if (component != null)
			{
				return component.GetCachedComponent<T>();
			}
			return default(T);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static T GetComponentInParentSlow<T>(this GameObject instance)
		{
			Transform transform = instance.transform;
			while (transform)
			{
				ComponentCache component = transform.GetComponent<ComponentCache>();
				if (component != null)
				{
					T cachedComponent = component.GetCachedComponent<T>();
					if (cachedComponent != null)
					{
						return cachedComponent;
					}
				}
				transform = transform.parent;
			}
			return default(T);
		}
	}
}
