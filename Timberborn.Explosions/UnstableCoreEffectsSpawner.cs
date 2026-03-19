using System;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x0200001A RID: 26
	public class UnstableCoreEffectsSpawner : BaseComponent, IAwakableComponent
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x0000429A File Offset: 0x0000249A
		public UnstableCoreEffectsSpawner(IAssetLoader assetLoader, IInstantiator instantiator, ExplosionSoundPlayer explosionSoundPlayer)
		{
			this._assetLoader = assetLoader;
			this._instantiator = instantiator;
			this._explosionSoundPlayer = explosionSoundPlayer;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000042B8 File Offset: 0x000024B8
		public void Awake()
		{
			UnstableCoreEffectsSpawnerSpec component = base.GetComponent<UnstableCoreEffectsSpawnerSpec>();
			this._explosionPrefab = this._assetLoader.Load<GameObject>(component.ExplosionPrefabPath);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000042E4 File Offset: 0x000024E4
		public void SpawnEffects()
		{
			BlockObjectCenter component = base.GetComponent<BlockObjectCenter>();
			GameObject gameObject = this._instantiator.Instantiate(this._explosionPrefab, null);
			gameObject.transform.position = component.WorldCenterAtBaseZ;
			this._explosionSoundPlayer.PlayGlobal(gameObject);
		}

		// Token: 0x0400007C RID: 124
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400007D RID: 125
		public readonly IInstantiator _instantiator;

		// Token: 0x0400007E RID: 126
		public readonly ExplosionSoundPlayer _explosionSoundPlayer;

		// Token: 0x0400007F RID: 127
		public GameObject _explosionPrefab;
	}
}
