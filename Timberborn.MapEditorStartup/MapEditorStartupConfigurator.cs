using System;
using Bindito.Core;

namespace Timberborn.MapEditorStartup
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	internal class MapEditorStartupConfigurator : Configurator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020EB File Offset: 0x000002EB
		protected override void Configure()
		{
			base.Bind<MapEditorInitializer>().AsSingleton();
		}
	}
}
