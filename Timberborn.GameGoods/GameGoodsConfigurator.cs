using System;
using Bindito.Core;
using Timberborn.Goods;

namespace Timberborn.GameGoods
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class GameGoodsConfigurator : Configurator
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000224A File Offset: 0x0000044A
		public override void Configure()
		{
			base.Bind<IGoodFilter>().To<GameGoodFilter>().AsSingleton();
		}
	}
}
