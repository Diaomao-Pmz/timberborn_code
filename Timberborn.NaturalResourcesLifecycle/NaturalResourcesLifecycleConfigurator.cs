using System;
using Bindito.Core;

namespace Timberborn.NaturalResourcesLifecycle
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	[Context("MapEditor")]
	public class NaturalResourcesLifecycleConfigurator : Configurator
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000025E3 File Offset: 0x000007E3
		public override void Configure()
		{
			base.Bind<DyingNaturalResource>().AsTransient();
			base.Bind<LivingNaturalResource>().AsTransient();
		}
	}
}
