using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x0200001C RID: 28
	public static class GameObjectExtensions
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002F41 File Offset: 0x00001141
		public static IEnumerable<GameObject> GetDirectChildren(this GameObject gameObject)
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

		// Token: 0x0600005C RID: 92 RVA: 0x00002F51 File Offset: 0x00001151
		public static IEnumerable<GameObject> GetAllChildren(this GameObject gameObject)
		{
			Transform transform = gameObject.transform;
			int num;
			for (int i = 0; i < transform.childCount; i = num)
			{
				GameObject child = transform.GetChild(i).gameObject;
				yield return child;
				foreach (GameObject gameObject2 in child.GetAllChildren())
				{
					yield return gameObject2;
				}
				IEnumerator<GameObject> enumerator = null;
				child = null;
				num = i + 1;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F61 File Offset: 0x00001161
		public static GameObject FindChildIfNameNotEmpty(this GameObject gameObject, string childName)
		{
			if (!string.IsNullOrEmpty(childName))
			{
				return gameObject.FindChild(childName);
			}
			return null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F74 File Offset: 0x00001174
		public static GameObject FindChild(this GameObject gameObject, string childName)
		{
			return gameObject.FindChildTransform(childName).gameObject;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F84 File Offset: 0x00001184
		public static Transform FindChildTransform(this GameObject gameObject, string childName)
		{
			if (string.IsNullOrEmpty(childName))
			{
				throw new ArgumentException("Child name cannot be empty", "childName");
			}
			Transform transform = gameObject.FindChildRecursive(childName);
			if (transform == null)
			{
				throw new NullReferenceException("Child " + childName + " not found in " + gameObject.name);
			}
			return transform;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FD8 File Offset: 0x000011D8
		public static Transform FindChildRecursive(this GameObject gameObject, string childName)
		{
			Transform transform = gameObject.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child.name == childName)
				{
					return child;
				}
				Transform transform2 = child.gameObject.FindChildRecursive(childName);
				if (transform2 != null)
				{
					return transform2;
				}
			}
			return null;
		}
	}
}
