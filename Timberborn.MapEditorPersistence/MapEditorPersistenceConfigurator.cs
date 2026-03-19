using System;
using Bindito.Core;

namespace Timberborn.MapEditorPersistence
{
	// Token: 0x02000005 RID: 5
	[Context("MapEditor")]
	public class MapEditorPersistenceConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002153 File Offset: 0x00000353
		public override void Configure()
		{
			base.Bind<MapEditorMapLoader>().AsSingleton();
		}
	}
}
