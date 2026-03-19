using System;
using Bindito.Core;

namespace Timberborn.Multithreading
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	[Context("MapEditor")]
	public class MultithreadingConfigurator : Configurator
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000234C File Offset: 0x0000054C
		public override void Configure()
		{
			base.Bind<Parallelizer>().AsSingleton();
			base.Bind<IParallelizer>().ToExisting<Parallelizer>();
			base.Bind<MainThreadPrioritySetter>().AsSingleton();
			base.Bind<ScheduledTaskPool>().AsSingleton();
		}
	}
}
