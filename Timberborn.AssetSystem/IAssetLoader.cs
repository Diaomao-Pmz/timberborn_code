using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.AssetSystem
{
	// Token: 0x0200000B RID: 11
	public interface IAssetLoader
	{
		// Token: 0x0600001C RID: 28
		T Load<T>(string path) where T : Object;

		// Token: 0x0600001D RID: 29
		T LoadSafe<T>(string path) where T : Object;

		// Token: 0x0600001E RID: 30
		IEnumerable<LoadedAsset<T>> LoadAll<T>(string path) where T : Object;

		// Token: 0x0600001F RID: 31
		void Reset();
	}
}
