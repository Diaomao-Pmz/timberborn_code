using System;
using System.Collections.Immutable;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000015 RID: 21
	public interface IPrefabOptimizationChain
	{
		// Token: 0x060000AD RID: 173
		GameObject Process(GameObject inputPrefab);

		// Token: 0x060000AE RID: 174
		ImmutableArray<GameObject> GetCached();

		// Token: 0x060000AF RID: 175
		GameObject Process(Blueprint inputBlueprint);
	}
}
