using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TextureOperations;
using Unity.Collections;
using UnityEngine;

namespace Timberborn.SpriteOperations
{
	// Token: 0x0200000C RID: 12
	public class SpriteResizer : IUnloadableSingleton
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002494 File Offset: 0x00000694
		public SpriteResizer(TextureFactory textureFactory)
		{
			this._textureFactory = textureFactory;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024BC File Offset: 0x000006BC
		public void Unload()
		{
			for (int i = 0; i < this._createdSprites.Count; i++)
			{
				Object.Destroy(this._createdSprites[i].texture);
				Object.Destroy(this._createdSprites[i]);
			}
			this._spritesMap.Clear();
			this._createdSprites.Clear();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000251C File Offset: 0x0000071C
		public Sprite GetResizedSprite(Sprite original, int size)
		{
			SpriteResizer.TargetSprite targetSprite = new SpriteResizer.TargetSprite(original, size);
			return this._spritesMap.GetOrAdd(targetSprite, () => this.GetResizedSprite(targetSprite));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002560 File Offset: 0x00000760
		public Sprite GetResizedSprite(SpriteResizer.TargetSprite targetSprite)
		{
			if (targetSprite.HasMipmaps)
			{
				return this.CreateResizedSprite(targetSprite);
			}
			return targetSprite.Original;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000257C File Offset: 0x0000077C
		public Sprite CreateResizedSprite(SpriteResizer.TargetSprite targetSprite)
		{
			Texture2D texture2D = this.CreateResizedTexture(targetSprite);
			Sprite original = targetSprite.Original;
			Sprite sprite = Sprite.Create(texture2D, original.rect, original.pivot, original.pixelsPerUnit);
			this._createdSprites.Add(sprite);
			return sprite;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025C0 File Offset: 0x000007C0
		public Texture2D CreateResizedTexture(SpriteResizer.TargetSprite targetSprite)
		{
			Texture2D texture = targetSprite.Original.texture;
			int mipMapCount = SpriteResizer.GetMipMapCount(targetSprite, texture);
			TextureSettings textureSettings = new TextureSettings.Builder().SetSize(texture.width, texture.height).SetTextureFormat(texture.format).SetMipmapCount(mipMapCount).SetIgnoreMipmapLimits(true).Build();
			Texture2D texture2D = this._textureFactory.CreateTexture(textureSettings);
			for (int i = 0; i < texture2D.mipmapCount; i++)
			{
				NativeArray<Color32> pixelData = texture.GetPixelData<Color32>(i);
				texture2D.SetPixelData<Color32>(pixelData, i, 0);
			}
			texture2D.Apply(false, true);
			return texture2D;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002655 File Offset: 0x00000855
		public static int GetMipMapCount(SpriteResizer.TargetSprite targetSprite, Texture2D originalTexture)
		{
			return Math.Clamp((int)Math.Ceiling(Math.Log((double)(originalTexture.width / (targetSprite.Size - 1))) / Math.Log(2.0)) - 1, 0, originalTexture.mipmapCount - 1) + 1;
		}

		// Token: 0x0400000E RID: 14
		public readonly TextureFactory _textureFactory;

		// Token: 0x0400000F RID: 15
		public readonly Dictionary<SpriteResizer.TargetSprite, Sprite> _spritesMap = new Dictionary<SpriteResizer.TargetSprite, Sprite>();

		// Token: 0x04000010 RID: 16
		public readonly List<Sprite> _createdSprites = new List<Sprite>();

		// Token: 0x0200000D RID: 13
		public readonly struct TargetSprite : IEquatable<SpriteResizer.TargetSprite>
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002694 File Offset: 0x00000894
			public Sprite Original { get; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600002A RID: 42 RVA: 0x0000269C File Offset: 0x0000089C
			public int Size { get; }

			// Token: 0x0600002B RID: 43 RVA: 0x000026A4 File Offset: 0x000008A4
			public TargetSprite(Sprite original, int size)
			{
				this.Original = original;
				this.Size = size;
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600002C RID: 44 RVA: 0x000026B4 File Offset: 0x000008B4
			public bool HasMipmaps
			{
				get
				{
					return this.Original.texture.mipmapCount > 1;
				}
			}

			// Token: 0x0600002D RID: 45 RVA: 0x000026C9 File Offset: 0x000008C9
			public bool Equals(SpriteResizer.TargetSprite other)
			{
				return object.Equals(this.Original, other.Original) && this.Size == other.Size;
			}

			// Token: 0x0600002E RID: 46 RVA: 0x000026F0 File Offset: 0x000008F0
			public override bool Equals(object obj)
			{
				if (obj is SpriteResizer.TargetSprite)
				{
					SpriteResizer.TargetSprite other = (SpriteResizer.TargetSprite)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002715 File Offset: 0x00000915
			public override int GetHashCode()
			{
				return HashCode.Combine<Sprite, int>(this.Original, this.Size);
			}
		}
	}
}
