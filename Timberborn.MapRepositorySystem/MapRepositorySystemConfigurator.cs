using System;
using Bindito.Core;

namespace Timberborn.MapRepositorySystem
{
	// Token: 0x02000009 RID: 9
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class MapRepositorySystemConfigurator : Configurator
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000268D File Offset: 0x0000088D
		public override void Configure()
		{
			base.Bind<MapRepository>().AsSingleton();
			base.Bind<MapDeserializer>().AsSingleton();
		}
	}
}
