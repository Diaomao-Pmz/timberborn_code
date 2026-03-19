using System;
using System.Collections.Immutable;
using Timberborn.PlatformUtilities;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Timberborn.GraphicsQualitySystem
{
	// Token: 0x02000006 RID: 6
	public class AntiAliasingTypeSetting : ILoadableSingleton
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000211E File Offset: 0x0000031E
		public AntiAliasingTypeSetting(GraphicsQualitySettings graphicsQualitySettings)
		{
			this._graphicsQualitySettings = graphicsQualitySettings;
			this._urpAsset = (QualitySettings.renderPipeline as UniversalRenderPipelineAsset);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002140 File Offset: 0x00000340
		public static AntialiasingType GetValueForPreset(GraphicsQualityPreset preset)
		{
			AntialiasingType result;
			if (ApplicationPlatform.IsWindows())
			{
				switch (preset)
				{
				case GraphicsQualityPreset.Ultra:
					result = AntialiasingType.MSAAx8;
					break;
				case GraphicsQualityPreset.High:
					result = AntialiasingType.MSAAx4;
					break;
				case GraphicsQualityPreset.Medium:
					result = AntialiasingType.FXAA;
					break;
				case GraphicsQualityPreset.Low:
					result = AntialiasingType.Off;
					break;
				default:
					throw new ArgumentException();
				}
				return result;
			}
			switch (preset)
			{
			case GraphicsQualityPreset.Ultra:
				result = AntialiasingType.SMAA;
				break;
			case GraphicsQualityPreset.High:
				result = AntialiasingType.SMAA;
				break;
			case GraphicsQualityPreset.Medium:
				result = AntialiasingType.FXAA;
				break;
			case GraphicsQualityPreset.Low:
				result = AntialiasingType.Off;
				break;
			default:
				throw new ArgumentException();
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021B3 File Offset: 0x000003B3
		public void Load()
		{
			this._graphicsQualitySettings.AntiAliasingTypeChanged += delegate(object _, SettingChangedEventArgs<AntialiasingType> args)
			{
				this.Set(args.Value);
			};
			this.Set(this._graphicsQualitySettings.AntiAliasingType);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E0 File Offset: 0x000003E0
		public void Set(AntialiasingType antialiasingType)
		{
			switch (antialiasingType)
			{
			case AntialiasingType.Off:
			case AntialiasingType.FXAA:
			case AntialiasingType.SMAA:
				this._urpAsset.msaaSampleCount = 1;
				return;
			case AntialiasingType.MSAAx2:
				this._urpAsset.msaaSampleCount = 2;
				return;
			case AntialiasingType.MSAAx4:
				this._urpAsset.msaaSampleCount = 4;
				return;
			case AntialiasingType.MSAAx8:
				this._urpAsset.msaaSampleCount = 8;
				return;
			default:
				throw new ArgumentOutOfRangeException("antialiasingType", antialiasingType, null);
			}
		}

		// Token: 0x0400000F RID: 15
		public static readonly ImmutableArray<int> ValidValues = new int[]
		{
			0,
			1,
			2,
			3,
			4,
			5
		}.ToImmutableArray<int>();

		// Token: 0x04000010 RID: 16
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000011 RID: 17
		public readonly UniversalRenderPipelineAsset _urpAsset;
	}
}
