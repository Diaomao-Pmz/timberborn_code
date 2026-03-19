using System;
using Bindito.Core;
using UnityEngine;

namespace Timberborn.SingletonSystem
{
	// Token: 0x02000015 RID: 21
	public class SingletonLifecycleUnityAdapter : MonoBehaviour
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002788 File Offset: 0x00000988
		[Inject]
		public void InjectDependencies(SingletonLifecycleService singletonLifecycleService)
		{
			this._singletonLifecycleService = singletonLifecycleService;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002791 File Offset: 0x00000991
		public void Start()
		{
			this._singletonLifecycleService.LoadAll();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000279E File Offset: 0x0000099E
		public void OnDestroy()
		{
			this._singletonLifecycleService.UnloadAll();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027AB File Offset: 0x000009AB
		public void Update()
		{
			this._singletonLifecycleService.UpdateAll();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027B8 File Offset: 0x000009B8
		public void LateUpdate()
		{
			this._singletonLifecycleService.LateUpdateAll();
		}

		// Token: 0x0400001D RID: 29
		[HideInInspector]
		public SingletonLifecycleService _singletonLifecycleService;
	}
}
