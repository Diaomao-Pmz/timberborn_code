using System;
using Bindito.Core;
using Timberborn.BlueprintSystem;

namespace Timberborn.SpriteOperations
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	[Context("MapEditor")]
	public class SpriteOperationsConfigurator : Configurator
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002450 File Offset: 0x00000650
		public override void Configure()
		{
			base.Bind<SpriteResizer>().AsSingleton();
			base.Bind<SpriteFlipper>().AsSingleton();
			base.MultiBind<IDeserializer>().To<UISpriteDeserializer>().AsSingleton();
			base.MultiBind<IDeserializer>().To<FlippedSpriteDeserializer>().AsSingleton();
		}
	}
}
