using System;
using Bindito.Core;

namespace Timberborn.InventoryNeedSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class InventoryNeedSystemConfigurator : Configurator
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002738 File Offset: 0x00000938
		public override void Configure()
		{
			base.Bind<InventoryNeedBehavior>().AsTransient();
			base.Bind<InventoryGoodConsumptionBlocker>().AsTransient();
			base.Bind<InventoryNeedBehaviorInitializer>().AsSingleton();
		}
	}
}
