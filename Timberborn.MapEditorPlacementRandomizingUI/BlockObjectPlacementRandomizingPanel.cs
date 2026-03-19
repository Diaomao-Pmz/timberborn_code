using System;
using Timberborn.BlockObjectTools;
using Timberborn.CoreUI;
using Timberborn.MapEditorPlacementRandomizing;
using Timberborn.SingletonSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorPlacementRandomizingUI
{
	// Token: 0x02000003 RID: 3
	internal class BlockObjectPlacementRandomizingPanel : IToolFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BlockObjectPlacementRandomizingPanel(EventBus eventBus, VisualElementLoader visualElementLoader, BlockObjectPlacementRandomizingService blockObjectPlacementRandomizingService)
		{
			this._eventBus = eventBus;
			this._visualElementLoader = visualElementLoader;
			this._blockObjectPlacementRandomizingService = blockObjectPlacementRandomizingService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
		public VisualElement InitializeFragment()
		{
			string elementName = "MapEditor/ToolPanel/BlockObjectPlacementRandomizingPanel";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._root.ToggleDisplayStyle(false);
			this._randomizeToggle = this._root.Q("RandomizeToggle", null);
			this._randomizeToggle.value = this._blockObjectPlacementRandomizingService.Randomize;
			this._randomizeToggle.RegisterValueChangedCallback(delegate(ChangeEvent<bool> evt)
			{
				this._blockObjectPlacementRandomizingService.Randomize = evt.newValue;
			});
			this._eventBus.Register(this);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002164 File Offset: 0x00000364
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			BlockObjectTool blockObjectTool = toolEnteredEvent.Tool as BlockObjectTool;
			if (blockObjectTool != null && blockObjectTool.Template.HasSpec<BlockObjectRandomizablePlacementSpec>())
			{
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002199 File Offset: 0x00000399
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000001 RID: 1
		private readonly EventBus _eventBus;

		// Token: 0x04000002 RID: 2
		private readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000003 RID: 3
		private readonly BlockObjectPlacementRandomizingService _blockObjectPlacementRandomizingService;

		// Token: 0x04000004 RID: 4
		private VisualElement _root;

		// Token: 0x04000005 RID: 5
		private Toggle _randomizeToggle;
	}
}
