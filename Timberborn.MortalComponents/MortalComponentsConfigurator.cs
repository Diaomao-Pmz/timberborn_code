using System;
using Bindito.Core;

namespace Timberborn.MortalComponents
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class MortalComponentsConfigurator : Configurator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002128 File Offset: 0x00000328
		public override void Configure()
		{
			base.Bind<DeadComponentDisabler>().AsSingleton();
		}
	}
}
