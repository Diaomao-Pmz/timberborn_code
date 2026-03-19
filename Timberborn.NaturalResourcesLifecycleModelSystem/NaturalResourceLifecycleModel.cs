using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.NaturalResourcesLifecycle;
using UnityEngine;

namespace Timberborn.NaturalResourcesLifecycleModelSystem
{
	// Token: 0x02000004 RID: 4
	public class NaturalResourceLifecycleModel
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public NaturalResourceLifecycleModel(LivingNaturalResource livingNaturalResource, DyingNaturalResource dyingNaturalResource, GameObject aliveModel, GameObject dyingModel, GameObject deadModel)
		{
			this._livingNaturalResource = livingNaturalResource;
			this._dyingNaturalResource = dyingNaturalResource;
			this._aliveModel = aliveModel;
			this._dyingModel = dyingModel;
			this._deadModel = deadModel;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public static NaturalResourceLifecycleModel Create(BaseComponent naturalResource, GameObject modelsParent, string parentModelName)
		{
			LivingNaturalResource component = naturalResource.GetComponent<LivingNaturalResource>();
			DyingNaturalResource component2 = naturalResource.GetComponent<DyingNaturalResource>();
			GameObject model = NaturalResourceLifecycleModel.GetModel(modelsParent, parentModelName, "#Alive");
			GameObject model2 = NaturalResourceLifecycleModel.GetModel(modelsParent, parentModelName, "#Dying");
			GameObject model3 = NaturalResourceLifecycleModel.GetModel(modelsParent, parentModelName, "#Dead");
			return new NaturalResourceLifecycleModel(component, component2, model, model2, model3);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002134 File Offset: 0x00000334
		public void Show()
		{
			bool isDying = this._dyingNaturalResource.IsDying;
			bool isDead = this._livingNaturalResource.IsDead;
			this._aliveModel.SetActive(!isDead && !isDying);
			this._dyingModel.SetActive(!isDead && isDying);
			if (this._deadModel)
			{
				this._deadModel.SetActive(isDead);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002198 File Offset: 0x00000398
		public void Hide()
		{
			this._aliveModel.SetActive(false);
			this._dyingModel.SetActive(false);
			if (this._deadModel)
			{
				this._deadModel.SetActive(false);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021CC File Offset: 0x000003CC
		public static GameObject GetModel(GameObject modelsParent, string parentModelName, string modelName)
		{
			return modelsParent.GetDirectChildren().Single((GameObject gameObject) => gameObject.name == parentModelName).GetDirectChildren().SingleOrDefault((GameObject gameObject) => gameObject.name == modelName);
		}

		// Token: 0x04000006 RID: 6
		public readonly LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000007 RID: 7
		public readonly DyingNaturalResource _dyingNaturalResource;

		// Token: 0x04000008 RID: 8
		public readonly GameObject _aliveModel;

		// Token: 0x04000009 RID: 9
		public readonly GameObject _dyingModel;

		// Token: 0x0400000A RID: 10
		public readonly GameObject _deadModel;
	}
}
