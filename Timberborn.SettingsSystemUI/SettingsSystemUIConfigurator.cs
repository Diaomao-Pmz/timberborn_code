using System;
using Bindito.Core;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x0200001A RID: 26
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class SettingsSystemUIConfigurator : Configurator
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003D04 File Offset: 0x00001F04
		public override void Configure()
		{
			base.Bind<AnalyticsSettingsController>().AsSingleton();
			base.Bind<AnisotropicFilteringDropdownProvider>().AsSingleton();
			base.Bind<AntiAliasingDropdownProvider>().AsSingleton();
			base.Bind<DevModeSettingsController>().AsSingleton();
			base.Bind<FrameRateLimitDropdownProvider>().AsSingleton();
			base.Bind<GameSavingSettings>().AsSingleton();
			base.Bind<GameSavingSettingsController>().AsSingleton();
			base.Bind<GraphicsSettingsController>().AsSingleton();
			base.Bind<GraphicsQualityDropdownProvider>().AsSingleton();
			base.Bind<ISettingsController>().To<SettingsBox>().AsSingleton();
			base.Bind<InputSettingsController>().AsSingleton();
			base.Bind<LanguageSettingsController>().AsSingleton();
			base.Bind<LightQualityDropdownProvider>().AsSingleton();
			base.Bind<OnScreenKeyboardDropdownProvider>().AsSingleton();
			base.Bind<ScreenResolutionDropdownProvider>().AsSingleton();
			base.Bind<ScreenSettingsController>().AsSingleton();
			base.Bind<ShadowQualityGraphicsDropdownProvider>().AsSingleton();
			base.Bind<SoundSettingsController>().AsSingleton();
			base.Bind<TextureQualityDropdownProvider>().AsSingleton();
			base.Bind<TutorialSettingsController>().AsSingleton();
			base.Bind<UISettingsController>().AsSingleton();
			base.Bind<VSyncDropdownProvider>().AsSingleton();
			base.Bind<AccessibilitySettingsController>().AsSingleton();
			base.Bind<WaterQualityDropdownProvider>().AsSingleton();
			base.Bind<BloomDropdownProvider>().AsSingleton();
			base.Bind<CameraSettingsController>().AsSingleton();
		}
	}
}
