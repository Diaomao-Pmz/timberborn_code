using System;
using UnityEngine;

namespace Bindito.Unity
{
	// Token: 0x0200006D RID: 109
	public class InstantiatingSceneInitializer<T> : ISceneInitializer where T : MonoBehaviour
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x0000291E File Offset: 0x00000B1E
		public InstantiatingSceneInitializer(IInstantiator instantiator)
		{
			this._instantiator = instantiator;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002930 File Offset: 0x00000B30
		public void InitializeScene()
		{
			GameObject gameObject = this._instantiator.InstantiateEmpty(typeof(T).Name);
			this._instantiator.AddComponent<T>(gameObject);
		}

		// Token: 0x04000068 RID: 104
		private readonly IInstantiator _instantiator;
	}
}
