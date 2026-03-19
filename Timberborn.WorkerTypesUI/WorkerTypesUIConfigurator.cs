using System;
using Bindito.Core;

namespace Timberborn.WorkerTypesUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class WorkerTypesUIConfigurator : Configurator
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002144 File Offset: 0x00000344
		public override void Configure()
		{
			base.Bind<WorkerTypeHelper>().AsSingleton();
		}
	}
}
