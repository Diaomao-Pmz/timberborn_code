using System;
using Bindito.Core;

namespace Timberborn.DuplicationSystem
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class DuplicationSystemConfigurator : Configurator
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public override void Configure()
		{
			base.Bind<DuplicationBlocker>().AsTransient();
			base.Bind<Duplicator>().AsSingleton();
		}
	}
}
