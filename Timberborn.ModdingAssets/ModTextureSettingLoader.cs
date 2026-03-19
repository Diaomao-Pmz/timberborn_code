using System;
using System.IO;
using Timberborn.SerializationSystem;
using Timberborn.TextureOperations;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x02000012 RID: 18
	public class ModTextureSettingLoader
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003618 File Offset: 0x00001818
		public TextureSettings Load(FileInfo fileInfo, SerializedObject metadata)
		{
			TextureSettings.Builder builder = new TextureSettings.Builder().SetName(Path.GetFileNameWithoutExtension(fileInfo.Name));
			if (metadata.Has("isSprite") && metadata.Get<bool>("isSprite"))
			{
				builder.SetSpritePreset();
			}
			if (metadata.Has("isNormalMap") && metadata.Get<bool>("isNormalMap"))
			{
				builder.SetNormalMapPreset();
			}
			if (metadata.Has("linear"))
			{
				builder.SetLinear(metadata.Get<bool>("linear"));
			}
			if (metadata.Has("generateMipmap"))
			{
				builder.SetGenerateMipmap(metadata.Get<bool>("generateMipmap"));
			}
			if (metadata.Has("mipmapCount"))
			{
				builder.SetMipmapCount(metadata.Get<int>("mipmapCount"));
			}
			if (metadata.Has("ignoreMipmapLimits"))
			{
				builder.SetIgnoreMipmapLimits(metadata.Get<bool>("ignoreMipmapLimits"));
			}
			FilterMode filterMode;
			if (metadata.Has("filterMode") && Enum.TryParse<FilterMode>(metadata.Get<string>("filterMode"), out filterMode))
			{
				builder.SetFilterMode(filterMode);
			}
			TextureWrapMode wrapMode;
			if (metadata.Has("wrapMode") && Enum.TryParse<TextureWrapMode>(metadata.Get<string>("wrapMode"), out wrapMode))
			{
				builder.SetWrapMode(wrapMode);
			}
			TextureFormat textureFormat;
			if (metadata.Has("textureFormat") && Enum.TryParse<TextureFormat>(metadata.Get<string>("textureFormat"), out textureFormat))
			{
				builder.SetTextureFormat(textureFormat);
			}
			if (metadata.Has("anisoLevel"))
			{
				builder.SetAnisoLevel(metadata.Get<int>("anisoLevel"));
			}
			if (metadata.Has("width") && metadata.Has("height"))
			{
				builder.SetSize(metadata.Get<int>("width"), metadata.Get<int>("height"));
			}
			return builder.Build();
		}
	}
}
