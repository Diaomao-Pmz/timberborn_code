using System;
using Bindito.Core;
using Timberborn.TickSystem;

namespace Timberborn.MapEditorTickSystem
{
	// Token: 0x02000006 RID: 6
	[Context("MapEditor")]
	public class MapEditorTickSystemConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020EC File Offset: 0x000002EC
		public override void Configure()
		{
			base.Bind<ITickingMode>().To<MapEditorTickingMode>().AsSingleton();
		}
	}
}
