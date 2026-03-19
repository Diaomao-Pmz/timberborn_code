using System;
using Bindito.Core;

namespace Timberborn.BatchControl
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class BatchControlConfigurator : Configurator
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002C1C File Offset: 0x00000E1C
		public override void Configure()
		{
			base.Bind<IBatchControlBox>().To<BatchControlBox>().AsSingleton();
			base.Bind<BatchControlRowGroupFactory>().AsSingleton();
			base.Bind<BatchControlRowHighlighter>().AsSingleton();
			base.Bind<BatchControlBoxDistrictController>().AsSingleton();
			base.Bind<BatchControlBoxOpener>().AsSingleton();
			base.Bind<BatchControlBoxTabController>().AsSingleton();
			base.Bind<BatchControlDistrict>().AsSingleton();
			base.Bind<DistrictDropdownProvider>().AsSingleton();
			base.Bind<ToggleButtonBatchControlRowItemFactory>().AsSingleton();
		}
	}
}
