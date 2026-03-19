using System;
using System.Collections.Immutable;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.GraphicsQualitySystem
{
	// Token: 0x0200000D RID: 13
	public class TextureQualitySetting : ILoadableSingleton
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002E40 File Offset: 0x00001040
		public TextureQualitySetting(GraphicsQualitySettings graphicsQualitySettings)
		{
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E50 File Offset: 0x00001050
		public static int GetValueForPreset(GraphicsQualityPreset preset)
		{
			int result;
			switch (preset)
			{
			case GraphicsQualityPreset.Ultra:
				result = 0;
				break;
			case GraphicsQualityPreset.High:
				result = 0;
				break;
			case GraphicsQualityPreset.Medium:
				result = 1;
				break;
			case GraphicsQualityPreset.Low:
				result = 2;
				break;
			default:
				throw new ArgumentException();
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E8C File Offset: 0x0000108C
		public void Load()
		{
			this._graphicsQualitySettings.TextureQualityChanged += delegate(object _, SettingChangedEventArgs<int> args)
			{
				TextureQualitySetting.Set(args.Value);
			};
			TextureQualitySetting.Set(this._graphicsQualitySettings.TextureQuality);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002EC8 File Offset: 0x000010C8
		public static void Set(int value)
		{
			QualitySettings.globalTextureMipmapLimit = value;
		}

		// Token: 0x04000030 RID: 48
		public static readonly ImmutableArray<int> ValidValues = new int[]
		{
			0,
			1,
			2
		}.ToImmutableArray<int>();

		// Token: 0x04000031 RID: 49
		public readonly GraphicsQualitySettings _graphicsQualitySettings;
	}
}
