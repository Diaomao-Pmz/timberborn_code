using System;
using System.Collections.Immutable;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Timberborn.GraphicsQualitySystem
{
	// Token: 0x0200000C RID: 12
	public class ShadowQualityGraphicsSettings : ILoadableSingleton
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002D29 File Offset: 0x00000F29
		public ShadowQualityGraphicsSettings(GraphicsQualitySettings graphicsQualitySettings)
		{
			this._graphicsQualitySettings = graphicsQualitySettings;
			this._urpAsset = (QualitySettings.renderPipeline as UniversalRenderPipelineAsset);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D48 File Offset: 0x00000F48
		public static int GetValueForPreset(GraphicsQualityPreset preset)
		{
			int result;
			switch (preset)
			{
			case GraphicsQualityPreset.Ultra:
				result = 4;
				break;
			case GraphicsQualityPreset.High:
				result = 3;
				break;
			case GraphicsQualityPreset.Medium:
				result = 2;
				break;
			case GraphicsQualityPreset.Low:
				result = 0;
				break;
			default:
				throw new ArgumentException();
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D84 File Offset: 0x00000F84
		public void Load()
		{
			this._graphicsQualitySettings.ShadowQualityChanged += delegate(object _, SettingChangedEventArgs<int> args)
			{
				this.Set(args.Value);
			};
			this.Set(this._graphicsQualitySettings.ShadowQuality);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public void Set(int value)
		{
			UniversalRenderPipelineAsset urpAsset = this._urpAsset;
			int mainLightShadowmapResolution;
			switch (value)
			{
			case 0:
				mainLightShadowmapResolution = 1024;
				break;
			case 1:
				mainLightShadowmapResolution = 1024;
				break;
			case 2:
				mainLightShadowmapResolution = 2048;
				break;
			case 3:
				mainLightShadowmapResolution = 4096;
				break;
			case 4:
				mainLightShadowmapResolution = 8192;
				break;
			default:
				mainLightShadowmapResolution = 4096;
				break;
			}
			urpAsset.mainLightShadowmapResolution = mainLightShadowmapResolution;
		}

		// Token: 0x0400002D RID: 45
		public static readonly ImmutableArray<int> ValidValues = new int[]
		{
			0,
			1,
			2,
			3,
			4
		}.ToImmutableArray<int>();

		// Token: 0x0400002E RID: 46
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x0400002F RID: 47
		public readonly UniversalRenderPipelineAsset _urpAsset;
	}
}
