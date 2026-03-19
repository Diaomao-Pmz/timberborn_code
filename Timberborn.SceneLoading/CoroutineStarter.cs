using System;
using System.Collections;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SceneLoading
{
	// Token: 0x02000004 RID: 4
	public class CoroutineStarter : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public CoroutineStarter(RootObjectProvider rootObjectProvider)
		{
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public void Load()
		{
			GameObject gameObject = this._rootObjectProvider.CreateRootObject("SceneLoader");
			Object.DontDestroyOnLoad(gameObject);
			this._monoBehaviour = gameObject.AddComponent<CoroutineStarter.CoroutineStarterMonoBehaviour>();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002100 File Offset: 0x00000300
		public void StartCoroutine(IEnumerator routine)
		{
			this._monoBehaviour.StartCoroutine(routine);
		}

		// Token: 0x04000006 RID: 6
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000007 RID: 7
		public CoroutineStarter.CoroutineStarterMonoBehaviour _monoBehaviour;

		// Token: 0x02000005 RID: 5
		public class CoroutineStarterMonoBehaviour : MonoBehaviour
		{
		}
	}
}
