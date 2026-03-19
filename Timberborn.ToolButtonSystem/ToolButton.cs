using System;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.ToolButtonSystem
{
	// Token: 0x02000007 RID: 7
	public class ToolButton : IToolbarButton
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021E6 File Offset: 0x000003E6
		public ITool Tool { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021EE File Offset: 0x000003EE
		public VisualElement Root { get; }

		// Token: 0x0600000F RID: 15 RVA: 0x000021F8 File Offset: 0x000003F8
		public ToolButton(ToolService toolService, DevModeManager devModeManager, ToolGroupService toolGroupService, MapEditorMode mapEditorMode, ImmutableArray<IToolDisabler> toolDisablers, ITool tool, VisualElement root, Button button)
		{
			this._toolService = toolService;
			this._devModeManager = devModeManager;
			this._toolGroupService = toolGroupService;
			this._mapEditorMode = mapEditorMode;
			this._toolDisablers = toolDisablers;
			this.Tool = tool;
			this.Root = root;
			this._button = button;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002248 File Offset: 0x00000448
		public bool ToolEnabled
		{
			get
			{
				return this._devModeManager.Enabled || this._mapEditorMode.IsMapEditor || (!this.DevModeTool && this._toolDisablers.FastAll((IToolDisabler disabler) => disabler.IsEnabled(this.Tool)));
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002297 File Offset: 0x00000497
		public bool IsVisible
		{
			get
			{
				return this.ToolEnabled && (!this._toolGroupService.IsAssignedToAnyGroup(this.Tool) || this._toolGroupService.IsAssignedToGroup(this.Tool, this._toolGroupService.ActiveToolGroup));
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022D4 File Offset: 0x000004D4
		public bool IsActive
		{
			get
			{
				return this._toolService.ActiveTool == this.Tool;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022EC File Offset: 0x000004EC
		public void PostLoad()
		{
			this._button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._toolService.SwitchTool(this.Tool);
			}, 0);
			this._tooltip = UQueryExtensions.Q<Label>(this.Root, "Tooltip", null);
			this.Root.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this._toolService.SetTemporaryTool(this.Tool);
			}, 0);
			this.Root.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this._toolService.ClearTemporaryTool();
			}, 0);
			this.UpdateToolVisibility();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000235E File Offset: 0x0000055E
		public void InitializeTooltip(string tooltipText)
		{
			this._tooltipText = tooltipText;
			this.Root.RegisterCallback<MouseOverEvent>(new EventCallback<MouseOverEvent>(this.ShowTooltip), 0);
			this.Root.RegisterCallback<MouseOutEvent>(new EventCallback<MouseOutEvent>(this.HideTooltip), 0);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002397 File Offset: 0x00000597
		[OnEvent]
		public void OnDevModeToggledEvent(DevModeToggledEvent devModeToggledEvent)
		{
			this.UpdateToolVisibility();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000239F File Offset: 0x0000059F
		[OnEvent]
		public void OnToolGroupEnteredEvent(ToolGroupEnteredEvent toolGroupEnteredEvent)
		{
			if (this._toolGroupService.IsAssignedToGroup(this.Tool, toolGroupEnteredEvent.ToolGroup))
			{
				this.UpdateToolVisibility();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023C0 File Offset: 0x000005C0
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			if (toolEnteredEvent.Tool == this.Tool)
			{
				this.Root.AddToClassList(ToolButton.ActiveClassName);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E0 File Offset: 0x000005E0
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			if (toolExitedEvent.Tool == this.Tool)
			{
				this.Root.RemoveFromClassList(ToolButton.ActiveClassName);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002400 File Offset: 0x00000600
		[OnEvent]
		public void OnToolLocked(ToolLockedEvent toolLockedEvent)
		{
			if (toolLockedEvent.Tool == this.Tool)
			{
				this._button.AddToClassList(ToolButton.LockedClassName);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002420 File Offset: 0x00000620
		[OnEvent]
		public void OnToolUnlocked(ToolUnlockedEvent toolUnlockedEvent)
		{
			if (toolUnlockedEvent.Tool == this.Tool)
			{
				this._button.RemoveFromClassList(ToolButton.LockedClassName);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002440 File Offset: 0x00000640
		public void Select()
		{
			this._toolService.SwitchTool(this.Tool);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002454 File Offset: 0x00000654
		public bool DevModeTool
		{
			get
			{
				IDevModeTool devModeTool = this.Tool as IDevModeTool;
				return devModeTool != null && devModeTool.IsDevMode;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002478 File Offset: 0x00000678
		public void ShowTooltip(MouseOverEvent mouseOverEvent)
		{
			if (this._tooltip != null)
			{
				this._tooltip.text = this._tooltipText;
				this._tooltip.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000249F File Offset: 0x0000069F
		public void HideTooltip(MouseOutEvent mouseOutEvent)
		{
			Label tooltip = this._tooltip;
			if (tooltip == null)
			{
				return;
			}
			tooltip.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024B2 File Offset: 0x000006B2
		public void UpdateToolVisibility()
		{
			this.Root.ToggleDisplayStyle(this.ToolEnabled);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string ActiveClassName = "button--active";

		// Token: 0x04000009 RID: 9
		public static readonly string LockedClassName = "button--locked";

		// Token: 0x0400000C RID: 12
		public readonly ToolService _toolService;

		// Token: 0x0400000D RID: 13
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400000E RID: 14
		public readonly ToolGroupService _toolGroupService;

		// Token: 0x0400000F RID: 15
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000010 RID: 16
		public readonly ImmutableArray<IToolDisabler> _toolDisablers;

		// Token: 0x04000011 RID: 17
		public readonly Button _button;

		// Token: 0x04000012 RID: 18
		public Label _tooltip;

		// Token: 0x04000013 RID: 19
		public string _tooltipText;

		// Token: 0x04000014 RID: 20
		public bool _containedMouse;
	}
}
