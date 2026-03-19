using System;
using Timberborn.AssetSystem;
using UnityEngine;

namespace Timberborn.MainMenuScene
{
	// Token: 0x02000006 RID: 6
	public class EditorBuildValidator
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002236 File Offset: 0x00000436
		public EditorBuildValidator(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002248 File Offset: 0x00000448
		public void Validate()
		{
			try
			{
				if (!Application.isEditor && this._assetLoader.Load<TextAsset>("EditorBuild"))
				{
					throw new ApplicationException("EditorBuild detected outside of Unity Editor.");
				}
			}
			catch (InvalidOperationException)
			{
			}
		}

		// Token: 0x0400000C RID: 12
		public readonly IAssetLoader _assetLoader;
	}
}
