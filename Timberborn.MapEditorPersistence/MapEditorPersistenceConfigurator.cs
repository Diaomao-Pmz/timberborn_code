using System;
using Bindito.Core;

namespace Timberborn.MapEditorPersistence
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	internal class MapEditorPersistenceConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002153 File Offset: 0x00000353
		protected override void Configure()
		{
			base.Bind<MapEditorMapLoader>().AsSingleton();
		}
	}
}
