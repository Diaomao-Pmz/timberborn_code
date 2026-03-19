using System;
using Bindito.Core;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200001A RID: 26
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class BlueprintSystemConfigurator : Configurator
	{
		// Token: 0x0600009B RID: 155 RVA: 0x0000398C File Offset: 0x00001B8C
		public override void Configure()
		{
			base.Bind<BasicDeserializer>().AsSingleton();
			base.Bind<AdvancedDeserializer>().AsSingleton();
			base.Bind<AssetRefDeserializer>().AsSingleton();
			base.Bind<BlueprintDeserializer>().AsSingleton();
			base.Bind<BlueprintFileBundleLoader>().AsSingleton();
			base.Bind<BlueprintSourceService>().AsSingleton();
			base.Bind<ISpecService>().To<SpecService>().AsSingleton();
		}
	}
}
