using System;
using Timberborn.AssetSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000014 RID: 20
	public class StatusIconCyclerFactory
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002CF1 File Offset: 0x00000EF1
		public StatusIconCyclerFactory(BoundsCalculator boundsCalculator, IAssetLoader assetLoader)
		{
			this._boundsCalculator = boundsCalculator;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D08 File Offset: 0x00000F08
		public GameObject CreateAsChild(Transform parent)
		{
			Vector3 position = parent.position;
			position.y = this._boundsCalculator.GetRendererYMaxBound(parent) + StatusIconCyclerFactory.YOffset;
			GameObject gameObject = Object.Instantiate<GameObject>(this.GetPrefab(), parent);
			gameObject.transform.position = position;
			return gameObject;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D4D File Offset: 0x00000F4D
		public GameObject GetPrefab()
		{
			if (!this._statusIconCyclerPrefab)
			{
				this._statusIconCyclerPrefab = this._assetLoader.Load<GameObject>(StatusIconCyclerFactory.IconCyclerPrefabPath);
			}
			return this._statusIconCyclerPrefab;
		}

		// Token: 0x04000036 RID: 54
		public static readonly string IconCyclerPrefabPath = "UI/Statuses/StatusIconCycler";

		// Token: 0x04000037 RID: 55
		public static readonly float YOffset = 1f;

		// Token: 0x04000038 RID: 56
		public readonly BoundsCalculator _boundsCalculator;

		// Token: 0x04000039 RID: 57
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400003A RID: 58
		public GameObject _statusIconCyclerPrefab;
	}
}
