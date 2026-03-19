using System;
using Bindito.Core;

namespace Timberborn.RecoverableGoodSystemUI
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class RecoverableGoodSystemUIConfigurator : Configurator
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000266A File Offset: 0x0000086A
		public override void Configure()
		{
			base.Bind<RecoverableGoodDialogBoxShower>().AsSingleton();
			base.Bind<RecoverableGoodElementFactory>().AsSingleton();
			base.Bind<RecoverableGoodItemFactory>().AsSingleton();
			base.Bind<RecoverableGoodTooltip>().AsSingleton();
		}
	}
}
