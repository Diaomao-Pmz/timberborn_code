using System;
using System.IO;
using Timberborn.TextureOperations;
using UnityEngine;

namespace Timberborn.SteamWorkshopModUploadingUI
{
	// Token: 0x02000006 RID: 6
	public class SteamWorkshopModThumbnail
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000023EC File Offset: 0x000005EC
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000023F4 File Offset: 0x000005F4
		public Texture2D Thumbnail { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x000023FD File Offset: 0x000005FD
		public SteamWorkshopModThumbnail(TextureFactory textureFactory, string modPath)
		{
			this._textureFactory = textureFactory;
			this._modPath = modPath;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002414 File Offset: 0x00000614
		public void UpdateThumbnail()
		{
			this.Clear();
			string path;
			if (this.TryGetCustomThumbnailPath(out path))
			{
				TextureSettings textureSettings = new TextureSettings.Builder().SetSpritePreset().Build();
				Texture2D thumbnail;
				if (this._textureFactory.TryCreateTexture(textureSettings, File.ReadAllBytes(path), out thumbnail))
				{
					this.Thumbnail = thumbnail;
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000245E File Offset: 0x0000065E
		public void Clear()
		{
			if (this.Thumbnail)
			{
				Object.Destroy(this.Thumbnail);
				this.Thumbnail = null;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002480 File Offset: 0x00000680
		public string GetThumbnailPath()
		{
			string result;
			if (!this.TryGetCustomThumbnailPath(out result))
			{
				return Path.Combine(Application.streamingAssetsPath, SteamWorkshopModThumbnail.DefaultPreviewAssetPath);
			}
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024A8 File Offset: 0x000006A8
		public bool TryGetCustomThumbnailPath(out string previewPath)
		{
			foreach (string path in SteamWorkshopModThumbnail.PreviewNames)
			{
				previewPath = Path.Combine(this._modPath, path);
				if (File.Exists(previewPath))
				{
					return true;
				}
			}
			previewPath = null;
			return false;
		}

		// Token: 0x04000010 RID: 16
		public static readonly string[] PreviewNames = new string[]
		{
			"thumbnail.png",
			"thumbnail.jpg",
			"thumbnail.jpeg"
		};

		// Token: 0x04000011 RID: 17
		public static readonly string DefaultPreviewAssetPath = Path.Combine("Modding", "mod-thumbnail.png");

		// Token: 0x04000013 RID: 19
		public readonly TextureFactory _textureFactory;

		// Token: 0x04000014 RID: 20
		public readonly string _modPath;
	}
}
