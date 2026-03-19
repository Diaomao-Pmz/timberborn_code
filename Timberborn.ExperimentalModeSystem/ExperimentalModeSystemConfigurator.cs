using System;
using Bindito.Core;

namespace Timberborn.ExperimentalModeSystem
{
	// Token: 0x02000005 RID: 5
	[Context("Bootstrapper")]
	public class ExperimentalModeSystemConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000210D File Offset: 0x0000030D
		public override void Configure()
		{
			base.Bind<ExperimentalMode>().AsSingleton().AsExported();
		}
	}
}
