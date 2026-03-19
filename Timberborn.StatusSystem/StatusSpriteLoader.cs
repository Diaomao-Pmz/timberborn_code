using System;
using System.IO;
using Timberborn.AssetSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000024 RID: 36
	public class StatusSpriteLoader
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x000045F0 File Offset: 0x000027F0
		public StatusSpriteLoader(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004600 File Offset: 0x00002800
		public Sprite LoadSprite(string spriteName)
		{
			string path = Path.Combine(StatusSpriteLoader.StatusSpriteDirectory, spriteName);
			return this._assetLoader.Load<Sprite>(path);
		}

		// Token: 0x04000084 RID: 132
		public static readonly string StatusSpriteDirectory = "Sprites/StatusIcons";

		// Token: 0x04000085 RID: 133
		public readonly IAssetLoader _assetLoader;
	}
}
