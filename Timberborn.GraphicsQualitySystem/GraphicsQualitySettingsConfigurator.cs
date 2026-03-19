using System;
using Bindito.Core;

namespace Timberborn.GraphicsQualitySystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MainMenu")]
	[Context("MapEditor")]
	public class GraphicsQualitySettingsConfigurator : Configurator
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public override void Configure()
		{
			base.Bind<AnisotropicFilteringSetting>().AsSingleton();
			base.Bind<AntiAliasingTypeSetting>().AsSingleton();
			base.Bind<GraphicsQualitySettings>().AsSingleton().AsExported();
			base.Bind<LightQualitySetting>().AsSingleton();
			base.Bind<ShadowQualityGraphicsSettings>().AsSingleton();
			base.Bind<TextureQualitySetting>().AsSingleton();
			base.Bind<WaterQualitySetting>().AsSingleton();
			base.Bind<BloomSetting>().AsSingleton();
		}
	}
}
