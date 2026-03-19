using System;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.GraphicsQualitySystem;
using Timberborn.RootProviders;
using Timberborn.ScreenSystem;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Timberborn.Rendering
{
	// Token: 0x0200001F RID: 31
	public class PostprocessingService : ILoadableSingleton, IPostLoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000042A5 File Offset: 0x000024A5
		public PostprocessingService(IAssetLoader assetLoader, IInstantiator instantiator, RootObjectProvider rootObjectProvider, GraphicsQualitySettings graphicsQualitySettings, ScreenSettings screenSettings)
		{
			this._assetLoader = assetLoader;
			this._instantiator = instantiator;
			this._rootObjectProvider = rootObjectProvider;
			this._graphicsQualitySettings = graphicsQualitySettings;
			this._screenSettings = screenSettings;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000042D4 File Offset: 0x000024D4
		public void Load()
		{
			GameObject gameObject = this._assetLoader.Load<GameObject>(PostprocessingService.VolumePrefabPath);
			GameObject gameObject2 = this._rootObjectProvider.CreateRootObject("PostprocessingService");
			Volume component = this._instantiator.Instantiate(gameObject, gameObject2.transform).GetComponent<Volume>();
			component.profile.TryGet<Bloom>(ref this._bloom);
			component.profile.TryGet<ColorAdjustments>(ref this._colorAdjustments);
			this._initialPostExposure = this._colorAdjustments.postExposure.value;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004353 File Offset: 0x00002553
		public void PostLoad()
		{
			this._graphicsQualitySettings.BloomChanged += delegate(object _, SettingChangedEventArgs<bool> _)
			{
				this.UpdateBloom();
			};
			this._screenSettings.BrightnessChanged += this.OnBrightnessChanged;
			this.UpdateBloom();
			this.UpdateBrightness();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000438F File Offset: 0x0000258F
		public void Unload()
		{
			this._screenSettings.BrightnessChanged -= this.OnBrightnessChanged;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000043A8 File Offset: 0x000025A8
		public void UpdateBloom()
		{
			this._bloom.active = this._graphicsQualitySettings.BloomEnabled;
			Shader.SetKeyword(ref PostprocessingService.BloomPropertyId, this._bloom.active);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000043D5 File Offset: 0x000025D5
		public void OnBrightnessChanged(object sender, SettingChangedEventArgs<float> e)
		{
			this.UpdateBrightness();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000043E0 File Offset: 0x000025E0
		public void UpdateBrightness()
		{
			float num = (this._screenSettings.Brightness - 1f) * 6f;
			this._colorAdjustments.postExposure.value = this._initialPostExposure + num;
			this._colorAdjustments.postExposure.overrideState = true;
		}

		// Token: 0x04000058 RID: 88
		public static readonly GlobalKeyword BloomPropertyId = GlobalKeyword.Create("_BLOOM_ENABLED");

		// Token: 0x04000059 RID: 89
		public static readonly string VolumePrefabPath = "Rendering/Volume";

		// Token: 0x0400005A RID: 90
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400005B RID: 91
		public readonly IInstantiator _instantiator;

		// Token: 0x0400005C RID: 92
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400005D RID: 93
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x0400005E RID: 94
		public readonly ScreenSettings _screenSettings;

		// Token: 0x0400005F RID: 95
		public Bloom _bloom;

		// Token: 0x04000060 RID: 96
		public ColorAdjustments _colorAdjustments;

		// Token: 0x04000061 RID: 97
		public float _initialPostExposure;
	}
}
