using System;
using Bindito.Core;

namespace Timberborn.LifeSystem
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class LifeSystemConfigurator : Configurator
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000024C9 File Offset: 0x000006C9
		public override void Configure()
		{
			base.Bind<LifeProgressor>().AsTransient();
			base.Bind<LifeService>().AsSingleton();
		}
	}
}
