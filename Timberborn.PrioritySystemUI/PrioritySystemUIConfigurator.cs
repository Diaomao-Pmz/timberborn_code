using System;
using Bindito.Core;

namespace Timberborn.PrioritySystemUI
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class PrioritySystemUIConfigurator : Configurator
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000027A8 File Offset: 0x000009A8
		public override void Configure()
		{
			base.Bind<PriorityToggleFactory>().AsSingleton();
			base.Bind<PriorityToggleGroupFactory>().AsSingleton();
			base.Bind<PriorityColors>().AsSingleton();
		}
	}
}
