using System;
using Bindito.Core;

namespace Timberborn.MapEditorSimulationSystemUI
{
	// Token: 0x02000006 RID: 6
	[Context("MapEditor")]
	public class MapEditorSimulationUIConfigurator : Configurator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000022D3 File Offset: 0x000004D3
		public override void Configure()
		{
			base.Bind<MapEditorSimulationPanel>().AsSingleton();
		}
	}
}
