using System;
using System.Collections.Immutable;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Timberborn.GraphicsQualitySystem
{
	// Token: 0x0200000F RID: 15
	public class WaterQualitySetting : ILoadableSingleton
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600004E RID: 78 RVA: 0x00002F04 File Offset: 0x00001104
		// (remove) Token: 0x0600004F RID: 79 RVA: 0x00002F3C File Offset: 0x0000113C
		public event EventHandler WaterQualityChanged;

		// Token: 0x06000050 RID: 80 RVA: 0x00002F71 File Offset: 0x00001171
		public WaterQualitySetting(GraphicsQualitySettings graphicsQualitySettings)
		{
			this._graphicsQualitySettings = graphicsQualitySettings;
			this._urpAsset = (QualitySettings.renderPipeline as UniversalRenderPipelineAsset);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F90 File Offset: 0x00001190
		public static int GetValueForPreset(GraphicsQualityPreset preset)
		{
			int result;
			switch (preset)
			{
			case GraphicsQualityPreset.Ultra:
				result = 1;
				break;
			case GraphicsQualityPreset.High:
				result = 1;
				break;
			case GraphicsQualityPreset.Medium:
				result = 1;
				break;
			case GraphicsQualityPreset.Low:
				result = 0;
				break;
			default:
				throw new ArgumentException();
			}
			return result;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002FCC File Offset: 0x000011CC
		public bool HighQualityWaterEnabled
		{
			get
			{
				return this._graphicsQualitySettings.WaterQuality > 0;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FDC File Offset: 0x000011DC
		public void Load()
		{
			this._graphicsQualitySettings.WaterQualityChanged += delegate(object _, SettingChangedEventArgs<int> _)
			{
				this.UpdateQuality();
			};
			this.UpdateQuality();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002FFB File Offset: 0x000011FB
		public void UpdateQuality()
		{
			this._urpAsset.supportsCameraOpaqueTexture = this.HighQualityWaterEnabled;
			Shader.SetKeyword(ref WaterQualitySetting.HighQualityWaterEnabledKey, this.HighQualityWaterEnabled);
			EventHandler waterQualityChanged = this.WaterQualityChanged;
			if (waterQualityChanged == null)
			{
				return;
			}
			waterQualityChanged(this, EventArgs.Empty);
		}

		// Token: 0x04000034 RID: 52
		public static readonly ImmutableArray<int> ValidValues = new int[]
		{
			0,
			1
		}.ToImmutableArray<int>();

		// Token: 0x04000035 RID: 53
		public static readonly GlobalKeyword HighQualityWaterEnabledKey = GlobalKeyword.Create("_HIGH_QUALITY_WATER_ENABLED");

		// Token: 0x04000037 RID: 55
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000038 RID: 56
		public readonly UniversalRenderPipelineAsset _urpAsset;
	}
}
