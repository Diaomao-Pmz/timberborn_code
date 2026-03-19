using System;
using System.IO;
using Timberborn.TextureOperations;
using Timberborn.ThumbnailSystem;
using UnityEngine;

namespace Timberborn.ThumbnailCapturing
{
	// Token: 0x02000009 RID: 9
	public class ThumbnailSaveEntryWriter
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000223C File Offset: 0x0000043C
		public ThumbnailSaveEntryWriter(ThumbnailRenderer thumbnailRenderer, IThumbnailRenderTextureProvider thumbnailRenderTextureProvider, ThumbnailSerializer thumbnailSerializer, TextureFactory textureFactory)
		{
			this._thumbnailRenderer = thumbnailRenderer;
			this._thumbnailRenderTextureProvider = thumbnailRenderTextureProvider;
			this._thumbnailSerializer = thumbnailSerializer;
			this._textureFactory = textureFactory;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002264 File Offset: 0x00000464
		public void WriteToSaveEntryStream(Stream entryStream, IThumbnailConfiguration thumbnailConfiguration, Texture2D overlay = null)
		{
			if (this._thumbnailRenderTextureProvider.RenderTexture)
			{
				this._thumbnailRenderer.Render();
				TextureSettings textureSettings = new TextureSettings.Builder().SetSize(thumbnailConfiguration.Width, thumbnailConfiguration.Height).SetTextureFormat(thumbnailConfiguration.TextureFormat).SetGenerateMipmap(false).Build();
				RenderTexture renderTexture = this._thumbnailRenderTextureProvider.RenderTexture;
				Texture2D texture2D = this._textureFactory.CreateTexture(textureSettings, renderTexture);
				if (overlay)
				{
					ThumbnailSaveEntryWriter.AddOverlay(texture2D, overlay);
				}
				texture2D.Apply(false, false);
				this._thumbnailSerializer.WriteToSaveEntryStream(entryStream, texture2D, thumbnailConfiguration);
				Object.Destroy(texture2D);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002300 File Offset: 0x00000500
		public static void AddOverlay(Texture2D thumbnail, Texture2D overlay)
		{
			int num = (thumbnail.width - overlay.width) / 2;
			int num2 = (thumbnail.height - overlay.height) / 2;
			for (int i = 0; i < overlay.width; i++)
			{
				for (int j = 0; j < overlay.height; j++)
				{
					Color pixel = thumbnail.GetPixel(i + num, j + num2);
					Color pixel2 = overlay.GetPixel(i, j);
					Color color = Color.Lerp(pixel, pixel2, pixel2.a / 1f);
					thumbnail.SetPixel(i + num, j + num2, color);
				}
			}
		}

		// Token: 0x0400000C RID: 12
		public readonly ThumbnailRenderer _thumbnailRenderer;

		// Token: 0x0400000D RID: 13
		public readonly IThumbnailRenderTextureProvider _thumbnailRenderTextureProvider;

		// Token: 0x0400000E RID: 14
		public readonly ThumbnailSerializer _thumbnailSerializer;

		// Token: 0x0400000F RID: 15
		public readonly TextureFactory _textureFactory;
	}
}
