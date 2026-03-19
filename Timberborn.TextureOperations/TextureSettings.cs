using System;
using UnityEngine;

namespace Timberborn.TextureOperations
{
	// Token: 0x02000006 RID: 6
	public class TextureSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002217 File Offset: 0x00000417
		public bool Linear { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000221F File Offset: 0x0000041F
		public bool GenerateMipmap { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002227 File Offset: 0x00000427
		public int MipmapCount { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000222F File Offset: 0x0000042F
		public bool IgnoreMipmapLimits { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002237 File Offset: 0x00000437
		public FilterMode FilterMode { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000223F File Offset: 0x0000043F
		public TextureWrapMode WrapMode { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002247 File Offset: 0x00000447
		public TextureFormat TextureFormat { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000224F File Offset: 0x0000044F
		public int AnisoLevel { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002257 File Offset: 0x00000457
		public int Width { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000225F File Offset: 0x0000045F
		public int Height { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002267 File Offset: 0x00000467
		public string Name { get; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002270 File Offset: 0x00000470
		public TextureSettings(bool linear, bool generateMipmap, int mipmapCount, bool ignoreMipmapLimits, FilterMode filterMode, TextureWrapMode wrapMode, TextureFormat textureFormat, int anisoLevel, int width, int height, string name)
		{
			this.Linear = linear;
			this.GenerateMipmap = generateMipmap;
			this.MipmapCount = mipmapCount;
			this.IgnoreMipmapLimits = ignoreMipmapLimits;
			this.FilterMode = filterMode;
			this.WrapMode = wrapMode;
			this.TextureFormat = textureFormat;
			this.AnisoLevel = anisoLevel;
			this.Width = width;
			this.Height = height;
			this.Name = name;
		}

		// Token: 0x02000007 RID: 7
		public class Builder
		{
			// Token: 0x06000017 RID: 23 RVA: 0x000022D8 File Offset: 0x000004D8
			public TextureSettings.Builder SetSpritePreset()
			{
				return this.SetGenerateMipmap(false).SetWrapMode(1);
			}

			// Token: 0x06000018 RID: 24 RVA: 0x000022E7 File Offset: 0x000004E7
			public TextureSettings.Builder SetNormalMapPreset()
			{
				return this.SetLinear(true);
			}

			// Token: 0x06000019 RID: 25 RVA: 0x000022F0 File Offset: 0x000004F0
			public TextureSettings.Builder SetLinear(bool linear)
			{
				this._linear = linear;
				return this;
			}

			// Token: 0x0600001A RID: 26 RVA: 0x000022FA File Offset: 0x000004FA
			public TextureSettings.Builder SetGenerateMipmap(bool generateMipmap)
			{
				this._generateMipmap = generateMipmap;
				return this;
			}

			// Token: 0x0600001B RID: 27 RVA: 0x00002304 File Offset: 0x00000504
			public TextureSettings.Builder SetMipmapCount(int mipmapCount)
			{
				this._mipmapCount = mipmapCount;
				return this;
			}

			// Token: 0x0600001C RID: 28 RVA: 0x0000230E File Offset: 0x0000050E
			public TextureSettings.Builder SetIgnoreMipmapLimits(bool ignoreMipmapLimits)
			{
				this._ignoreMipmapLimits = ignoreMipmapLimits;
				return this;
			}

			// Token: 0x0600001D RID: 29 RVA: 0x00002318 File Offset: 0x00000518
			public TextureSettings.Builder SetFilterMode(FilterMode filterMode)
			{
				this._filterMode = filterMode;
				return this;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x00002322 File Offset: 0x00000522
			public TextureSettings.Builder SetWrapMode(TextureWrapMode wrapMode)
			{
				this._wrapMode = wrapMode;
				return this;
			}

			// Token: 0x0600001F RID: 31 RVA: 0x0000232C File Offset: 0x0000052C
			public TextureSettings.Builder SetTextureFormat(TextureFormat textureFormat)
			{
				this._textureFormat = textureFormat;
				return this;
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00002336 File Offset: 0x00000536
			public TextureSettings.Builder SetAnisoLevel(int anisoLevel)
			{
				this._anisoLevel = anisoLevel;
				return this;
			}

			// Token: 0x06000021 RID: 33 RVA: 0x00002340 File Offset: 0x00000540
			public TextureSettings.Builder SetSize(int width, int height)
			{
				this._width = width;
				this._height = height;
				return this;
			}

			// Token: 0x06000022 RID: 34 RVA: 0x00002351 File Offset: 0x00000551
			public TextureSettings.Builder SetName(string name)
			{
				this._name = name;
				return this;
			}

			// Token: 0x06000023 RID: 35 RVA: 0x0000235C File Offset: 0x0000055C
			public TextureSettings Build()
			{
				return new TextureSettings(this._linear, this._generateMipmap, this._mipmapCount, this._ignoreMipmapLimits, this._filterMode, this._wrapMode, this._textureFormat, this._anisoLevel, this._width, this._height, this._name);
			}

			// Token: 0x04000011 RID: 17
			public bool _linear;

			// Token: 0x04000012 RID: 18
			public bool _generateMipmap = true;

			// Token: 0x04000013 RID: 19
			public int _mipmapCount = -1;

			// Token: 0x04000014 RID: 20
			public bool _ignoreMipmapLimits;

			// Token: 0x04000015 RID: 21
			public FilterMode _filterMode = 2;

			// Token: 0x04000016 RID: 22
			public TextureWrapMode _wrapMode;

			// Token: 0x04000017 RID: 23
			public TextureFormat _textureFormat = 4;

			// Token: 0x04000018 RID: 24
			public int _anisoLevel = 16;

			// Token: 0x04000019 RID: 25
			public int _width = 1;

			// Token: 0x0400001A RID: 26
			public int _height = 1;

			// Token: 0x0400001B RID: 27
			public string _name = "Texture";
		}
	}
}
