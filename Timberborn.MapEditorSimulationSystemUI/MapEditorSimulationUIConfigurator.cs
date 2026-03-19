using System;
using Bindito.Core;

namespace Timberborn.MapEditorSimulationSystemUI
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	internal class MapEditorSimulationUIConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000022B5 File Offset: 0x000004B5
		protected override void Configure()
		{
			base.Bind<MapEditorSimulationPanel>().AsSingleton();
		}
	}
}
