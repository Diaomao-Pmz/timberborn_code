using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintPrefabSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200001D RID: 29
	public class PrefabOptimizationChain : IPrefabOptimizationChain
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00004DEC File Offset: 0x00002FEC
		public PrefabOptimizationChain(IEnumerable<IPrefabOptimizer> prefabProcessors, BlueprintPrefabConverter blueprintPrefabConverter)
		{
			this._prefabProcessors = prefabProcessors.ToList<IPrefabOptimizer>();
			this._blueprintPrefabConverter = blueprintPrefabConverter;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004E40 File Offset: 0x00003040
		public GameObject Process(GameObject inputPrefab)
		{
			if (!this._prefabCache.ContainsKey(inputPrefab))
			{
				GameObject value = this.ProcessPrefab(inputPrefab);
				this._prefabCache.Add(inputPrefab, value);
			}
			return this._prefabCache[inputPrefab];
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004E7C File Offset: 0x0000307C
		public GameObject Process(Blueprint inputBlueprint)
		{
			if (!this._blueprintCache.ContainsKey(inputBlueprint))
			{
				GameObject value = this.ProcessPrefab(inputBlueprint);
				this._blueprintCache.Add(inputBlueprint, value);
			}
			return this._blueprintCache[inputBlueprint];
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004EB8 File Offset: 0x000030B8
		public ImmutableArray<GameObject> GetCached()
		{
			return this._prefabCache.Values.ToImmutableArray<GameObject>();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004ECA File Offset: 0x000030CA
		public static GameObject CreateRootGameObject()
		{
			GameObject gameObject = new GameObject(PrefabOptimizationChain.RootGameObjectName);
			gameObject.SetActive(false);
			return gameObject;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004EE0 File Offset: 0x000030E0
		public GameObject ProcessPrefab(GameObject inputPrefab)
		{
			if (this._prefabProcessors.IsEmpty<IPrefabOptimizer>())
			{
				return inputPrefab;
			}
			GameObject result;
			try
			{
				GameObject gameObject = Object.Instantiate<GameObject>(inputPrefab, this._rootGameObject.Value.transform);
				gameObject.name = inputPrefab.name;
				foreach (IPrefabOptimizer prefabOptimizer in this._prefabProcessors)
				{
					prefabOptimizer.Optimize(gameObject);
				}
				result = gameObject;
			}
			catch (Exception innerException)
			{
				throw new Exception("Processing prefab " + inputPrefab.name + " failed.", innerException);
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004F94 File Offset: 0x00003194
		public GameObject ProcessPrefab(Blueprint inputBlueprint)
		{
			return this.ProcessPrefab(this._blueprintPrefabConverter.Convert(inputBlueprint, this._rootGameObject.Value.transform));
		}

		// Token: 0x0400007B RID: 123
		public static readonly string RootGameObjectName = "OptimizedPrefabs";

		// Token: 0x0400007C RID: 124
		public readonly List<IPrefabOptimizer> _prefabProcessors;

		// Token: 0x0400007D RID: 125
		public readonly BlueprintPrefabConverter _blueprintPrefabConverter;

		// Token: 0x0400007E RID: 126
		public readonly Dictionary<GameObject, GameObject> _prefabCache = new Dictionary<GameObject, GameObject>();

		// Token: 0x0400007F RID: 127
		public readonly Dictionary<Blueprint, GameObject> _blueprintCache = new Dictionary<Blueprint, GameObject>();

		// Token: 0x04000080 RID: 128
		public readonly Lazy<GameObject> _rootGameObject = new Lazy<GameObject>(new Func<GameObject>(PrefabOptimizationChain.CreateRootGameObject));
	}
}
