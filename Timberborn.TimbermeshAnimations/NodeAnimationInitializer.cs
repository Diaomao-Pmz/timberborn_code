using System;
using System.Linq;
using Bindito.Unity;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000011 RID: 17
	public class NodeAnimationInitializer
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002B87 File Offset: 0x00000D87
		public NodeAnimationInitializer(IInstantiator instantiator, NodeAnimationCache nodeAnimationCache)
		{
			this._instantiator = instantiator;
			this._nodeAnimationCache = nodeAnimationCache;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public void InitializeAnimations(GameObject animatedObject, Node source)
		{
			if (source.NodeAnimations.Any<NodeAnimation>())
			{
				int animationSetId = this._nodeAnimationCache.CacheAnimations(source);
				this._instantiator.AddComponent<NodeAnimationUpdater>(animatedObject).AssignAnimationsId(animationSetId);
			}
		}

		// Token: 0x04000023 RID: 35
		public readonly IInstantiator _instantiator;

		// Token: 0x04000024 RID: 36
		public readonly NodeAnimationCache _nodeAnimationCache;
	}
}
