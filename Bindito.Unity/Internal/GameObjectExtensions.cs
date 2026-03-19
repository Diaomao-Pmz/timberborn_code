using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bindito.Unity.Internal
{
	// Token: 0x02000073 RID: 115
	internal static class GameObjectExtensions
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00002EA8 File Offset: 0x000010A8
		public static IEnumerable<GameObject> GetSelfAndChildren(this GameObject gameObject)
		{
			yield return gameObject;
			foreach (GameObject gameObject2 in GameObjectExtensions.GetDirectChildren(gameObject))
			{
				foreach (GameObject gameObject3 in gameObject2.GetSelfAndChildren())
				{
					yield return gameObject3;
				}
				IEnumerator<GameObject> enumerator2 = null;
			}
			IEnumerator<GameObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00002EB8 File Offset: 0x000010B8
		private static IEnumerable<GameObject> GetDirectChildren(GameObject gameObject)
		{
			Transform transform = gameObject.transform;
			int num;
			for (int i = 0; i < transform.childCount; i = num)
			{
				yield return transform.GetChild(i).gameObject;
				num = i + 1;
			}
			yield break;
		}
	}
}
