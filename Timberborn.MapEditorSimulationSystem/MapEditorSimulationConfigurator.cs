using System;
using Bindito.Core;

namespace Timberborn.MapEditorSimulationSystem
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	internal class MapEditorSimulationConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002111 File Offset: 0x00000311
		protected override void Configure()
		{
			base.Bind<MapEditorSimulation>().AsSingleton();
		}
	}
}
