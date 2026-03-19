using System;
using Bindito.Core;

namespace Timberborn.BuildingRange
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	[Context("MapEditor")]
	public class BuildingRangeConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<BuildingWithRoadSpillRange>().AsTransient();
			base.Bind<BuildingWithTerrainRange>().AsTransient();
		}
	}
}
