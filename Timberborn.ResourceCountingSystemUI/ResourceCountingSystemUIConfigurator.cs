using System;
using Bindito.Core;

namespace Timberborn.ResourceCountingSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class ResourceCountingSystemUIConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002136 File Offset: 0x00000336
		public override void Configure()
		{
			base.Bind<ContextualResourceCountingService>().AsSingleton();
		}
	}
}
