using System;
using Bindito.Core;
using Timberborn.BlueprintSystem;

namespace Timberborn.LocalizationSerialization
{
	// Token: 0x02000007 RID: 7
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class LocalizationSerializationConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public override void Configure()
		{
			base.MultiBind<IDeserializer>().To<LocalizedTextDeserializer>().AsSingleton();
		}
	}
}
