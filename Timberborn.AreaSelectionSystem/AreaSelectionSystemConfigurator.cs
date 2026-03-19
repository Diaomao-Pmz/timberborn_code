using System;
using Bindito.Core;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x02000020 RID: 32
	[Context("Game")]
	[Context("MapEditor")]
	public class AreaSelectionSystemConfigurator : Configurator
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000036F8 File Offset: 0x000018F8
		public override void Configure()
		{
			base.Bind<AreaBoundsDrawingBlocker>().AsTransient();
			base.Bind<AreaBlockObjectPickerFactory>().AsSingleton();
			base.Bind<RectangleBoundsDrawerFactory>().AsSingleton();
			base.Bind<AreaSelector>().AsSingleton();
			base.Bind<AreaBlockObjectAndTerrainPicker>().AsSingleton();
			base.Bind<AreaPicker>().AsSingleton();
			base.Bind<SculptingTerrainPicker>().AsSingleton();
			base.Bind<AreaSelectionController>().AsSingleton();
		}
	}
}
