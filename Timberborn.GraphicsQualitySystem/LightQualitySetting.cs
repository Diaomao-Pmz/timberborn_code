using System;
using System.Collections.Immutable;
using System.Reflection;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Timberborn.GraphicsQualitySystem
{
	// Token: 0x0200000B RID: 11
	public class LightQualitySetting : ILoadableSingleton
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002C1D File Offset: 0x00000E1D
		public LightQualitySetting(GraphicsQualitySettings graphicsQualitySettings)
		{
			this._graphicsQualitySettings = graphicsQualitySettings;
			this._urpAsset = (QualitySettings.renderPipeline as UniversalRenderPipelineAsset);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C3C File Offset: 0x00000E3C
		public static int GetValueForPreset(GraphicsQualityPreset preset)
		{
			int result;
			switch (preset)
			{
			case GraphicsQualityPreset.Ultra:
				result = 8;
				break;
			case GraphicsQualityPreset.High:
				result = 6;
				break;
			case GraphicsQualityPreset.Medium:
				result = 4;
				break;
			case GraphicsQualityPreset.Low:
				result = 0;
				break;
			default:
				throw new ArgumentException();
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C78 File Offset: 0x00000E78
		public void Load()
		{
			this._graphicsQualitySettings.LightQualityChanged += delegate(object _, SettingChangedEventArgs<int> args)
			{
				this.Set(args.Value);
			};
			this.Set(this._graphicsQualitySettings.LightQuality);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public static void SetAdditionalLightRenderingMode(LightRenderingMode lightRenderingMode, UniversalRenderPipelineAsset urpAsset)
		{
			FieldInfo field = typeof(UniversalRenderPipelineAsset).GetField("m_AdditionalLightsRenderingMode", BindingFlags.Instance | BindingFlags.NonPublic);
			if (field != null)
			{
				field.SetValue(urpAsset, lightRenderingMode);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002CDE File Offset: 0x00000EDE
		public void Set(int value)
		{
			this._urpAsset.maxAdditionalLightsCount = value;
			LightQualitySetting.SetAdditionalLightRenderingMode((value == 0) ? 0 : 1, this._urpAsset);
		}

		// Token: 0x0400002A RID: 42
		public static readonly ImmutableArray<int> ValidValues = new int[]
		{
			0,
			4,
			6,
			8
		}.ToImmutableArray<int>();

		// Token: 0x0400002B RID: 43
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x0400002C RID: 44
		public readonly UniversalRenderPipelineAsset _urpAsset;
	}
}
