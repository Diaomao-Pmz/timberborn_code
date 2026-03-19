using System;
using Timberborn.TemplateInstantiation;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x0200000B RID: 11
	public class DistributorTemplateInitializer : IDedicatedDecoratorInitializer<IDistributorTemplate, PopulationDistributor>
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002445 File Offset: 0x00000645
		public void Initialize(IDistributorTemplate subject, PopulationDistributor decorator)
		{
			decorator.Initialize(subject);
		}
	}
}
