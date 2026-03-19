using System;
using Bindito.Core;

namespace Timberborn.GoodCollectionSystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class GoodCollectionSystemConfigurator : Configurator
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002389 File Offset: 0x00000589
		public override void Configure()
		{
			base.Bind<CommonGoodCollectionIdsProvider>().AsSingleton();
			base.MultiBind<IGoodCollectionIdsProvider>().ToExisting<CommonGoodCollectionIdsProvider>();
		}
	}
}
