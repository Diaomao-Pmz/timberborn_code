using System;
using Bindito.Core;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x0200000C RID: 12
	[Context("Bootstrapper")]
	public class ModdingAssetsConfigurator : Configurator
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002C08 File Offset: 0x00000E08
		public override void Configure()
		{
			base.Bind<ModAssetBundleLoader>().AsSingleton();
			base.Bind<ModTextureSettingLoader>().AsSingleton();
			base.Bind<IModFileConverter<Sprite>>().To<ModSpriteConverter>().AsSingleton();
			base.Bind<IModFileConverter<Texture2D>>().To<ModTextureConverter>().AsSingleton();
			base.Bind<IModFileConverter<TextAsset>>().To<ModTextAssetConverter>().AsSingleton();
			base.Bind<IModFileConverter<BinaryData>>().To<ModTimbermeshConverter>().AsSingleton();
			base.Bind<IModFileConverter<BlueprintAsset>>().To<ModBlueprintConverter>().AsSingleton();
			base.MultiBind<IAssetProvider>().To<ModSystemFileProvider<Sprite>>().AsSingleton();
			base.MultiBind<IAssetProvider>().To<ModSystemFileProvider<Texture2D>>().AsSingleton();
			base.MultiBind<IAssetProvider>().To<ModSystemFileProvider<TextAsset>>().AsSingleton();
			base.MultiBind<IAssetProvider>().To<ModSystemFileProvider<BinaryData>>().AsSingleton();
			base.MultiBind<IAssetProvider>().To<ModSystemFileProvider<BlueprintAsset>>().AsSingleton();
			base.MultiBind<IAssetProvider>().To<ModAssetBundleProvider>().AsSingleton();
		}
	}
}
