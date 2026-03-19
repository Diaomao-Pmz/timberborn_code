using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Coordinates;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200000A RID: 10
	public class ConstructionFactory
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000025F0 File Offset: 0x000007F0
		public ConstructionFactory(BlockObjectFactory blockObjectFactory)
		{
			this._blockObjectFactory = blockObjectFactory;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025FF File Offset: 0x000007FF
		public ConstructionSite CreateAsUnfinished(BuildingSpec template, Placement placement)
		{
			return this._blockObjectFactory.CreateUnfinished(template.GetSpec<BlockObjectSpec>(), placement).GetComponent<ConstructionSite>();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002618 File Offset: 0x00000818
		public BaseComponent CreateAsFinished(BuildingSpec template, Placement placement)
		{
			ConstructionSite constructionSite = this.CreateAsUnfinished(template, placement);
			constructionSite.FinishNow();
			return constructionSite;
		}

		// Token: 0x0400001E RID: 30
		public readonly BlockObjectFactory _blockObjectFactory;
	}
}
