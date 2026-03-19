using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Bindito.Unity.Internal
{
	// Token: 0x02000074 RID: 116
	public class InactiveParent : MonoBehaviour
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00002EC8 File Offset: 0x000010C8
		public static GameObject InstantiatePrefabUnderInactiveParent(GameObject prefab)
		{
			return (GameObject)Object.Instantiate(prefab, InactiveParent.Instance.Value);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002EDF File Offset: 0x000010DF
		[UsedImplicitly]
		private void Awake()
		{
			GameObject gameObject = base.gameObject;
			Object.DontDestroyOnLoad(gameObject);
			gameObject.SetActive(false);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002EF3 File Offset: 0x000010F3
		private static Transform CreateInstance()
		{
			return new GameObject("InactiveParent", new Type[]
			{
				typeof(InactiveParent)
			}).transform;
		}

		// Token: 0x04000070 RID: 112
		private static readonly Lazy<Transform> Instance = new Lazy<Transform>(new Func<Transform>(InactiveParent.CreateInstance));
	}
}
