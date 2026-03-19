using System;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.MechanicalSystem;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.MechanicalConnectorSystem
{
	// Token: 0x02000008 RID: 8
	public class MechanicalConnectorFactory : ILoadableSingleton
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002396 File Offset: 0x00000596
		public MechanicalConnectorFactory(OptimizedPrefabInstantiator optimizedPrefabInstantiator, IAssetLoader assetLoader, ISpecService specService)
		{
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
			this._assetLoader = assetLoader;
			this._specService = specService;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023B4 File Offset: 0x000005B4
		public void Load()
		{
			MechanicalConnectorFactorySpec singleSpec = this._specService.GetSingleSpec<MechanicalConnectorFactorySpec>();
			this._mechanicalConnectorPrefab = this._assetLoader.Load<GameObject>(singleSpec.MechanicalConnectorPrefabPath);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023E4 File Offset: 0x000005E4
		public void Create(Transput transput, Transform parent, MechanicalConnectors mechanicalConnectors)
		{
			GameObject gameObject = this._optimizedPrefabInstantiator.Instantiate(this._mechanicalConnectorPrefab, parent);
			Vector3 vector = CoordinateSystem.GridToWorld(transput.Offset) + new Vector3(0.5f, 0.5f, 0.5f);
			Quaternion quaternion = transput.BaseDirection.ToRotation();
			gameObject.transform.SetLocalPositionAndRotation(vector, quaternion);
			mechanicalConnectors.Add(transput, gameObject);
		}

		// Token: 0x0400000F RID: 15
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x04000010 RID: 16
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000011 RID: 17
		public readonly ISpecService _specService;

		// Token: 0x04000012 RID: 18
		public GameObject _mechanicalConnectorPrefab;
	}
}
