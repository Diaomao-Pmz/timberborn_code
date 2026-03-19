using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000019 RID: 25
	public class PopulationDistributorRetriever
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003474 File Offset: 0x00001674
		public PopulationDistributor GetPopulationDistributor<T>(BaseComponent component) where T : BaseComponent, IDistributorTemplate
		{
			string componentName = component.GetComponent<T>().ComponentName;
			return component.GetNamedComponent(componentName);
		}
	}
}
