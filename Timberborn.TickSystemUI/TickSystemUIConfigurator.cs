using System;
using Bindito.Core;

namespace Timberborn.TickSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class TickSystemUIConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002171 File Offset: 0x00000371
		public override void Configure()
		{
			base.Bind<ParallelSingletonDebuggingPanel>().AsSingleton();
		}
	}
}
