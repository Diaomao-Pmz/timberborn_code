using System;
using Bindito.Core;

namespace Timberborn.SelectionToolSystem
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class SelectionToolSystemConfigurator : Configurator
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021CD File Offset: 0x000003CD
		public override void Configure()
		{
			base.Bind<SelectionToolProcessorFactory>().AsSingleton();
		}
	}
}
