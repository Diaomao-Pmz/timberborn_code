using System;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.RootProviders;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000009 RID: 9
	public class CameraFactory
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022A9 File Offset: 0x000004A9
		public CameraFactory(IAssetLoader assetLoader, IInstantiator instantiator, RootObjectProvider rootObjectProvider)
		{
			this._assetLoader = assetLoader;
			this._instantiator = instantiator;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C8 File Offset: 0x000004C8
		public Camera Create(string name)
		{
			GameObject gameObject = this._assetLoader.Load<GameObject>(CameraFactory.CameraPrefabPath);
			GameObject gameObject2 = this._rootObjectProvider.CreateRootObject(name);
			return this._instantiator.Instantiate(gameObject, gameObject2.transform).GetComponent<Camera>();
		}

		// Token: 0x0400000E RID: 14
		public static readonly string CameraPrefabPath = "Camera/Camera";

		// Token: 0x0400000F RID: 15
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000010 RID: 16
		public readonly IInstantiator _instantiator;

		// Token: 0x04000011 RID: 17
		public readonly RootObjectProvider _rootObjectProvider;
	}
}
