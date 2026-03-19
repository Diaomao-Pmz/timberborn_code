using System;
using Bindito.Core;

namespace Timberborn.MapIndexSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	[Context("MapEditor")]
	public class MapIndexSystemConfigurator : Configurator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000289C File Offset: 0x00000A9C
		public override void Configure()
		{
			base.Bind<MapIndexService>().AsSingleton();
			base.Bind<FloatPackedListSerializer>().AsSingleton();
			base.Bind<IntPackedListSerializer>().AsSingleton();
			base.Bind<BoolPackedListSerializer>().AsSingleton();
		}
	}
}
