using System;
using Bindito.Core;
using Timberborn.TickSystem;

namespace Timberborn.MapEditorTickSystem
{
	// Token: 0x02000005 RID: 5
	[Context("MapEditor")]
	internal class MapEditorTickSystemConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
		protected override void Configure()
		{
			base.Bind<ITickingMode>().To<MapEditorTickingMode>().AsSingleton();
		}
	}
}
