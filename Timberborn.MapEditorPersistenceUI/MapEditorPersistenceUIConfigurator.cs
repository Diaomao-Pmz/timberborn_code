using System;
using Bindito.Core;

namespace Timberborn.MapEditorPersistenceUI
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	public class MapEditorPersistenceUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<MapPersistenceController>().AsSingleton();
			base.Bind<MapSaverLoader>().AsSingleton();
			base.Bind<SaveMapBox>().AsSingleton();
		}
	}
}
