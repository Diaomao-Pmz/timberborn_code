using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.AssetSystem
{
	// Token: 0x0200000C RID: 12
	public interface IAssetProvider
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000020 RID: 32
		bool IsBuiltIn { get; }

		// Token: 0x06000021 RID: 33
		bool TryLoad<T>(string path, out OrderedAsset orderedAsset) where T : Object;

		// Token: 0x06000022 RID: 34
		IEnumerable<OrderedAsset> LoadAll<T>(string path) where T : Object;

		// Token: 0x06000023 RID: 35
		void Reset();
	}
}
