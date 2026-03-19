using System;
using Bindito.Core;

namespace Timberborn.MapMetadataSystem
{
	// Token: 0x02000006 RID: 6
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class MapMetadataSystemConfigurator : Configurator
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000022F5 File Offset: 0x000004F5
		public override void Configure()
		{
			base.Bind<MapMetadataSerializer>().AsSingleton();
		}
	}
}
