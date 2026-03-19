using System;
using Bindito.Core;

namespace Timberborn.NeedCollectionSystem
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class NeedCollectionSystemConfigurator : Configurator
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002389 File Offset: 0x00000589
		public override void Configure()
		{
			base.Bind<CommonNeedCollectionIdsProvider>().AsSingleton();
			base.MultiBind<INeedCollectionIdsProvider>().ToExisting<CommonNeedCollectionIdsProvider>();
		}
	}
}
