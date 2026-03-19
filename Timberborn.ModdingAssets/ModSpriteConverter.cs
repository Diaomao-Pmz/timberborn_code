using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.SerializationSystem;
using Timberborn.TextureOperations;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x0200000D RID: 13
	public class ModSpriteConverter : IModFileConverter<Sprite>
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public ModSpriteConverter(TextureFactory textureFactory, ModTextureSettingLoader modTextureSettingLoader)
		{
			this._textureFactory = textureFactory;
			this._modTextureSettingLoader = modTextureSettingLoader;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D1C File Offset: 0x00000F1C
		public bool CanConvert(FileInfo fileInfo)
		{
			return ModSpriteConverter.ValidExtensions.Contains(fileInfo.Extension);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D30 File Offset: 0x00000F30
		public bool TryConvert(OrderedFile orderedFile, string path, SerializedObject metadata, out Sprite asset)
		{
			FileInfo file = orderedFile.File;
			TextureSettings textureSettings = this._modTextureSettingLoader.Load(file, metadata);
			Texture2D texture2D;
			if (this._textureFactory.TryCreateTexture(textureSettings, File.ReadAllBytes(file.FullName), out texture2D))
			{
				asset = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
				this._textures.Add(texture2D);
				this._sprites.Add(asset);
				return true;
			}
			asset = null;
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public void Reset()
		{
			foreach (Sprite sprite in this._sprites)
			{
				Object.Destroy(sprite);
			}
			foreach (Texture2D texture2D in this._textures)
			{
				Object.Destroy(texture2D);
			}
			this._textures.Clear();
			this._sprites.Clear();
		}

		// Token: 0x04000027 RID: 39
		public static readonly List<string> ValidExtensions = new List<string>
		{
			".png",
			".jpg"
		};

		// Token: 0x04000028 RID: 40
		public readonly TextureFactory _textureFactory;

		// Token: 0x04000029 RID: 41
		public readonly ModTextureSettingLoader _modTextureSettingLoader;

		// Token: 0x0400002A RID: 42
		public readonly List<Texture2D> _textures = new List<Texture2D>();

		// Token: 0x0400002B RID: 43
		public readonly List<Sprite> _sprites = new List<Sprite>();
	}
}
