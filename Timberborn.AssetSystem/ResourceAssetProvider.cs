using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.AssetSystem
{
	// Token: 0x0200000F RID: 15
	public class ResourceAssetProvider : IAssetProvider
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002494 File Offset: 0x00000694
		public bool IsBuiltIn
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002497 File Offset: 0x00000697
		public bool TryLoad<T>(string path, out OrderedAsset orderedAsset) where T : Object
		{
			orderedAsset = new OrderedAsset(ResourceAssetProvider.ResourceAssetOrder, Resources.Load<T>(path));
			return orderedAsset.Asset != null;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024C0 File Offset: 0x000006C0
		public IEnumerable<OrderedAsset> LoadAll<T>(string path) where T : Object
		{
			return from asset in Resources.LoadAll<T>(path)
			select new OrderedAsset(ResourceAssetProvider.ResourceAssetOrder, asset);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024EC File Offset: 0x000006EC
		public void Reset()
		{
		}

		// Token: 0x04000013 RID: 19
		public static readonly int ResourceAssetOrder = -1;
	}
}
