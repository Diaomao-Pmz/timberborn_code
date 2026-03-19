using System;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlueprintSystem;
using Timberborn.PrefabOptimization;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x0200000E RID: 14
	public class GoodStackModelFactory : ILoadableSingleton
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000262D File Offset: 0x0000082D
		public GoodStackModelFactory(OptimizedPrefabInstantiator optimizedPrefabInstantiator, ISpecService specService)
		{
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
			this._specService = specService;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002643 File Offset: 0x00000843
		public void Load()
		{
			this._goodStackModelTemplate = this._specService.GetBlueprint(GoodStackModelFactory.GoodStackModelPath);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000265C File Offset: 0x0000085C
		public void Create(GoodStack owner)
		{
			GameObject fullModel = owner.GetComponent<BlockObjectModel>().FullModel;
			GameObject gameObject = this._optimizedPrefabInstantiator.InstantiateInactive(this._goodStackModelTemplate, fullModel.transform);
			owner.GetComponent<EntityMaterials>().AddMaterials(gameObject);
			owner.GetComponent<GoodStackModel>().Initialize(gameObject, this._goodStackModelTemplate.GetSpec<GoodStackModelSpec>());
		}

		// Token: 0x04000020 RID: 32
		public static readonly string GoodStackModelPath = "Environment/GoodStack/GoodStackModel.blueprint";

		// Token: 0x04000021 RID: 33
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x04000022 RID: 34
		public readonly ISpecService _specService;

		// Token: 0x04000023 RID: 35
		public Blueprint _goodStackModelTemplate;
	}
}
