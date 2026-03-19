using System;
using Bindito.Core;

namespace Timberborn.YielderFinding
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class YielderFindingConfigurator : Configurator
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002434 File Offset: 0x00000634
		public override void Configure()
		{
			base.Bind<YieldStatus>().AsTransient();
			base.Bind<YielderFinder>().AsSingleton();
			base.Bind<ClosestYielderFinder>().AsSingleton();
		}
	}
}
