using System;
using Bindito.Unity;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200001C RID: 28
	public class OptimizedPrefabInstantiator
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00004D82 File Offset: 0x00002F82
		public OptimizedPrefabInstantiator(IInstantiator instantiator, IPrefabOptimizationChain prefabOptimizationChain)
		{
			this._instantiator = instantiator;
			this._prefabOptimizationChain = prefabOptimizationChain;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004D98 File Offset: 0x00002F98
		public GameObject Instantiate(GameObject prefab, Transform parent)
		{
			GameObject gameObject = this._prefabOptimizationChain.Process(prefab);
			return this._instantiator.Instantiate(gameObject, parent);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004DC0 File Offset: 0x00002FC0
		public GameObject InstantiateInactive(Blueprint blueprint, Transform parent)
		{
			GameObject gameObject = this._prefabOptimizationChain.Process(blueprint);
			bool flag;
			return this._instantiator.InstantiateInactive(gameObject, parent, ref flag);
		}

		// Token: 0x04000079 RID: 121
		public readonly IInstantiator _instantiator;

		// Token: 0x0400007A RID: 122
		public readonly IPrefabOptimizationChain _prefabOptimizationChain;
	}
}
