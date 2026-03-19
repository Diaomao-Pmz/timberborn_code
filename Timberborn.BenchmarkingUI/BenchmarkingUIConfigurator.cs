using System;
using Bindito.Core;

namespace Timberborn.BenchmarkingUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class BenchmarkingUIConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000022E8 File Offset: 0x000004E8
		public override void Configure()
		{
			base.Bind<BenchmarkDebuggingPanel>().AsSingleton();
		}
	}
}
