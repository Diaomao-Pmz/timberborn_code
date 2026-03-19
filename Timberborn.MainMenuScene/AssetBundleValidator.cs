using System;
using System.IO;
using UnityEngine;

namespace Timberborn.MainMenuScene
{
	// Token: 0x02000004 RID: 4
	public class AssetBundleValidator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public void Validate()
		{
			if (Directory.Exists(Path.Combine(Application.streamingAssetsPath, "AssetBundles")))
			{
				throw new NotSupportedException("Loading AssetBundles from StreamingAssets is not supported.");
			}
		}
	}
}
