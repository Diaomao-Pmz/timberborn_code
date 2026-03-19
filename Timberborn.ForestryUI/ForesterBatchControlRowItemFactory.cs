using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Forestry;

namespace Timberborn.ForestryUI
{
	// Token: 0x02000007 RID: 7
	public class ForesterBatchControlRowItemFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FF File Offset: 0x000002FF
		public ForesterBatchControlRowItemFactory(ToggleButtonBatchControlRowItemFactory toggleButtonFactory)
		{
			this._toggleButtonFactory = toggleButtonFactory;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Forester component = entity.GetComponent<Forester>();
			if (!component)
			{
				return null;
			}
			return this.Create(component);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002138 File Offset: 0x00000338
		public IBatchControlRowItem Create(Forester forester)
		{
			return this._toggleButtonFactory.Create(ForesterBatchControlRowItemFactory.ButtonClass, delegate
			{
				forester.SetReplantDeadTrees(!forester.ReplantDeadTrees);
			}, () => forester.ReplantDeadTrees, ForesterBatchControlRowItemFactory.ReplantDeadTreesLocKey);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string ButtonClass = "forester-batch-control-row-item";

		// Token: 0x04000009 RID: 9
		public static readonly string ReplantDeadTreesLocKey = "Planting.ReplantDeadTrees";

		// Token: 0x0400000A RID: 10
		public readonly ToggleButtonBatchControlRowItemFactory _toggleButtonFactory;
	}
}
