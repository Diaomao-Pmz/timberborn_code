using System;
using Bindito.Core;

namespace Timberborn.Goods
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	[Context("MapEditor")]
	public class GoodsConfigurator : Configurator
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002B0C File Offset: 0x00000D0C
		public override void Configure()
		{
			base.Bind<GoodsGroupSpecService>().AsSingleton();
			base.Bind<GoodIconVisualizer>().AsSingleton();
			base.Bind<GoodAmountSerializer>().AsSingleton();
			base.Bind<GoodRegistryValueSerializer>().AsSingleton();
			base.Bind<SerializedGoodValueSerializer>().AsSingleton();
			base.Bind<IGoodService>().To<GoodService>().AsSingleton();
		}
	}
}
