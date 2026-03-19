using System;
using Bindito.Core;

namespace Timberborn.BuilderPrioritySystem
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class BuilderPrioritySystemConfigurator : Configurator
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000235F File Offset: 0x0000055F
		public override void Configure()
		{
			base.Bind<BuilderPrioritizable>().AsTransient();
		}
	}
}
