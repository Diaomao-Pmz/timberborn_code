using System;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.ToolSystemUI
{
	// Token: 0x02000005 RID: 5
	public class DescriptionPanelController : IToolFragment, IUpdatableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002140 File Offset: 0x00000340
		public DescriptionPanelController(EventBus eventBus, DescriptionPanels descriptionPanels)
		{
			this._eventBus = eventBus;
			this._descriptionPanels = descriptionPanels;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002161 File Offset: 0x00000361
		public VisualElement InitializeFragment()
		{
			this._eventBus.Register(this);
			this.Hide();
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000217B File Offset: 0x0000037B
		public void UpdateSingleton()
		{
			DescriptionPanel shownPanel = this._shownPanel;
			if (shownPanel == null)
			{
				return;
			}
			shownPanel.Update();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000218D File Offset: 0x0000038D
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			this._permanentTool = toolEnteredEvent.Tool;
			this.SetDescription(this._permanentTool);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021A7 File Offset: 0x000003A7
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this.Hide();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021AF File Offset: 0x000003AF
		[OnEvent]
		public void OnToolUnlocked(ToolUnlockedEvent toolUnlockedEvent)
		{
			if (toolUnlockedEvent.Tool == this._permanentTool)
			{
				this.SetDescription(this._permanentTool);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021CB File Offset: 0x000003CB
		[OnEvent]
		public void OnTemporaryToolEntered(TemporaryToolEnteredEvent temporaryToolEnteredEvent)
		{
			this.SetTemporaryTool(temporaryToolEnteredEvent.Tool);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D9 File Offset: 0x000003D9
		[OnEvent]
		public void OnTemporaryToolExited(TemporaryToolExitedEvent temporaryToolExitedEvent)
		{
			this.ClearTemporaryTool();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021E1 File Offset: 0x000003E1
		public void SetTemporaryTool(ITool tool)
		{
			this.SetDescription(tool);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021EA File Offset: 0x000003EA
		public void ClearTemporaryTool()
		{
			this.SetDescription(this._permanentTool);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F8 File Offset: 0x000003F8
		public void SetDescription(ITool tool)
		{
			this.Hide();
			IToolDescriptor toolDescriptor = tool as IToolDescriptor;
			if (toolDescriptor != null)
			{
				DescriptionPanel descriptionPanel = this._descriptionPanels.GetDescriptionPanel(toolDescriptor);
				this.Show(descriptionPanel);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002229 File Offset: 0x00000429
		public void Show(DescriptionPanel panel)
		{
			this._shownPanel = panel;
			panel.Update();
			this._root.Add(panel.Root);
			this._root.ToggleDisplayStyle(true);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002255 File Offset: 0x00000455
		public void Hide()
		{
			this._shownPanel = null;
			this._root.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly DescriptionPanels _descriptionPanels;

		// Token: 0x0400000A RID: 10
		public ITool _permanentTool;

		// Token: 0x0400000B RID: 11
		public readonly VisualElement _root = new VisualElement();

		// Token: 0x0400000C RID: 12
		public DescriptionPanel _shownPanel;
	}
}
