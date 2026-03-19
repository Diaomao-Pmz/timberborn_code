using System;
using Bindito.Core;

namespace Timberborn.GoodsUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class GoodsUIConfigurator : Configurator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000221A File Offset: 0x0000041A
		public override void Configure()
		{
			base.Bind<GoodDescriber>().AsSingleton();
			base.Bind<GoodItemFactory>().AsSingleton();
		}
	}
}
