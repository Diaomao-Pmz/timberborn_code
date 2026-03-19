using System;
using Bindito.Core;

namespace Timberborn.InventorySystemBatchControl
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class InventorySystemBatchControlConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002527 File Offset: 0x00000727
		public override void Configure()
		{
			base.Bind<InventoryCapacityBatchControlRowItemFactory>().AsSingleton();
		}
	}
}
