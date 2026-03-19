using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TextureOperations;
using UnityEngine;

namespace Timberborn.SpriteOperations
{
	// Token: 0x02000009 RID: 9
	public class SpriteFlipper : IUnloadableSingleton
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002286 File Offset: 0x00000486
		public SpriteFlipper(TextureFactory textureFactory)
		{
			this._textureFactory = textureFactory;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022A0 File Offset: 0x000004A0
		public void Unload()
		{
			foreach (Sprite sprite in this._spritesMap.Values)
			{
				Object.Destroy(sprite.texture);
				Object.Destroy(sprite);
			}
			this._spritesMap.Clear();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000230C File Offset: 0x0000050C
		public Sprite GetFlippedSprite(Sprite original)
		{
			return this._spritesMap.GetOrAdd(original, () => this.CreateFlippedSprite(original));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000234A File Offset: 0x0000054A
		public Sprite CreateFlippedSprite(Sprite original)
		{
			return Sprite.Create(this.CreateFlippedTexture(original), original.rect, original.pivot, original.pixelsPerUnit);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000236C File Offset: 0x0000056C
		public Texture2D CreateFlippedTexture(Sprite original)
		{
			Texture2D texture = original.texture;
			TextureSettings textureSettings = new TextureSettings.Builder().SetSize(texture.width, texture.height).SetTextureFormat(texture.format).SetMipmapCount(texture.mipmapCount).SetIgnoreMipmapLimits(texture.ignoreMipmapLimit).Build();
			Texture2D texture2D = this._textureFactory.CreateTexture(textureSettings);
			Color32[] pixels = texture.GetPixels32();
			Color32[] pixels2 = SpriteFlipper.FlipPixels(texture.width, texture.height, pixels);
			texture2D.SetPixels32(pixels2);
			texture2D.Apply(true, true);
			return texture2D;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023F4 File Offset: 0x000005F4
		public static Color32[] FlipPixels(int width, int height, Color32[] originalPixelData)
		{
			Color32[] array = new Color32[originalPixelData.Length];
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					array[i * width + j] = originalPixelData[i * width + width - 1 - j];
				}
			}
			return array;
		}

		// Token: 0x0400000A RID: 10
		public readonly TextureFactory _textureFactory;

		// Token: 0x0400000B RID: 11
		public readonly Dictionary<Sprite, Sprite> _spritesMap = new Dictionary<Sprite, Sprite>();
	}
}
