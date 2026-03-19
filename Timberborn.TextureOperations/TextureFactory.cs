using System;
using UnityEngine;

namespace Timberborn.TextureOperations
{
	// Token: 0x02000004 RID: 4
	public class TextureFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public Texture2D CreateTexture(TextureSettings textureSettings)
		{
			Texture2D texture2D = textureSettings.GenerateMipmap ? new Texture2D(textureSettings.Width, textureSettings.Height, textureSettings.TextureFormat, textureSettings.MipmapCount, textureSettings.Linear, true) : new Texture2D(textureSettings.Width, textureSettings.Height, textureSettings.TextureFormat, false, textureSettings.Linear, true);
			TextureFactory.ApplyTextureSettings(textureSettings, texture2D);
			return texture2D;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002124 File Offset: 0x00000324
		public Texture2D CreateTexture(TextureSettings textureSettings, byte[] bytes)
		{
			Texture2D texture2D = this.CreateTexture(textureSettings);
			ImageConversion.LoadImage(texture2D, bytes);
			TextureFactory.ApplyTextureSettings(textureSettings, texture2D);
			return texture2D;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002149 File Offset: 0x00000349
		public bool TryCreateTexture(TextureSettings textureSettings, byte[] bytes, out Texture2D texture)
		{
			texture = this.CreateTexture(textureSettings);
			if (ImageConversion.LoadImage(texture, bytes))
			{
				TextureFactory.ApplyTextureSettings(textureSettings, texture);
				return true;
			}
			Object.Destroy(texture);
			texture = null;
			return false;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002174 File Offset: 0x00000374
		public Texture2D CreateTexture(TextureSettings textureSettings, RenderTexture renderTexture)
		{
			Texture2D texture2D = this.CreateTexture(textureSettings);
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = renderTexture;
			texture2D.ReadPixels(new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), 0, 0);
			RenderTexture.active = active;
			return texture2D;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BF File Offset: 0x000003BF
		public static void ApplyTextureSettings(TextureSettings textureSettings, Texture2D texture)
		{
			texture.anisoLevel = textureSettings.AnisoLevel;
			texture.filterMode = textureSettings.FilterMode;
			texture.wrapMode = textureSettings.WrapMode;
			texture.ignoreMipmapLimit = textureSettings.IgnoreMipmapLimits;
			texture.name = textureSettings.Name;
		}
	}
}
