using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000011 RID: 17
	public class DestroyEmptyChildrenPrefabOptimizer : IPrefabOptimizer
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000033AD File Offset: 0x000015AD
		public void Optimize(GameObject prefab)
		{
			DestroyEmptyChildrenPrefabOptimizer.VisitChildren(prefab);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000033B8 File Offset: 0x000015B8
		public static void VisitChildren(GameObject visitee)
		{
			Transform transform = visitee.transform;
			int i = 0;
			while (i < transform.childCount)
			{
				if (!DestroyEmptyChildrenPrefabOptimizer.VisitGameObject(transform.GetChild(i).gameObject))
				{
					i++;
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000033F0 File Offset: 0x000015F0
		public static bool VisitGameObject(GameObject visitee)
		{
			DestroyEmptyChildrenPrefabOptimizer.VisitChildren(visitee);
			return DestroyEmptyChildrenPrefabOptimizer.DestroyIfEmpty(visitee);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000033FE File Offset: 0x000015FE
		public static bool DestroyIfEmpty(GameObject visitee)
		{
			if (!SpecialGameObjects.GameObjectIsRoot(visitee) && visitee.transform.childCount == 0 && visitee.GetComponents<Component>().Length == 1)
			{
				Object.DestroyImmediate(visitee);
				return true;
			}
			return false;
		}
	}
}
