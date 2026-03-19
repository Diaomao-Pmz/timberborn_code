using System;
using Timberborn.Coordinates;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x0200000D RID: 13
	public class ModularShaftModelService : ILoadableSingleton
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002799 File Offset: 0x00000999
		public ModularShaftModelService(RootObjectProvider rootObjectProvider, ShaftModelFactory shaftModelFactory)
		{
			this._rootObjectProvider = rootObjectProvider;
			this._shaftModelFactory = shaftModelFactory;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027C5 File Offset: 0x000009C5
		public void Load()
		{
			this._root = this._rootObjectProvider.CreateRootObject("ModularShaftModelService").transform;
			this.BuildAllVariants();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027E8 File Offset: 0x000009E8
		public OrientedValue<GameObject> GetModel(ShaftVariant variant)
		{
			return this._nonStackableShafts.GetMatch(variant);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000027F6 File Offset: 0x000009F6
		public OrientedValue<GameObject> GetStackableModel(ShaftVariant variant)
		{
			return this._stackableShafts.GetMatch(variant);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002804 File Offset: 0x00000A04
		public void BuildAllVariants()
		{
			foreach (ShaftVariant variant in ShaftVariants.GetAllVariants())
			{
				if (!this._nonStackableShafts.Contains(variant) && variant.Top == 0)
				{
					GameObject value = this.BuildModel(variant, false);
					this._nonStackableShafts.AddVariant(value, variant);
				}
				if (!this._stackableShafts.Contains(variant))
				{
					GameObject value2 = this.BuildModel(variant, true);
					this._stackableShafts.AddVariant(value2, variant);
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000289C File Offset: 0x00000A9C
		public GameObject BuildModel(ShaftVariant variant, bool isStackable)
		{
			GameObject gameObject = new GameObject(variant.GetName() + (isStackable ? "S" : string.Empty));
			gameObject.transform.SetParent(this._root);
			if (isStackable)
			{
				this._shaftModelFactory.BuildStackable(variant, gameObject);
			}
			else
			{
				this._shaftModelFactory.BuildNonStackable(variant, gameObject);
			}
			gameObject.SetActive(false);
			return gameObject;
		}

		// Token: 0x0400001E RID: 30
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400001F RID: 31
		public readonly ShaftModelFactory _shaftModelFactory;

		// Token: 0x04000020 RID: 32
		public Transform _root;

		// Token: 0x04000021 RID: 33
		public readonly ModularShaftOrientedVariants _nonStackableShafts = new ModularShaftOrientedVariants();

		// Token: 0x04000022 RID: 34
		public readonly ModularShaftOrientedVariants _stackableShafts = new ModularShaftOrientedVariants();
	}
}
