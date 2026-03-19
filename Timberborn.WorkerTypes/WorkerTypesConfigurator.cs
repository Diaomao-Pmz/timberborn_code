using System;
using Bindito.Core;

namespace Timberborn.WorkerTypes
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class WorkerTypesConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		public override void Configure()
		{
			base.Bind<WorkerTypeService>().AsSingleton();
		}
	}
}
