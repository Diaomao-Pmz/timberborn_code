using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.SerializationSystem;
using Timberborn.TextureOperations;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x02000011 RID: 17
	public class ModTextureConverter : IModFileConverter<Texture2D>
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003513 File Offset: 0x00001713
		public ModTextureConverter(TextureFactory textureFactory, ModTextureSettingLoader modTextureSettingLoader)
		{
			this._textureFactory = textureFactory;
			this._modTextureSettingLoader = modTextureSettingLoader;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003534 File Offset: 0x00001734
		public bool CanConvert(FileInfo fileInfo)
		{
			return ModTextureConverter.ValidExtensions.Contains(fileInfo.Extension);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003548 File Offset: 0x00001748
		public bool TryConvert(OrderedFile orderedFile, string path, SerializedObject metadata, out Texture2D asset)
		{
			FileInfo file = orderedFile.File;
			TextureSettings textureSettings = this._modTextureSettingLoader.Load(file, metadata);
			if (this._textureFactory.TryCreateTexture(textureSettings, File.ReadAllBytes(file.FullName), out asset))
			{
				this._textures.Add(asset);
				return true;
			}
			return false;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003598 File Offset: 0x00001798
		public void Reset()
		{
			foreach (Texture2D texture2D in this._textures)
			{
				Object.Destroy(texture2D);
			}
			this._textures.Clear();
		}

		// Token: 0x0400003E RID: 62
		public static readonly List<string> ValidExtensions = new List<string>
		{
			".png",
			".jpg"
		};

		// Token: 0x0400003F RID: 63
		public readonly TextureFactory _textureFactory;

		// Token: 0x04000040 RID: 64
		public readonly ModTextureSettingLoader _modTextureSettingLoader;

		// Token: 0x04000041 RID: 65
		public readonly List<Texture2D> _textures = new List<Texture2D>();
	}
}
