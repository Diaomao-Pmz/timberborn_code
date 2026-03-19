using System;
using Timberborn.BlockObjectTools;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.BlockObjectToolsUI
{
	// Token: 0x02000009 RID: 9
	public class BlockObjectToolWarningPanel : IToolFragment, IUpdatableSingleton
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000264C File Offset: 0x0000084C
		public BlockObjectToolWarningPanel(ToolService toolService, VisualElementLoader visualElementLoader)
		{
			this._toolService = toolService;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002664 File Offset: 0x00000864
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/ToolPanel/BlockObjectToolWarningPanel");
			this._text = UQueryExtensions.Q<Label>(this._root, "Warning", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026B0 File Offset: 0x000008B0
		public void UpdateSingleton()
		{
			BlockObjectTool blockObjectTool = this._toolService.ActiveTool as BlockObjectTool;
			if (blockObjectTool != null)
			{
				this.UpdateText(blockObjectTool.WarningText);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026EA File Offset: 0x000008EA
		public void UpdateText(string warning)
		{
			if (!string.IsNullOrWhiteSpace(warning))
			{
				this._root.ToggleDisplayStyle(true);
				this._text.text = warning;
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000020 RID: 32
		public readonly ToolService _toolService;

		// Token: 0x04000021 RID: 33
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000022 RID: 34
		public VisualElement _root;

		// Token: 0x04000023 RID: 35
		public Label _text;
	}
}
