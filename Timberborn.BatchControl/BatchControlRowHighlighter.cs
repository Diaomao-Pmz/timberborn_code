using System;
using Timberborn.EntitySystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.BatchControl
{
	// Token: 0x02000018 RID: 24
	public class BatchControlRowHighlighter : ILoadableSingleton
	{
		// Token: 0x06000077 RID: 119 RVA: 0x0000352E File Offset: 0x0000172E
		public BatchControlRowHighlighter(EventBus eventBus, BatchControlBoxTabController batchControlBoxTabController, EntitySelectionService entitySelectionService)
		{
			this._eventBus = eventBus;
			this._batchControlBoxTabController = batchControlBoxTabController;
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000354B File Offset: 0x0000174B
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003559 File Offset: 0x00001759
		[OnEvent]
		public void OnBatchControlTabShown(BatchControlTabShownEvent batchControlTabShownEvent)
		{
			if (this._entitySelectionService.IsAnythingSelected)
			{
				BatchControlRowHighlighter.SetEntityRowsHighlight(batchControlTabShownEvent.BatchControlTab, this._entitySelectionService.SelectedObject.GetComponent<EntityComponent>(), true);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003584 File Offset: 0x00001784
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			EntityComponent component = selectableObjectSelectedEvent.SelectableObject.GetComponent<EntityComponent>();
			foreach (BatchControlTab batchControlTab in this._batchControlBoxTabController.Tabs)
			{
				BatchControlRowHighlighter.SetEntityRowsHighlight(batchControlTab, component, true);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000035E4 File Offset: 0x000017E4
		[OnEvent]
		public void OnSelectableObjectUnselected(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			SelectableObject selectableObject = selectableObjectUnselectedEvent.SelectableObject;
			if (selectableObject)
			{
				foreach (BatchControlTab batchControlTab in this._batchControlBoxTabController.Tabs)
				{
					BatchControlRowHighlighter.SetEntityRowsHighlight(batchControlTab, selectableObject.GetComponent<EntityComponent>(), false);
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000364C File Offset: 0x0000184C
		public static void SetEntityRowsHighlight(BatchControlTab batchControlTab, EntityComponent entity, bool isHighlighted)
		{
			foreach (BatchControlRow batchControlRow in batchControlTab.GetEntityRows(entity))
			{
				batchControlRow.Root.EnableInClassList(BatchControlRowHighlighter.HighlightedClass, isHighlighted);
			}
		}

		// Token: 0x04000051 RID: 81
		public static readonly string HighlightedClass = "batch-control-box__row--highlighted";

		// Token: 0x04000052 RID: 82
		public readonly EventBus _eventBus;

		// Token: 0x04000053 RID: 83
		public readonly BatchControlBoxTabController _batchControlBoxTabController;

		// Token: 0x04000054 RID: 84
		public readonly EntitySelectionService _entitySelectionService;
	}
}
