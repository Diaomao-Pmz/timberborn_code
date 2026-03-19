using System;
using Bindito.Core;
using Timberborn.ToolSystem;

namespace Timberborn.BuildingAvailability
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class BuildingAvailabilityConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<BuildingAvailabilityValidator>().AsSingleton();
			base.MultiBind<IToolDisabler>().To<BuildingAvailabilityToolDisabler>().AsSingleton();
		}
	}
}
